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
            PowerButton = new PictureBox();
            GoLabel = new Label();
            pictureBox1 = new PictureBox();
            exitToolStripMenuItem = new ToolStripMenuItem();
            convertFilesTT425ToolStripMenuItem = new ToolStripMenuItem();
            convertToTT425ToolStripMenuItem = new ToolStripMenuItem();
            refreshUSBToolStripMenuItem = new ToolStripMenuItem();
            clearLogToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            patchNotesToolStripMenuItem = new ToolStripMenuItem();
            autoClearBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)statusLight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)USBCheckLight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RefreshButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)changeFolderIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PowerButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
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
            folderLocationBox.Anchor = AnchorStyles.None;
            folderLocationBox.BorderStyle = BorderStyle.FixedSingle;
            folderLocationBox.Cursor = Cursors.Hand;
            folderLocationBox.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderLocationBox.Location = new Point(10, 56);
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
            logBox.Anchor = AnchorStyles.None;
            logBox.BackColor = Color.DarkSlateBlue;
            logBox.BorderStyle = BorderStyle.FixedSingle;
            logBox.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            logBox.ForeColor = Color.MintCream;
            logBox.Location = new Point(10, 248);
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
            removableDrivesSelection.Anchor = AnchorStyles.None;
            removableDrivesSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            removableDrivesSelection.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            removableDrivesSelection.FormattingEnabled = true;
            removableDrivesSelection.Location = new Point(10, 122);
            removableDrivesSelection.MaximumSize = new Size(378, 0);
            removableDrivesSelection.MinimumSize = new Size(378, 0);
            removableDrivesSelection.Name = "removableDrivesSelection";
            removableDrivesSelection.Size = new Size(378, 24);
            removableDrivesSelection.TabIndex = 4;
            removableDrivesSelection.SelectedIndexChanged += removableDrivesSelection_SelectedIndexChanged;
            // 
            // statusLight
            // 
            statusLight.Anchor = AnchorStyles.None;
            statusLight.Image = Properties.Resources.FileCheckNG;
            statusLight.Location = new Point(10, 90);
            statusLight.Name = "statusLight";
            statusLight.Size = new Size(26, 26);
            statusLight.TabIndex = 6;
            statusLight.TabStop = false;
            // 
            // fileCheckerStatusText
            // 
            fileCheckerStatusText.Anchor = AnchorStyles.None;
            fileCheckerStatusText.BackColor = Color.Black;
            fileCheckerStatusText.BorderStyle = BorderStyle.FixedSingle;
            fileCheckerStatusText.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fileCheckerStatusText.ForeColor = Color.White;
            fileCheckerStatusText.Location = new Point(42, 90);
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
            USBCheckLight.Anchor = AnchorStyles.None;
            USBCheckLight.Image = Properties.Resources.FileCheckNG;
            USBCheckLight.Location = new Point(10, 156);
            USBCheckLight.Name = "USBCheckLight";
            USBCheckLight.Size = new Size(26, 26);
            USBCheckLight.TabIndex = 8;
            USBCheckLight.TabStop = false;
            // 
            // USBStatusText
            // 
            USBStatusText.Anchor = AnchorStyles.None;
            USBStatusText.BackColor = Color.Black;
            USBStatusText.BorderStyle = BorderStyle.FixedSingle;
            USBStatusText.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            USBStatusText.ForeColor = Color.White;
            USBStatusText.Location = new Point(42, 156);
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
            RefreshButton.Anchor = AnchorStyles.None;
            RefreshButton.Image = Properties.Resources.RefreshUsbIcon;
            RefreshButton.Location = new Point(350, 152);
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
            changeFolderIcon.Anchor = AnchorStyles.None;
            changeFolderIcon.Image = Properties.Resources.ChangeFolderIcon;
            changeFolderIcon.Location = new Point(10, 35);
            changeFolderIcon.Name = "changeFolderIcon";
            changeFolderIcon.Size = new Size(25, 20);
            changeFolderIcon.TabIndex = 11;
            changeFolderIcon.TabStop = false;
            changeFolderIcon.Click += changeFolderIcon_Click;
            changeFolderIcon.MouseEnter += changeFolderIcon_MouseEnter;
            changeFolderIcon.MouseLeave += changeFolderIcon_MouseLeave;
            // 
            // PowerButton
            // 
            PowerButton.Anchor = AnchorStyles.None;
            PowerButton.Image = Properties.Resources.PowerIdle;
            PowerButton.Location = new Point(365, 220);
            PowerButton.Name = "PowerButton";
            PowerButton.Size = new Size(24, 24);
            PowerButton.TabIndex = 12;
            PowerButton.TabStop = false;
            PowerButton.Click += PowerButton_Click;
            PowerButton.MouseEnter += PowerButton_MouseEnter;
            PowerButton.MouseLeave += PowerButton_MouseLeave;
            // 
            // GoLabel
            // 
            GoLabel.Anchor = AnchorStyles.None;
            GoLabel.AutoSize = true;
            GoLabel.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GoLabel.ForeColor = SystemColors.ActiveCaptionText;
            GoLabel.Location = new Point(216, 225);
            GoLabel.Name = "GoLabel";
            GoLabel.Size = new Size(143, 16);
            GoLabel.TabIndex = 13;
            GoLabel.Text = "Convert to TT-425";
            GoLabel.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = Properties.Resources.tamao;
            pictureBox1.Location = new Point(10, 188);
            pictureBox1.MaximumSize = new Size(171, 60);
            pictureBox1.MinimumSize = new Size(171, 60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(171, 60);
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.ForeColor = Color.White;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(27, 20);
            exitToolStripMenuItem.Text = "X";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // convertFilesTT425ToolStripMenuItem
            // 
            convertFilesTT425ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { convertToTT425ToolStripMenuItem, refreshUSBToolStripMenuItem, clearLogToolStripMenuItem });
            convertFilesTT425ToolStripMenuItem.ForeColor = Color.White;
            convertFilesTT425ToolStripMenuItem.Name = "convertFilesTT425ToolStripMenuItem";
            convertFilesTT425ToolStripMenuItem.Size = new Size(51, 20);
            convertFilesTT425ToolStripMenuItem.Text = "File";
            // 
            // convertToTT425ToolStripMenuItem
            // 
            convertToTT425ToolStripMenuItem.Name = "convertToTT425ToolStripMenuItem";
            convertToTT425ToolStripMenuItem.Size = new Size(202, 22);
            convertToTT425ToolStripMenuItem.Text = "Convert To TT425";
            convertToTT425ToolStripMenuItem.Click += convertToTT425ToolStripMenuItem_Click;
            // 
            // refreshUSBToolStripMenuItem
            // 
            refreshUSBToolStripMenuItem.Name = "refreshUSBToolStripMenuItem";
            refreshUSBToolStripMenuItem.Size = new Size(202, 22);
            refreshUSBToolStripMenuItem.Text = "Refresh USB";
            refreshUSBToolStripMenuItem.Click += refreshUSBToolStripMenuItem_Click;
            // 
            // clearLogToolStripMenuItem
            // 
            clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            clearLogToolStripMenuItem.Size = new Size(202, 22);
            clearLogToolStripMenuItem.Text = "Clear Log";
            clearLogToolStripMenuItem.Click += clearLogToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackgroundImage = Properties.Resources.bg006;
            menuStrip1.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { exitToolStripMenuItem, toolStripMenuItem1, convertFilesTT425ToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RightToLeft = RightToLeft.Yes;
            menuStrip1.Size = new Size(398, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            menuStrip1.MouseDown += menuStrip1_MouseDown;
            menuStrip1.MouseMove += menuStrip1_MouseMove;
            menuStrip1.MouseUp += menuStrip1_MouseUp;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor = Color.Transparent;
            toolStripMenuItem1.ForeColor = Color.White;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(43, 20);
            toolStripMenuItem1.Text = "[-]";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { patchNotesToolStripMenuItem });
            helpToolStripMenuItem.ForeColor = Color.White;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(51, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // patchNotesToolStripMenuItem
            // 
            patchNotesToolStripMenuItem.Name = "patchNotesToolStripMenuItem";
            patchNotesToolStripMenuItem.Size = new Size(162, 22);
            patchNotesToolStripMenuItem.Text = "Patch Notes";
            patchNotesToolStripMenuItem.Click += patchNotesToolStripMenuItem_Click;
            // 
            // autoClearBox
            // 
            autoClearBox.AutoSize = true;
            autoClearBox.Checked = true;
            autoClearBox.CheckState = CheckState.Checked;
            autoClearBox.Font = new Font("SimSun", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            autoClearBox.ForeColor = Color.Black;
            autoClearBox.Location = new Point(10, 363);
            autoClearBox.Name = "autoClearBox";
            autoClearBox.Size = new Size(186, 20);
            autoClearBox.TabIndex = 16;
            autoClearBox.Text = "Clear original files";
            autoClearBox.UseVisualStyleBackColor = true;
            autoClearBox.CheckedChanged += autoClearBox_CheckedChanged;
            // 
            // mainWindow
            // 
            AutoScaleDimensions = new SizeF(12F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Lavender;
            ClientSize = new Size(398, 465);
            Controls.Add(autoClearBox);
            Controls.Add(pictureBox1);
            Controls.Add(GoLabel);
            Controls.Add(PowerButton);
            Controls.Add(changeFolderIcon);
            Controls.Add(RefreshButton);
            Controls.Add(USBStatusText);
            Controls.Add(USBCheckLight);
            Controls.Add(fileCheckerStatusText);
            Controls.Add(statusLight);
            Controls.Add(removableDrivesSelection);
            Controls.Add(logBox);
            Controls.Add(folderLocationBox);
            Controls.Add(menuStrip1);
            Font = new Font("MS PMincho", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ButtonHighlight;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "mainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lotus Monorail";
            Load += mainWindow_Load;
            Click += mainWindow_Click;
            MouseDown += mainWindow_MouseDown;
            MouseMove += mainWindow_MouseMove;
            MouseUp += mainWindow_MouseUp;
            ((System.ComponentModel.ISupportInitialize)statusLight).EndInit();
            ((System.ComponentModel.ISupportInitialize)USBCheckLight).EndInit();
            ((System.ComponentModel.ISupportInitialize)RefreshButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)changeFolderIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)PowerButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private PictureBox PowerButton;
        private Label GoLabel;
        private PictureBox pictureBox1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem convertFilesTT425ToolStripMenuItem;
        private ToolStripMenuItem convertToTT425ToolStripMenuItem;
        private ToolStripMenuItem refreshUSBToolStripMenuItem;
        private ToolStripMenuItem clearLogToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem patchNotesToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private CheckBox autoClearBox;
    }
}
