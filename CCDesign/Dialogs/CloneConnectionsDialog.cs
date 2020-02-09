using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public partial class CloneConnectionsDialog : Form
	{
		private LevelMapEditor _owner;
		private bool _isChanged;

		public CloneConnectionsDialog(LevelMapEditor owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
			_owner = owner;
			_owner._cloneConnectionsDialog = this;
			_isChanged = owner.IsChanged;
			_owner.Level.PropertyChanged += new PropertyChangedEventHandler(Level_PropertyChanged);
			listBox.DataSource = _owner.Level.CloneConnections;
			_owner.Level.CloneConnections.ListChanged += new ListChangedEventHandler(listBox_SelectedIndexChanged);
			UpdateTitle();
		}

		private void AddCloneConnection()
		{
			var selectedIndex = listBox.SelectedIndex;
			var connection = new TileConnection(0, 0, 0, 0);
			if (selectedIndex > -1)
				connection = _owner.Level.CloneConnections[selectedIndex];
			using (var dialog = new AddConnectionDialog(connection))
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					_owner.AddCloneConnection(dialog.TileConnection);
					listBox.SelectedIndex = _owner.Level.CloneConnections.Count - 1;
					UpdateTitle();
				}
		}

		private void MoveCurrentCloneConnection(int offset)
		{
			_owner.MoveCloneConnection(listBox.SelectedIndex, listBox.SelectedIndex + offset);
		}

		private void RemoveCurrentCloneConnection()
		{
			var index = listBox.SelectedIndex;
			if (index > -1)
			{
				listBox.BeginUpdate();
				_owner.RemoveCloneConnectionAt(index);
				if (index < _owner.Level.CloneConnections.Count)
					listBox.SelectedIndex = index;
				else if (index > 0)
					listBox.SelectedIndex = index - 1;
				listBox.EndUpdate();
			}
			UpdateTitle();
		}

		private void UpdateButtonsEnabled()
		{
			addButton.Enabled = _owner.Level.CloneConnections.Count < 31;
			moveUpButton.Enabled = listBox.SelectedIndex > 0;
			moveDownButton.Enabled = listBox.SelectedIndex >= 0 && listBox.SelectedIndex < listBox.Items.Count - 1;
			removeButton.Enabled = listBox.SelectedIndex > -1;
		}

		private void UpdateTitle()
		{
			Text = string.Format(CultureInfo.CurrentCulture, "{0} - Clone Connections ({1} Total)", _owner.Level.Title, _owner.Level.CloneConnections.Count);
		}

		private void Level_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Title" || e.PropertyName == "CloneConnections")
				UpdateTitle();
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			AddCloneConnection();
		}

		private void listBox_Format(object sender, ListControlConvertEventArgs e)
		{
			var connection = (TileConnection)e.ListItem;
			e.Value = string.Format(CultureInfo.CurrentCulture, "{0} {1} - {2} {3}", connection.Source, TileUtilities.GetDescription(_owner.Level.UpperLayer[connection.Source]), connection.Destination, TileUtilities.GetDescription(_owner.Level.UpperLayer[connection.Destination]));
		}

		private void moveUpButton_Click(object sender, EventArgs e)
		{
			MoveCurrentCloneConnection(-1);
		}

		private void moveDownButton_Click(object sender, EventArgs e)
		{
			MoveCurrentCloneConnection(1);
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			RemoveCurrentCloneConnection();
		}

		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.N)
				AddCloneConnection();
			else if (e.KeyCode == Keys.Delete)
				RemoveCurrentCloneConnection();
		}

		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtonsEnabled();
			var index = listBox.SelectedIndex;
			_owner.CustomTileHighlights.Clear();
			if (index > -1)
			{
				var current = _owner.Level.CloneConnections[index];
				_owner.CustomTileHighlights.Add(TileConnectionMarker.FromCloneConnection(current));
			}
			_owner.Invalidate(true);
		}
	}
}
