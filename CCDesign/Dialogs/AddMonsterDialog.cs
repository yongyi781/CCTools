using System;
using System.Windows.Forms;
using System.Drawing;

namespace CCTools.CCDesign
{
	// TODO: Update add monster
	public partial class AddMonsterDialog : Form
	{
		public AddMonsterDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		public AddMonsterDialog(TileLocation initialLocation)
			: this()
		{
			xUpDown.Value = initialLocation.X;
			yUpDown.Value = initialLocation.Y;
		}

		public TileLocation TileLocation
		{
			get { return new TileLocation((int)xUpDown.Value, (int)yUpDown.Value); }
		}

		private void xUpDown_Enter(object sender, EventArgs e)
		{
			xUpDown.Select(0, xUpDown.Text.Length);
		}

		private void yUpDown_Enter(object sender, EventArgs e)
		{
			yUpDown.Select(0, yUpDown.Text.Length);
		}
	}
}
