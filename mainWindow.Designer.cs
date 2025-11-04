namespace TT425_Lotus_Monorail
{
    partial class mainWindow
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            folderSelector = new FolderBrowserDialog();
            folderLocationBox = new TextBox();
            logBox = new RichTextBox();
            removableDrivesSelection = new ComboBox();
            statusLight = new PictureBox();
            fileCheckerStatusText = new RichTextBox();
            USBCheckLight = new PictureBox();
            USBStatusText = new RichTextBox();
            RefreshButton = new PictureBox();
            changeFolderIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)statusLight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)USBCheckLight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RefreshButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)changeFolderIcon).BeginInit();
            SuspendLayout();
            // 
            // folderSelector
            // 
            folderSelector.Description = "Folder With Bar Optimised Files";
            folderSelector.RootFolder = Environment.SpecialFolder.MyComputer;
            folderSelector.SelectedPath = "C:\\sawfiles_two";
            folderSelector.UseDescriptionForTitle = true;
            // 
            // folderLocationBox
            // 
            folderLocationBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            folderLocationBox.BorderStyle = BorderStyle.FixedSingle;
            folderLocationBox.Cursor = Cursors.Hand;
            folderLocationBox.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderLocationBox.Location = new Point(12, 26);
            folderLocationBox.Margin = new Padding(9, 6, 9, 6);
            folderLocationBox.MaximumSize = new Size(378, 26);
            folderLocationBox.Name = "folderLocationBox";
            folderLocationBox.ReadOnly = true;
            folderLocationBox.Size = new Size(378, 26);
            folderLocationBox.TabIndex = 1;
            folderLocationBox.Text = "C:\\Sawfiles_two";
            folderLocationBox.MouseClick += locationBoxClick;
            // 
            // logBox
            // 
            logBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            logBox.BackColor = Color.DarkSlateBlue;
            logBox.BorderStyle = BorderStyle.FixedSingle;
            logBox.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            logBox.ForeColor = Color.MintCream;
            logBox.Location = new Point(12, 216);
            logBox.MaximumSize = new Size(378, 109);
            logBox.MinimumSize = new Size(378, 109);
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            logBox.Size = new Size(378, 109);
            logBox.TabIndex = 3;
            logBox.Text = "Loading...";
            // 
            // removableDrivesSelection
            // 
            removableDrivesSelection.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            removableDrivesSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            removableDrivesSelection.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            removableDrivesSelection.FormattingEnabled = true;
            removableDrivesSelection.Location = new Point(12, 93);
            removableDrivesSelection.MaximumSize = new Size(378, 0);
            removableDrivesSelection.MinimumSize = new Size(378, 0);
            removableDrivesSelection.Name = "removableDrivesSelection";
            removableDrivesSelection.Size = new Size(378, 24);
            removableDrivesSelection.TabIndex = 4;
            removableDrivesSelection.SelectedIndexChanged += removableDrivesSelection_SelectedIndexChanged;
            // 
            // statusLight
            // 
            statusLight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            statusLight.Image = Properties.Resources.FileCheckNG;
            statusLight.Location = new Point(12, 61);
            statusLight.Name = "statusLight";
            statusLight.Size = new Size(26, 26);
            statusLight.TabIndex = 6;
            statusLight.TabStop = false;
            // 
            // fileCheckerStatusText
            // 
            fileCheckerStatusText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            fileCheckerStatusText.BackColor = Color.Black;
            fileCheckerStatusText.BorderStyle = BorderStyle.FixedSingle;
            fileCheckerStatusText.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fileCheckerStatusText.ForeColor = Color.White;
            fileCheckerStatusText.Location = new Point(43, 61);
            fileCheckerStatusText.Multiline = false;
            fileCheckerStatusText.Name = "fileCheckerStatusText";
            fileCheckerStatusText.ReadOnly = true;
            fileCheckerStatusText.ScrollBars = RichTextBoxScrollBars.None;
            fileCheckerStatusText.Size = new Size(347, 26);
            fileCheckerStatusText.TabIndex = 7;
            fileCheckerStatusText.Text = "Checking folder for saw files...";
            fileCheckerStatusText.WordWrap = false;
            // 
            // USBCheckLight
            // 
            USBCheckLight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            USBCheckLight.Image = Properties.Resources.FileCheckNG;
            USBCheckLight.Location = new Point(12, 123);
            USBCheckLight.Name = "USBCheckLight";
            USBCheckLight.Size = new Size(26, 26);
            USBCheckLight.TabIndex = 8;
            USBCheckLight.TabStop = false;
            // 
            // USBStatusText
            // 
            USBStatusText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            USBStatusText.BackColor = Color.Black;
            USBStatusText.BorderStyle = BorderStyle.FixedSingle;
            USBStatusText.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            USBStatusText.ForeColor = Color.White;
            USBStatusText.Location = new Point(43, 124);
            USBStatusText.Multiline = false;
            USBStatusText.Name = "USBStatusText";
            USBStatusText.ReadOnly = true;
            USBStatusText.ScrollBars = RichTextBoxScrollBars.None;
            USBStatusText.Size = new Size(302, 26);
            USBStatusText.TabIndex = 9;
            USBStatusText.Text = "Checking USB...";
            USBStatusText.WordWrap = false;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RefreshButton.Image = Properties.Resources.RefreshUsbIcon;
            RefreshButton.Location = new Point(349, 121);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(39, 30);
            RefreshButton.TabIndex = 10;
            RefreshButton.TabStop = false;
            RefreshButton.Click += RefreshButton_Click;
            RefreshButton.MouseEnter += RefreshButton_MouseEnter;
            RefreshButton.MouseLeave += RefreshButton_MouseLeave;
            // 
            // changeFolderIcon
            // 
            changeFolderIcon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            changeFolderIcon.Image = Properties.Resources.ChangeFolderIcon;
            changeFolderIcon.Location = new Point(12, 6);
            changeFolderIcon.Name = "changeFolderIcon";
            changeFolderIcon.Size = new Size(25, 20);
            changeFolderIcon.TabIndex = 11;
            changeFolderIcon.TabStop = false;
            changeFolderIcon.Click += changeFolderIcon_Click;
            changeFolderIcon.MouseEnter += changeFolderIcon_MouseEnter;
            changeFolderIcon.MouseLeave += changeFolderIcon_MouseLeave;
            // 
            // mainWindow
            // 
            AutoScaleDimensions = new SizeF(12F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 337);
            Controls.Add(changeFolderIcon);
            Controls.Add(RefreshButton);
            Controls.Add(USBStatusText);
            Controls.Add(USBCheckLight);
            Controls.Add(fileCheckerStatusText);
            Controls.Add(statusLight);
            Controls.Add(removableDrivesSelection);
            Controls.Add(logBox);
            Controls.Add(folderLocationBox);
            Font = new Font("MS PMincho", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximumSize = new Size(416, 363);
            MinimumSize = new Size(416, 363);
            Name = "mainWindow";
            Text = "Lotus Monorail";
            Click += mainWindow_Click;
            ((System.ComponentModel.ISupportInitialize)statusLight).EndInit();
            ((System.ComponentModel.ISupportInitialize)USBCheckLight).EndInit();
            ((System.ComponentModel.ISupportInitialize)RefreshButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)changeFolderIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox folderLocationBox;

        private System.Windows.Forms.FolderBrowserDialog folderSelector;

        #endregion
        private RichTextBox logBox;
        private ComboBox removableDrivesSelection;
        private PictureBox statusLight;
        private RichTextBox fileCheckerStatusText;
        private PictureBox USBCheckLight;
        private RichTextBox USBStatusText;
        private PictureBox RefreshButton;
        private PictureBox changeFolderIcon;
    }
}
