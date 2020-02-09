using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public partial class MonstersDialog : Form
	{
		private LevelMapEditor _owner;
		private bool _isChanged;

		public MonstersDialog(LevelMapEditor owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			InitializeComponent();
			Font = SystemFonts.MessageBoxFont;
			_owner = owner;
			owner._monstersDialog = this;
			_isChanged = owner.IsChanged;
			owner.Level.PropertyChanged += new PropertyChangedEventHandler(Level_PropertyChanged);
			listBox.DataSource = owner.Level.MonsterLocations;
			owner.Level.MonsterLocations.ListChanged += new ListChangedEventHandler(listBox_SelectedIndexChanged);
			UpdateTitle();
		}

		private void AddMonster()
		{
			var selectedIndex = listBox.SelectedIndex;
			var location = new TileLocation(0, 0);
			if (selectedIndex > -1)
				location = _owner.Level.MonsterLocations[selectedIndex];
			using (var dialog = new AddMonsterDialog(location))
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					_owner.AddMonster(dialog.TileLocation);
					listBox.SelectedIndex = _owner.Level.MonsterLocations.Count - 1;
					UpdateTitle();
				}
		}

		private void MoveCurrentMonster(int offset)
		{
			_owner.Level.MonsterLocations.Move(listBox.SelectedIndex, listBox.SelectedIndex + offset);
		}

		private void RemoveCurrentMonster()
		{
			var index = listBox.SelectedIndex;
			if (index > -1)
			{
				listBox.BeginUpdate();
				_owner.RemoveMonsterAt(index);
				if (index < _owner.Level.MonsterLocations.Count)
					listBox.SelectedIndex = index;
				else if (index > 0)
					listBox.SelectedIndex = index - 1;
				listBox.EndUpdate();
			}
			UpdateTitle();
		}

		private void UpdateButtonsEnabled()
		{
			addButton.Enabled = _owner.Level.MonsterLocations.Count < 127;
			moveUpButton.Enabled = listBox.SelectedIndex > 0;
			moveDownButton.Enabled = listBox.SelectedIndex >= 0 && listBox.SelectedIndex < _owner.Level.MonsterLocations.Count - 1;
			removeButton.Enabled = listBox.SelectedIndex > -1;
		}

		private void UpdateTitle()
		{
			Text = string.Format(CultureInfo.CurrentCulture, "{0} - Monsters ({1} Total)", _owner.Level.Title, _owner.Level.MonsterLocations.Count);
		}

		private void Level_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Title" || e.PropertyName == "MonsterLocations")
				UpdateTitle();
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			AddMonster();
		}

		private void listBox_Format(object sender, ListControlConvertEventArgs e)
		{
			var location = (TileLocation)e.ListItem;
			e.Value = string.Format(CultureInfo.CurrentCulture, "{0} {1}", location, TileUtilities.GetDescription(_owner.Level.UpperLayer[location]));
		}

		private void moveUpButton_Click(object sender, EventArgs e)
		{
			MoveCurrentMonster(-1);
		}

		private void moveDownButton_Click(object sender, EventArgs e)
		{
			MoveCurrentMonster(1);
		}

		private void populateButton_Click(object sender, EventArgs e)
		{
			var location = TileLocation.Invalid;
			var index = listBox.SelectedIndex;
			if (listBox.SelectedIndex > -1)
				location = _owner.Level.MonsterLocations[index];
			_owner.PopulateMonsters();
			if (location != TileLocation.Invalid)
				listBox.SelectedIndex = _owner.Level.MonsterLocations.IndexOf(location);
			listBox.EndUpdate();
			UpdateTitle();
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			RemoveCurrentMonster();
		}

		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.N)
				AddMonster();
			else if (e.KeyCode == Keys.Delete)
				RemoveCurrentMonster();
		}

		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtonsEnabled();
			var index = listBox.SelectedIndex;
			_owner.CustomTileHighlights.Clear();
			if (index > -1)
			{
				var current = _owner.Level.MonsterLocations[index];
				_owner.CustomTileHighlights.Add(HighlightMarker.FromMonster(current));
			}
			_owner.Invalidate();
		}
	}
}
