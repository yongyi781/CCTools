using System;
using System.Windows.Forms;
using System.Drawing;

namespace CCTools.CCDesign
{
	public partial class OptionsDialog : Form
	{
		public OptionsDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
				fileLocationTextBox.Text = openFileDialog.FileName;
		}

		private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				Properties.Settings.Default.Save();
			else
				Properties.Settings.Default.Reload();
		}
	}
}
