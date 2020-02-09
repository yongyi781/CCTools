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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.chipsChallengeTabPage = new System.Windows.Forms.TabPage();
			this.automaticallyOpenNewLevelsCheckBox = new System.Windows.Forms.CheckBox();
			this.placeChipAtCenterCheckBox = new System.Windows.Forms.CheckBox();
			this.surroundLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.generalTabPage = new System.Windows.Forms.TabPage();
			this.associateCheckBox = new System.Windows.Forms.CheckBox();
			this.fileLocationGroupBox = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fileLocationTextBox = new System.Windows.Forms.TextBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tabControl.SuspendLayout();
			this.chipsChallengeTabPage.SuspendLayout();
			this.generalTabPage.SuspendLayout();
			this.fileLocationGroupBox.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.chipsChallengeTabPage);
			this.tabControl.Controls.Add(this.generalTabPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(484, 150);
			this.tabControl.TabIndex = 0;
			// 
			// chipsChallengeTabPage
			// 
			this.chipsChallengeTabPage.Controls.Add(this.automaticallyOpenNewLevelsCheckBox);
			this.chipsChallengeTabPage.Controls.Add(this.placeChipAtCenterCheckBox);
			this.chipsChallengeTabPage.Controls.Add(this.surroundLevelCheckBox);
			this.chipsChallengeTabPage.Location = new System.Drawing.Point(4, 24);
			this.chipsChallengeTabPage.Name = "chipsChallengeTabPage";
			this.chipsChallengeTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.chipsChallengeTabPage.Size = new System.Drawing.Size(476, 122);
			this.chipsChallengeTabPage.TabIndex = 1;
			this.chipsChallengeTabPage.Text = "Chip\'s Challenge";
			this.chipsChallengeTabPage.UseVisualStyleBackColor = true;
			// 
			// automaticallyOpenNewLevelsCheckBox
			// 
			this.automaticallyOpenNewLevelsCheckBox.AutoSize = true;
			this.automaticallyOpenNewLevelsCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.AutomaticallyOpenNewLevels;
			this.automaticallyOpenNewLevelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.automaticallyOpenNewLevelsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "AutomaticallyOpenNewLevels", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.automaticallyOpenNewLevelsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.automaticallyOpenNewLevelsCheckBox.Location = new System.Drawing.Point(6, 19);
			this.automaticallyOpenNewLevelsCheckBox.Name = "automaticallyOpenNewLevelsCheckBox";
			this.automaticallyOpenNewLevelsCheckBox.Size = new System.Drawing.Size(193, 20);
			this.automaticallyOpenNewLevelsCheckBox.TabIndex = 0;
			this.automaticallyOpenNewLevelsCheckBox.Text = "&Automatically open new levels";
			this.automaticallyOpenNewLevelsCheckBox.UseVisualStyleBackColor = true;
			// 
			// placeChipAtCenterCheckBox
			// 
			this.placeChipAtCenterCheckBox.AutoSize = true;
			this.placeChipAtCenterCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.PlaceChipAtCenter;
			this.placeChipAtCenterCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "PlaceChipAtCenter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.placeChipAtCenterCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.placeChipAtCenterCheckBox.Location = new System.Drawing.Point(6, 45);
			this.placeChipAtCenterCheckBox.Name = "placeChipAtCenterCheckBox";
			this.placeChipAtCenterCheckBox.Size = new System.Drawing.Size(207, 20);
			this.placeChipAtCenterCheckBox.TabIndex = 1;
			this.placeChipAtCenterCheckBox.Text = "&Place Chip at center in new levels";
			this.placeChipAtCenterCheckBox.UseVisualStyleBackColor = true;
			// 
			// surroundLevelCheckBox
			// 
			this.surroundLevelCheckBox.AutoSize = true;
			this.surroundLevelCheckBox.Checked = global::CCTools.CCDesign.Properties.Settings.Default.SurroundNewLevelsWithWalls;
			this.surroundLevelCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CCTools.CCDesign.Properties.Settings.Default, "SurroundNewLevelsWithWalls", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.surroundLevelCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.surroundLevelCheckBox.Location = new System.Drawing.Point(6, 71);
			this.surroundLevelCheckBox.Name = "surroundLevelCheckBox";
			this.surroundLevelCheckBox.Size = new System.Drawing.Size(193, 20);
			this.surroundLevelCheckBox.TabIndex = 2;
			this.surroundLevelCheckBox.Text = "&Surround new levels with walls";
			this.surroundLevelCheckBox.UseVisualStyleBackColor = true;
			// 
			// generalTabPage
			// 
			this.generalTabPage.Controls.Add(this.associateCheckBox);
			this.generalTabPage.Controls.Add(this.fileLocationGroupBox);
			this.generalTabPage.Location = new System.Drawing.Point(4, 24);
			this.generalTabPage.Name = "generalTabPage";
			this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.generalTabPage.Size = new System.Drawing.Size(476, 122);
			this.generalTabPage.TabIndex = 0;
			this.generalTabPage.Text = "General";
			this.generalTabPage.UseVisualStyleBackColor = true;
			// 
			// associateCheckBox
			// 
			this.associateCheckBox.AutoSize = true;
			this.associateCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.associateCheckBox.Location = new System.Drawing.Point(6, 14);
			this.associateCheckBox.Name = "associateCheckBox";
			this.associateCheckBox.Size = new System.Drawing.Size(296, 18);
			this.associateCheckBox.TabIndex = 0;
			this.associateCheckBox.Text = "&Associate .ccl files with Chip\'s Challenge Level Designer";
			this.associateCheckBox.UseVisualStyleBackColor = true;
			this.associateCheckBox.CheckedChanged += new System.EventHandler(this.associateCheckBox_CheckedChanged);
			// 
			// fileLocationGroupBox
			// 
			this.fileLocationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fileLocationGroupBox.Controls.Add(this.label1);
			this.fileLocationGroupBox.Controls.Add(this.fileLocationTextBox);
			this.fileLocationGroupBox.Controls.Add(this.browseButton);
			this.fileLocationGroupBox.Location = new System.Drawing.Point(6, 43);
			this.fileLocationGroupBox.Name = "fileLocationGroupBox";
			this.fileLocationGroupBox.Size = new System.Drawing.Size(464, 73);
			this.fileLocationGroupBox.TabIndex = 1;
			this.fileLocationGroupBox.TabStop = false;
			this.fileLocationGroupBox.Text = "&Testing";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 15);
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
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 150);
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
			// OptionsDialog
			// 
			this.AcceptButton = this.okButton;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(484, 198);
			this.Controls.Add(this.tabControl);
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
			this.tabControl.ResumeLayout(false);
			this.chipsChallengeTabPage.ResumeLayout(false);
			this.chipsChallengeTabPage.PerformLayout();
			this.generalTabPage.ResumeLayout(false);
			this.generalTabPage.PerformLayout();
			this.fileLocationGroupBox.ResumeLayout(false);
			this.fileLocationGroupBox.PerformLayout();
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage generalTabPage;
		private System.Windows.Forms.GroupBox fileLocationGroupBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox fileLocationTextBox;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TabPage chipsChallengeTabPage;
		private System.Windows.Forms.CheckBox surroundLevelCheckBox;
		private System.Windows.Forms.CheckBox placeChipAtCenterCheckBox;
		private System.Windows.Forms.CheckBox automaticallyOpenNewLevelsCheckBox;
		private System.Windows.Forms.CheckBox associateCheckBox;
	}
}