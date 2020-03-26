namespace CCTools.CCDesign
{
	partial class OptionsDialog
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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileLocationGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.windowsDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.automaticallyOpenNewLevelsCheckBox = new System.Windows.Forms.CheckBox();
            this.placeChipAtCenterCheckBox = new System.Windows.Forms.CheckBox();
            this.surroundLevelCheckBox = new System.Windows.Forms.CheckBox();
            this.bottomPanel.SuspendLayout();
            this.fileLocationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.okButton);
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 226);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(484, 48);
            this.bottomPanel.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(316, 13);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(397, 13);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Applications|*.exe|All Files|*.*";
            // 
            // fileLocationGroupBox
            // 
            this.fileLocationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileLocationGroupBox.Controls.Add(this.label2);
            this.fileLocationGroupBox.Controls.Add(this.windowsDirTextBox);
            this.fileLocationGroupBox.Controls.Add(this.label1);
            this.fileLocationGroupBox.Controls.Add(this.fileLocationTextBox);
            this.fileLocationGroupBox.Controls.Add(this.browseButton);
            this.fileLocationGroupBox.Location = new System.Drawing.Point(12, 90);
            this.fileLocationGroupBox.Name = "fileLocationGroupBox";
            this.fileLocationGroupBox.Size = new System.Drawing.Size(464, 125);
            this.fileLocationGroupBox.TabIndex = 5;
            this.fileLocationGroupBox.TabStop = false;
            this.fileLocationGroupBox.Text = "&Testing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter your virtualized Windows directory here:";
            // 
            // windowsDirTextBox
            // 
            this.windowsDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.windowsDirTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CCTools.CCDesign.Properties.Settings.Default, "WindowsDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.windowsDirTextBox.Location = new System.Drawing.Point(6, 81);
            this.windowsDirTextBox.Name = "windowsDirTextBox";
            this.windowsDirTextBox.Size = new System.Drawing.Size(452, 23);
            this.windowsDirTextBox.TabIndex = 4;
            this.windowsDirTextBox.Text = global::CCTools.CCDesign.Properties.Settings.Default.WindowsDirectory;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the location of your chips.exe file here:";
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileLocationTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CCTools.CCDesign.Properties.Settings.Default, "ChipsChallengeLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fileLocationTextBox.Location = new System.Drawing.Point(6, 37);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.Size = new System.Drawing.Size(371, 23);
            this.fileLocationTextBox.TabIndex = 1;
            this.fileLocationTextBox.Text = global::CCTools.CCDesign.Properties.Settings.Default.ChipsChallengeLocation;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.browseButton.Location = new System.Drawing.Point(383, 37);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "&Browse...";
            // 
            // automaticallyOpenNewLevelsCheckBox
            // 
            this.automaticallyOpenNewLevelsCheckBox.AutoSize = true;
            this.automaticallyOpenNewLevelsCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.AutomaticallyOpenNewLevels;
            this.automaticallyOpenNewLevelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.automaticallyOpenNewLevelsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "AutomaticallyOpenNewLevels", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.automaticallyOpenNewLevelsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.automaticallyOpenNewLevelsCheckBox.Location = new System.Drawing.Point(12, 12);
            this.automaticallyOpenNewLevelsCheckBox.Name = "automaticallyOpenNewLevelsCheckBox";
            this.automaticallyOpenNewLevelsCheckBox.Size = new System.Drawing.Size(193, 20);
            this.automaticallyOpenNewLevelsCheckBox.TabIndex = 3;
            this.automaticallyOpenNewLevelsCheckBox.Text = "&Automatically open new levels";
            this.automaticallyOpenNewLevelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // placeChipAtCenterCheckBox
            // 
            this.placeChipAtCenterCheckBox.AutoSize = true;
            this.placeChipAtCenterCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.PlaceChipAtCenter;
            this.placeChipAtCenterCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "PlaceChipAtCenter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.placeChipAtCenterCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.placeChipAtCenterCheckBox.Location = new System.Drawing.Point(12, 38);
            this.placeChipAtCenterCheckBox.Name = "placeChipAtCenterCheckBox";
            this.placeChipAtCenterCheckBox.Size = new System.Drawing.Size(207, 20);
            this.placeChipAtCenterCheckBox.TabIndex = 4;
            this.placeChipAtCenterCheckBox.Text = "&Place Chip at center in new levels";
            this.placeChipAtCenterCheckBox.UseVisualStyleBackColor = true;
            // 
            // surroundLevelCheckBox
            // 
            this.surroundLevelCheckBox.AutoSize = true;
            this.surroundLevelCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.SurroundNewLevelsWithWalls;
            this.surroundLevelCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "SurroundNewLevelsWithWalls", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.surroundLevelCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.surroundLevelCheckBox.Location = new System.Drawing.Point(12, 64);
            this.surroundLevelCheckBox.Name = "surroundLevelCheckBox";
            this.surroundLevelCheckBox.Size = new System.Drawing.Size(193, 20);
            this.surroundLevelCheckBox.TabIndex = 6;
            this.surroundLevelCheckBox.Text = "&Surround new levels with walls";
            this.surroundLevelCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(484, 274);
            this.Controls.Add(this.fileLocationGroupBox);
            this.Controls.Add(this.automaticallyOpenNewLevelsCheckBox);
            this.Controls.Add(this.placeChipAtCenterCheckBox);
            this.Controls.Add(this.surroundLevelCheckBox);
            this.Controls.Add(this.bottomPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsDialog_FormClosing);
            this.bottomPanel.ResumeLayout(false);
            this.fileLocationGroupBox.ResumeLayout(false);
            this.fileLocationGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox fileLocationGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox windowsDirTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.CheckBox automaticallyOpenNewLevelsCheckBox;
        private System.Windows.Forms.CheckBox placeChipAtCenterCheckBox;
        private System.Windows.Forms.CheckBox surroundLevelCheckBox;
    }
}