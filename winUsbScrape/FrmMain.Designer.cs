namespace winUsbScrape
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            lblTitle = new Label();
            sltCpyAll = new CheckBox();
            lstItems = new CheckedListBox();
            bxTerminal = new RichTextBox();
            btnReady = new Button();
            checkVerbose = new CheckBox();
            lblFoundFiles = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Impact", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(68, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(263, 34);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Windows Disk Scraper";
            // 
            // sltCpyAll
            // 
            sltCpyAll.AutoSize = true;
            sltCpyAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            sltCpyAll.ForeColor = Color.BlanchedAlmond;
            sltCpyAll.Location = new Point(12, 69);
            sltCpyAll.Name = "sltCpyAll";
            sltCpyAll.Size = new Size(97, 19);
            sltCpyAll.TabIndex = 1;
            sltCpyAll.Text = "Copy All Files";
            sltCpyAll.UseVisualStyleBackColor = true;
            sltCpyAll.CheckedChanged += btnCpyAll_CheckedChanged;
            // 
            // lstItems
            // 
            lstItems.BackColor = Color.FromArgb(45, 45, 45);
            lstItems.BorderStyle = BorderStyle.FixedSingle;
            lstItems.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lstItems.ForeColor = Color.BlanchedAlmond;
            lstItems.FormattingEnabled = true;
            lstItems.Items.AddRange(new object[] { "Text", "Images", "Executables", "Compressed", "Databases" });
            lstItems.Location = new Point(12, 94);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(120, 92);
            lstItems.TabIndex = 2;
            // 
            // bxTerminal
            // 
            bxTerminal.BackColor = Color.Black;
            bxTerminal.BorderStyle = BorderStyle.FixedSingle;
            bxTerminal.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            bxTerminal.ForeColor = Color.Gainsboro;
            bxTerminal.Location = new Point(138, 46);
            bxTerminal.Name = "bxTerminal";
            bxTerminal.ReadOnly = true;
            bxTerminal.RightToLeft = RightToLeft.No;
            bxTerminal.ScrollBars = RichTextBoxScrollBars.Horizontal;
            bxTerminal.Size = new Size(228, 158);
            bxTerminal.TabIndex = 3;
            bxTerminal.Text = resources.GetString("bxTerminal.Text");
            // 
            // btnReady
            // 
            btnReady.BackColor = Color.FromArgb(40, 40, 40);
            btnReady.FlatStyle = FlatStyle.Flat;
            btnReady.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnReady.ForeColor = Color.BlanchedAlmond;
            btnReady.Location = new Point(12, 192);
            btnReady.Name = "btnReady";
            btnReady.Size = new Size(120, 28);
            btnReady.TabIndex = 4;
            btnReady.Text = "Ready";
            btnReady.UseVisualStyleBackColor = false;
            btnReady.Click += btnReady_Click;
            // 
            // checkVerbose
            // 
            checkVerbose.AutoSize = true;
            checkVerbose.Checked = true;
            checkVerbose.CheckState = CheckState.Checked;
            checkVerbose.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            checkVerbose.ForeColor = Color.BlanchedAlmond;
            checkVerbose.Location = new Point(12, 48);
            checkVerbose.Name = "checkVerbose";
            checkVerbose.Size = new Size(71, 19);
            checkVerbose.TabIndex = 5;
            checkVerbose.Text = "Verbose";
            checkVerbose.UseVisualStyleBackColor = true;
            // 
            // lblFoundFiles
            // 
            lblFoundFiles.AutoSize = true;
            lblFoundFiles.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblFoundFiles.ForeColor = Color.White;
            lblFoundFiles.Location = new Point(138, 205);
            lblFoundFiles.Name = "lblFoundFiles";
            lblFoundFiles.Size = new Size(125, 17);
            lblFoundFiles.TabIndex = 6;
            lblFoundFiles.Text = "Waiting for drive...";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(384, 232);
            Controls.Add(lblFoundFiles);
            Controls.Add(checkVerbose);
            Controls.Add(btnReady);
            Controls.Add(bxTerminal);
            Controls.Add(lstItems);
            Controls.Add(sltCpyAll);
            Controls.Add(lblTitle);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Windows Disk Scraper - By Fclown";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private CheckBox sltCpyAll;
        private CheckedListBox lstItems;
        private RichTextBox bxTerminal;
        private Button btnReady;
        private CheckBox checkVerbose;
        private Label lblFoundFiles;
    }
}