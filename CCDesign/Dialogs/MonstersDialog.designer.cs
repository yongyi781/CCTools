﻿namespace CCTools.CCDesign
{
	partial class MonstersDialog
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
			if (disposing)
			{
				if (_owner.Level != null)
					_owner.Level.PropertyChanged -= Level_PropertyChanged;
				if (listBox != null)
					listBox.DataSource = null;
				if (_owner != null)
				{
					_owner.CustomTileHighlights.Clear();
					_owner.Invalidate(true);
					_owner._monstersDialog = null;
				}
				if (components != null)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonstersDialog));
			System.Windows.Forms.Button populateButton;
			this.listBox = new System.Windows.Forms.ListBox();
			this.addButton = new Button();
			this.moveUpButton = new Button();
			this.moveDownButton = new Button();
			this.removeButton = new Button();
			populateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox
			// 
			this.listBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.listBox.FormattingEnabled = true;
			this.listBox.Location = new System.Drawing.Point(12, 12);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(243, 199);
			this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
			this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
			this.listBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBox_Format);
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
			this.addButton.Location = new System.Drawing.Point(261, 12);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(24, 23);
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// moveUpButton
			// 
			this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.moveUpButton.Enabled = false;
			this.moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpButton.Image")));
			this.moveUpButton.Location = new System.Drawing.Point(261, 41);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(24, 23);
			this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
			// 
			// moveDownButton
			// 
			this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.moveDownButton.Enabled = false;
			this.moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownButton.Image")));
			this.moveDownButton.Location = new System.Drawing.Point(261, 70);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(24, 23);
			this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.removeButton.Enabled = false;
			this.removeButton.Image = ((System.Drawing.Image)(resources.GetObject("removeButton.Image")));
			this.removeButton.Location = new System.Drawing.Point(261, 99);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(24, 23);
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// populateButton
			// 
			populateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			populateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			populateButton.Location = new System.Drawing.Point(12, 217);
			populateButton.Name = "populateButton";
			populateButton.Text = "Populate";
			populateButton.Click += new System.EventHandler(this.populateButton_Click);
			// 
			// MonstersDialog
			// 
			this.ClientSize = new System.Drawing.Size(297, 252);
			this.Controls.Add(this.listBox);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.moveUpButton);
			this.Controls.Add(this.moveDownButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(populateButton);
			this.Font = System.Drawing.SystemFonts.MessageBoxFont;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MonstersDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Monsters";
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListBox listBox;
		private Button addButton;
		private Button moveUpButton;
		private Button moveDownButton;
		private Button removeButton;
	}
}