namespace TT425_Lotus_Monorail
{
    partial class ScriptsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            ScriptLogBox = new RichTextBox();
            runScriptButton = new Button();
            SuspendLayout();
            // 
            // ScriptLogBox
            // 
            ScriptLogBox.Location = new Point(12, 71);
            ScriptLogBox.Name = "ScriptLogBox";
            ScriptLogBox.Size = new Size(396, 172);
            ScriptLogBox.TabIndex = 2;
            ScriptLogBox.Text = "function echo(2) return s end return echo(\"Hello!\")";
            // 
            // runScriptButton
            // 
            runScriptButton.Location = new Point(12, 12);
            runScriptButton.Name = "runScriptButton";
            runScriptButton.Size = new Size(68, 53);
            runScriptButton.TabIndex = 3;
            runScriptButton.Text = "Run";
            runScriptButton.UseVisualStyleBackColor = true;
            runScriptButton.Click += runScriptButton_Click_1;
            // 
            // ScriptsWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 255);
            Controls.Add(runScriptButton);
            Controls.Add(ScriptLogBox);
            Name = "ScriptsWindow";
            Text = "ScriptsWindow";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox ScriptLogBox;
        private Button runScriptButton;
    }
}