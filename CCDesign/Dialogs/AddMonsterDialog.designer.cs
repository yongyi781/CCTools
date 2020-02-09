namespace CCTools.CCDesign
{
	partial class AddMonsterDialog
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
			System.Windows.Forms.Label xLabel;
			System.Windows.Forms.Label yLabel;
			System.Windows.Forms.Button okButton;
			System.Windows.Forms.Button cancelButton;
			this.xUpDown = new System.Windows.Forms.NumericUpDown();
			this.yUpDown = new System.Windows.Forms.NumericUpDown();
			xLabel = new System.Windows.Forms.Label();
			yLabel = new System.Windows.Forms.Label();
			okButton = new System.Windows.Forms.Button();
			cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.xUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// xLabel
			// 
			xLabel.AutoSize = true;
			xLabel.Location = new System.Drawing.Point(12, 14);
			xLabel.Name = "xLabel";
			xLabel.Text = "&X:";
			// 
			// xUpDown
			// 
			this.xUpDown.Location = new System.Drawing.Point(35, 12);
			this.xUpDown.Maximum = 255;
			this.xUpDown.Name = "xUpDown";
			this.xUpDown.Size = new System.Drawing.Size(40, 23);
			this.xUpDown.Enter += new System.EventHandler(this.xUpDown_Enter);
			// 
			// yLabel
			// 
			yLabel.AutoSize = true;
			yLabel.Location = new System.Drawing.Point(83, 14);
			yLabel.Name = "yLabel";
			yLabel.Text = "&Y:";
			// 
			// yUpDown
			// 
			this.yUpDown.Location = new System.Drawing.Point(106, 12);
			this.yUpDown.Maximum = 255;
			this.yUpDown.Name = "yUpDown";
			this.yUpDown.Size = new System.Drawing.Size(40, 23);
			this.yUpDown.Enter += new System.EventHandler(this.yUpDown_Enter);
			// 
			// okButton
			// 
			okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			okButton.Location = new System.Drawing.Point(12, 49);
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(68, 23);
			okButton.Text = "OK";
			// 
			// cancelButton
			// 
			cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			cancelButton.Location = new System.Drawing.Point(86, 49);
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(68, 23);
			cancelButton.Text = "Cancel";
			// 
			// AddMonsterDialog
			// 
			this.AcceptButton = okButton;
			this.CancelButton = cancelButton;
			this.ClientSize = new System.Drawing.Size(166, 84);
			this.ControlBox = false;
			this.Controls.Add(xLabel);
			this.Controls.Add(this.xUpDown);
			this.Controls.Add(yLabel);
			this.Controls.Add(this.yUpDown);
			this.Controls.Add(okButton);
			this.Controls.Add(cancelButton);
			this.Font = System.Drawing.SystemFonts.MessageBoxFont;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AddMonsterDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Monster";
			((System.ComponentModel.ISupportInitialize)(this.xUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown xUpDown;
		private System.Windows.Forms.NumericUpDown yUpDown;
	}
}