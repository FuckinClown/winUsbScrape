using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Management;
using System.Windows.Forms;

namespace winUsbScrape
{
    public partial class FrmMain : Form
    {
        bool ShowItemLst = true;
        bool ready;
        int FilesFound;
        string dwnDir;
        DriveInfo[] initDrives;
        IEnumerable<DriveInfo> newDrives;
        static readonly object counterLock = new object();

        #region Lists

        string[] File_word = new string[]{
            "DOC",
            "DOCX",
            "ODT",
            "PDF",
            "RTF",
            "TEX",
            "TXT",
            "WPD"
        };
        string[] File_Image = new string[]{
            "JPG",
            "PNG",
            "GIF",
            "WEBP",
            "RAW",
            "BMP",
            "HEIF",
            "ICO"
        };
        string[] File_Executable = new string[]{
            "EXE",
            "MSI",
            "BAT",
            "CMD",
            "DEB",
            "RPM"
        };
        string[] File_Compressed = new string[]{
            "TAR.GZ",
            "TAR",
            "GZ",
            "ZIP",
            "7Z",
            "LZO",
            "LZ4",
            "PKG"
        };
        string[] File_Database = new string[]{
            "CSV",
            "DAT",
            "DB",
            "LOG",
            "MDB",
            "SAV",
            "SQL",
            "XML"
        };

        #endregion

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCpyAll_CheckedChanged(object sender, EventArgs e)
        {
            ShowItemLst = !ShowItemLst;
            lstItems.Enabled = ShowItemLst;
            lstItems.ClearSelected();
            if (!ShowItemLst) bxTerminal.Clear();
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            if (lstItems.CheckedItems.Count <= 0 && sltCpyAll.Checked == false)
                Log("[!] Please select the file types to copy", false);

            if (!ready)
            {
                Log("[.] Excluding current drives\n===========================", true);
                initDrives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in initDrives)
                    Log($"{drive.Name} {drive.DriveType}, {(((drive.TotalSize / 1000) / 1000) / 1000)} GB", false);
                Log("===========================\n[?] Please plug-in your drive(s), then click the button again", false);
                ready = true;
                btnReady.Text = "Scan Drive";
            }
            else
            {
                newDrives = DriveInfo.GetDrives().Where(d => !initDrives.Any(initDrive => string.Equals(initDrive.Name, d.Name, StringComparison.OrdinalIgnoreCase)));
                if (newDrives.Count() > 0)
                {
                    lblFoundFiles.Text = "Found Files: ";
                    Log("[!] Found drive(s)!", true);
                    foreach (DriveInfo drive in newDrives)
                    {
                        Log($"{drive.Name} {drive.DriveType}, {(((drive.TotalSize / 1000) / 1000) / 1000)} GB", false);
                        CopyFiles(drive.Name);
                    }
                }
                else Log("[!] No drives found", true); ready = false; btnReady.Text = "Ready";
            }
        }

        private void CopyFiles(string rootDir)
        {
            dwnDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"{rootDir[0]} Drive");
            Directory.CreateDirectory(dwnDir);

            // Check if user selected anything
            // Maybe check for this at the beginning?
            if (lstItems.CheckedItems.Count > 0 && ShowItemLst)
            {
                Log("[*] Scanning every file", false);
                WalkDirectory(
                    rootDir,
                    lstItems.CheckedItems.Cast<string>().ToArray(),
                    dwnDir
                );
            }
            else if (!ShowItemLst)
            {
                Log("[*] Copying every file...", true);
            }
            else if (lstItems.CheckedItems.Count <= 0)
            {
                Log("[?] Please select the files to copy", false);
            }
            Log($"[!] Done! Ejecting Drive...", false);
            Program.EjectDrive(rootDir);
        }

        private void Log(string msg, bool clearScreen)
        {
            if (checkVerbose.Checked)
            {
                if (clearScreen)
                {
                    bxTerminal.Clear();
                    bxTerminal.Text += msg;
                }
                else
                {
                    bxTerminal.Text += "\n" + msg;

                    bxTerminal.SelectionStart = bxTerminal.Text.Length;
                    bxTerminal.ScrollToCaret();
                }
            }
        }

        void WalkDirectory(string dir, string[] LookFor, string DownloadDir)
        {
            try
            {
                var subdirectories = Directory.GetDirectories(dir);

                foreach (var subdirectory in subdirectories)
                {
                    Task.Run(() =>
                    WalkDirectory(subdirectory, LookFor, DownloadDir));
                }

                var files = Directory.GetFiles(dir);

                foreach (var file in files)
                {   //I hate long code as much as the next person
                    //I mean, 2 &&'s and 10 ()'s in one if statement? Booooo
                    //Buuuuuuuuuuuuuuuuuuuuut

                    if (LookFor.Contains("Text") && File_word.Contains(file.Split('.').Last().ToUpper()))
                    {
                        Task.Run(() => DownloadFile(file, "Text"));
                    }

                    if (LookFor.Contains("Images") && File_Image.Contains(file.Split('.').Last().ToUpper()))
                    {
                        Task.Run(() => DownloadFile(file, "Images"));
                    }

                    if (LookFor.Contains("Executables") && File_Executable.Contains(file.Split('.').Last().ToUpper()))
                    {
                        Task.Run(() => DownloadFile(file, "Executables"));
                    }

                    if (LookFor.Contains("Compressed") && File_Compressed.Contains(file.Split('.').Last().ToUpper()))
                    {
                        Task.Run(() => DownloadFile(file, "Compressed"));
                    }

                    if (LookFor.Contains("Databases") && File_Database.Contains(file.Split('.').Last().ToUpper()))
                    {
                        Task.Run(() => DownloadFile(file, "Databases"));
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Do nothing because we can do nothing
            }
            catch (Exception ex)
            {
                Log("[!] Unhandle error accured: " + ex.Message, false);
            }
        }

        private void DownloadFile(string filePath, string FileType)
        {
            switch (FileType)
            {
                // Yes, I could make a function and make it look "pretty"
                // I dont care, it works, I am NOT touching this
                case "Text":
                    string TextDest = Path.Combine(dwnDir, "Text");
                    if (!Directory.Exists(TextDest))
                        Directory.CreateDirectory(TextDest);

                    string TextFilePath = Path.Combine(TextDest, Path.GetFileName(filePath));
                    try
                    {
                        File.Copy(filePath, TextFilePath, false);
                    }
                    catch (IOException)
                    {
                        File.Copy(filePath, new Random().Next(1000) + TextFilePath, true);
                    }
                    break;
                case "Images":
                    string imagesDest = Path.Combine(dwnDir, "Images");
                    if (!Directory.Exists(imagesDest))
                        Directory.CreateDirectory(imagesDest);

                    string imagesFilePath = Path.Combine(imagesDest, Path.GetFileName(filePath));
                    try
                    {
                        File.Copy(filePath, imagesFilePath, true);
                    }
                    catch (IOException)
                    {
                        File.Copy(filePath, new Random().Next(1000) + imagesFilePath, true);
                    }
                    break;

                case "Executables":
                    string executablesDest = Path.Combine(dwnDir, "Executables");
                    if (!Directory.Exists(executablesDest))
                        Directory.CreateDirectory(executablesDest);

                    string executablesFilePath = Path.Combine(executablesDest, Path.GetFileName(filePath));
                    try
                    {
                        File.Copy(filePath, executablesFilePath);
                    }
                    catch (IOException)
                    {
                        File.Copy(filePath, new Random().Next(1000) + executablesFilePath, true);
                    }
                    break;

                case "Compressed":
                    string compressedDest = Path.Combine(dwnDir, "Compressed");
                    if (!Directory.Exists(compressedDest))
                        Directory.CreateDirectory(compressedDest);

                    string compressedFilePath = Path.Combine(compressedDest, Path.GetFileName(filePath));
                    try
                    {
                        File.Copy(filePath, compressedFilePath);
                    }
                    catch (IOException)
                    {
                        File.Copy(filePath, new Random().Next(1000) + compressedFilePath, true);
                    }
                    break;

                case "Databases":
                    string databasesDest = Path.Combine(dwnDir, "Databases");
                    if (!Directory.Exists(databasesDest))
                        Directory.CreateDirectory(databasesDest);

                    string databasesFilePath = Path.Combine(databasesDest, Path.GetFileName(filePath));
                    try
                    {
                        File.Copy(filePath, databasesFilePath);
                    }
                    catch (IOException)
                    {
                        File.Copy(filePath, new Random().Next(1000) + databasesFilePath, true);
                    }
                    break;

            }
            IncrementFileCounter();
        }

        void IncrementFileCounter()
        {
            lock (counterLock)
            {
                FilesFound++;
                lblFoundFiles.Invoke((MethodInvoker)delegate
                {
                    lblFoundFiles.Text = $"Downloaded Files: {FilesFound}";
                });
            }
        }
    }
}