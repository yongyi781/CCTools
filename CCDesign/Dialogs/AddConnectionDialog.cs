using System;
using System.Windows.Forms;
using System.Drawing;

namespace CCTools.CCDesign
{
	public partial class AddConnectionDialog : Form
	{
		public AddConnectionDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
		}

		public AddConnectionDialog(TileConnection initialConnection)
			: this()
		{
			sourceXUpDown.Value = initialConnection.Source.X;
			sourceYUpDown.Value = initialConnection.Source.Y;
			destinationXUpDown.Value = initialConnection.Destination.X;
			destinationYUpDown.Value = initialConnection.Destination.Y;
		}

		public TileConnection TileConnection
		{
			get { return new TileConnection((int)sourceXUpDown.Value, (int)sourceYUpDown.Value, (int)destinationXUpDown.Value, (int)destinationYUpDown.Value); }
		}

		private void sourceXUpDown_Enter(object sender, EventArgs e)
		{
			sourceXUpDown.Select(0, sourceXUpDown.Text.Length);
		}

		private void sourceYUpDown_Enter(object sender, EventArgs e)
		{
			sourceYUpDown.Select(0, sourceYUpDown.Text.Length);
		}

		private void destinationXUpDown_Enter(object sender, EventArgs e)
		{
			destinationXUpDown.Select(0, destinationXUpDown.Text.Length);
		}

		private void destinationYUpDown_Enter(object sender, EventArgs e)
		{
			destinationYUpDown.Select(0, destinationYUpDown.Text.Length);
		}
	}
}
