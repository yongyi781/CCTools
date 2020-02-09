namespace CCTools.CCDesign
{
	partial class AddConnectionDialog
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
			System.Windows.Forms.Label sourceLabel;
			System.Windows.Forms.Label sourceXLabel;
			System.Windows.Forms.Label sourceYLabel;
			System.Windows.Forms.Label destinationLabel;
			System.Windows.Forms.Label destinationXLabel;
			System.Windows.Forms.Label destinationYLabel;
			System.Windows.Forms.Button okButton;
			System.Windows.Forms.Button cancelButton;
			sourceLabel = new System.Windows.Forms.Label();
			sourceXLabel = new System.Windows.Forms.Label();
			this.sourceXUpDown = new System.Windows.Forms.NumericUpDown();
			sourceYLabel = new System.Windows.Forms.Label();
			this.sourceYUpDown = new System.Windows.Forms.NumericUpDown();
			destinationLabel = new System.Windows.Forms.Label();
			destinationXLabel = new System.Windows.Forms.Label();
			this.destinationXUpDown = new System.Windows.Forms.NumericUpDown();
			destinationYLabel = new System.Windows.Forms.Label();
			this.destinationYUpDown = new System.Windows.Forms.NumericUpDown();
			okButton = new System.Windows.Forms.Button();
			cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.sourceXUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceYUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.destinationXUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.destinationYUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// sourceLabel
			// 
			sourceLabel.AutoSize = true;
			sourceLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			sourceLabel.Location = new System.Drawing.Point(12, 14);
			sourceLabel.Name = "sourceLabel";
			sourceLabel.Text = "&Source";
			// 
			// sourceXLabel
			// 
			sourceXLabel.AutoSize = true;
			sourceXLabel.Location = new System.Drawing.Point(85, 14);
			sourceXLabel.Name = "sourceXLabel";
			sourceXLabel.Text = "X:";
			// 
			// sourceXUpDown
			// 
			this.sourceXUpDown.Location = new System.Drawing.Point(108, 12);
			this.sourceXUpDown.Maximum = 255;
			this.sourceXUpDown.Name = "sourceXUpDown";
			this.sourceXUpDown.Size = new System.Drawing.Size(40, 23);
			this.sourceXUpDown.Enter += new System.EventHandler(this.sourceXUpDown_Enter);
			// 
			// sourceYLabel
			// 
			sourceYLabel.AutoSize = true;
			sourceYLabel.Location = new System.Drawing.Point(156, 14);
			sourceYLabel.Name = "sourceYLabel";
			sourceYLabel.Text = "Y:";
			// 
			// sourceYUpDown
			// 
			this.sourceYUpDown.Location = new System.Drawing.Point(179, 12);
			this.sourceYUpDown.Maximum = 255;
			this.sourceYUpDown.Name = "sourceYUpDown";
			this.sourceYUpDown.Size = new System.Drawing.Size(40, 23);
			this.sourceYUpDown.Enter += new System.EventHandler(this.sourceYUpDown_Enter);
			// 
			// destinationLabel
			// 
			destinationLabel.AutoSize = true;
			destinationLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			destinationLabel.Location = new System.Drawing.Point(12, 43);
			destinationLabel.Name = "destinationLabel";
			destinationLabel.Text = "&Destination";
			// 
			// destinationXLabel
			// 
			destinationXLabel.AutoSize = true;
			destinationXLabel.Location = new System.Drawing.Point(85, 43);
			destinationXLabel.Name = "destinationXLabel";
			destinationXLabel.Text = "X:";
			// 
			// destinationXUpDown
			// 
			this.destinationXUpDown.Location = new System.Drawing.Point(108, 41);
			this.destinationXUpDown.Maximum = 255;
			this.destinationXUpDown.Name = "destinationXUpDown";
			this.destinationXUpDown.Size = new System.Drawing.Size(40, 23);
			this.destinationXUpDown.Enter += new System.EventHandler(this.destinationXUpDown_Enter);
			// 
			// destinationYLabel
			// 
			destinationYLabel.AutoSize = true;
			destinationYLabel.Location = new System.Drawing.Point(156, 43);
			destinationYLabel.Name = "destinationYLabel";
			destinationYLabel.Text = "Y:";
			// 
			// destinationYUpDown
			// 
			this.destinationYUpDown.Location = new System.Drawing.Point(179, 41);
			this.destinationYUpDown.Maximum = 255;
			this.destinationYUpDown.Name = "destinationYUpDown";
			this.destinationYUpDown.Size = new System.Drawing.Size(40, 23);
			this.destinationYUpDown.Enter += new System.EventHandler(this.destinationYUpDown_Enter);
			// 
			// okButton
			// 
			okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			okButton.Location = new System.Drawing.Point(85, 70);
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(68, 23);
			okButton.Text = "OK";
			// 
			// cancelButton
			// 
			okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			cancelButton.Location = new System.Drawing.Point(159, 70);
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(68, 23);
			cancelButton.Text = "Cancel";
			// 
			// AddConnectionDialog
			// 
			this.AcceptButton = okButton;
			this.CancelButton = cancelButton;
			this.ClientSize = new System.Drawing.Size(239, 105);
			this.ControlBox = false;
			this.Controls.Add(sourceLabel);
			this.Controls.Add(sourceXLabel);
			this.Controls.Add(this.sourceXUpDown);
			this.Controls.Add(sourceYLabel);
			this.Controls.Add(this.sourceYUpDown);
			this.Controls.Add(destinationLabel);
			this.Controls.Add(destinationXLabel);
			this.Controls.Add(this.destinationXUpDown);
			this.Controls.Add(destinationYLabel);
			this.Controls.Add(this.destinationYUpDown);
			this.Controls.Add(okButton);
			this.Controls.Add(cancelButton);
			this.Font = System.Drawing.SystemFonts.MessageBoxFont;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AddConnectionDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Connection";
			((System.ComponentModel.ISupportInitialize)(this.sourceXUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceYUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.destinationXUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.destinationYUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.NumericUpDown sourceXUpDown;
		private System.Windows.Forms.NumericUpDown sourceYUpDown;
		private System.Windows.Forms.NumericUpDown destinationXUpDown;
		private System.Windows.Forms.NumericUpDown destinationYUpDown;
	}
}