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
            this.sourceLabel = new System.Windows.Forms.Label();
            this.sourceXLabel = new System.Windows.Forms.Label();
            this.sourceYLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.destinationXLabel = new System.Windows.Forms.Label();
            this.destinationYLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sourceXUpDown = new System.Windows.Forms.NumericUpDown();
            this.sourceYUpDown = new System.Windows.Forms.NumericUpDown();
            this.destinationXUpDown = new System.Windows.Forms.NumericUpDown();
            this.destinationYUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.sourceXUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceYUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationXUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationYUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.sourceLabel.Location = new System.Drawing.Point(12, 14);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(43, 15);
            this.sourceLabel.TabIndex = 0;
            this.sourceLabel.Text = "&Source";
            // 
            // sourceXLabel
            // 
            this.sourceXLabel.AutoSize = true;
            this.sourceXLabel.Location = new System.Drawing.Point(85, 14);
            this.sourceXLabel.Name = "sourceXLabel";
            this.sourceXLabel.Size = new System.Drawing.Size(17, 15);
            this.sourceXLabel.TabIndex = 1;
            this.sourceXLabel.Text = "X:";
            // 
            // sourceYLabel
            // 
            this.sourceYLabel.AutoSize = true;
            this.sourceYLabel.Location = new System.Drawing.Point(156, 14);
            this.sourceYLabel.Name = "sourceYLabel";
            this.sourceYLabel.Size = new System.Drawing.Size(17, 15);
            this.sourceYLabel.TabIndex = 3;
            this.sourceYLabel.Text = "Y:";
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.destinationLabel.Location = new System.Drawing.Point(12, 43);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(67, 15);
            this.destinationLabel.TabIndex = 5;
            this.destinationLabel.Text = "&Destination";
            // 
            // destinationXLabel
            // 
            this.destinationXLabel.AutoSize = true;
            this.destinationXLabel.Location = new System.Drawing.Point(85, 43);
            this.destinationXLabel.Name = "destinationXLabel";
            this.destinationXLabel.Size = new System.Drawing.Size(17, 15);
            this.destinationXLabel.TabIndex = 6;
            this.destinationXLabel.Text = "X:";
            // 
            // destinationYLabel
            // 
            this.destinationYLabel.AutoSize = true;
            this.destinationYLabel.Location = new System.Drawing.Point(156, 43);
            this.destinationYLabel.Name = "destinationYLabel";
            this.destinationYLabel.Size = new System.Drawing.Size(17, 15);
            this.destinationYLabel.TabIndex = 8;
            this.destinationYLabel.Text = "Y:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(85, 70);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(68, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(159, 70);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(68, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            // 
            // sourceXUpDown
            // 
            this.sourceXUpDown.Location = new System.Drawing.Point(108, 12);
            this.sourceXUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.sourceXUpDown.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.sourceXUpDown.Name = "sourceXUpDown";
            this.sourceXUpDown.Size = new System.Drawing.Size(40, 23);
            this.sourceXUpDown.TabIndex = 2;
            this.sourceXUpDown.Enter += new System.EventHandler(this.sourceXUpDown_Enter);
            // 
            // sourceYUpDown
            // 
            this.sourceYUpDown.Location = new System.Drawing.Point(179, 12);
            this.sourceYUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.sourceYUpDown.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.sourceYUpDown.Name = "sourceYUpDown";
            this.sourceYUpDown.Size = new System.Drawing.Size(40, 23);
            this.sourceYUpDown.TabIndex = 4;
            this.sourceYUpDown.Enter += new System.EventHandler(this.sourceYUpDown_Enter);
            // 
            // destinationXUpDown
            // 
            this.destinationXUpDown.Location = new System.Drawing.Point(108, 41);
            this.destinationXUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.destinationXUpDown.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.destinationXUpDown.Name = "destinationXUpDown";
            this.destinationXUpDown.Size = new System.Drawing.Size(40, 23);
            this.destinationXUpDown.TabIndex = 7;
            this.destinationXUpDown.Enter += new System.EventHandler(this.destinationXUpDown_Enter);
            // 
            // destinationYUpDown
            // 
            this.destinationYUpDown.Location = new System.Drawing.Point(179, 41);
            this.destinationYUpDown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.destinationYUpDown.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.destinationYUpDown.Name = "destinationYUpDown";
            this.destinationYUpDown.Size = new System.Drawing.Size(40, 23);
            this.destinationYUpDown.TabIndex = 9;
            this.destinationYUpDown.Enter += new System.EventHandler(this.destinationYUpDown_Enter);
            // 
            // AddConnectionDialog
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(239, 105);
            this.ControlBox = false;
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.sourceXLabel);
            this.Controls.Add(this.sourceXUpDown);
            this.Controls.Add(this.sourceYLabel);
            this.Controls.Add(this.sourceYUpDown);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.destinationXLabel);
            this.Controls.Add(this.destinationXUpDown);
            this.Controls.Add(this.destinationYLabel);
            this.Controls.Add(this.destinationYUpDown);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
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
		private System.Windows.Forms.Label sourceLabel;
		private System.Windows.Forms.Label sourceXLabel;
		private System.Windows.Forms.Label sourceYLabel;
		private System.Windows.Forms.Label destinationLabel;
		private System.Windows.Forms.Label destinationXLabel;
		private System.Windows.Forms.Label destinationYLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}