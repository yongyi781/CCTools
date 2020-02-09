using System;
using System.Windows.Forms;
using System.Drawing;

namespace CCTools.CCDesign
{
	public partial class ChipsLocationForm : Form
	{
		public ChipsLocationForm()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
			textBox.Text = Properties.Settings.Default.ChipsChallengeLocation;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.ChipsChallengeLocation = textBox.Text;
			Properties.Settings.Default.Save();
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
				textBox.Text = openFileDialog.FileName;
		}
	}
}
