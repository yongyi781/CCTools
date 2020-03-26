using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
    public partial class TrapConnectionsDialog : Form
    {
        private readonly LevelMapEditor _owner;

        public TrapConnectionsDialog(LevelMapEditor owner)
        {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            _owner = owner ?? throw new ArgumentNullException("owner");
            _owner._trapConnectionsDialog = this;
            owner.Level.PropertyChanged += new PropertyChangedEventHandler(Level_PropertyChanged);
            listBox.DataSource = owner.Level.TrapConnections;
            owner.Level.TrapConnections.ListChanged += new ListChangedEventHandler(listBox_SelectedIndexChanged);
            UpdateTitle();
        }

        private void AddTrapConnection()
        {
            var selectedIndex = listBox.SelectedIndex;
            var connection = new TileConnection(0, 0, 0, 0);
            if (selectedIndex > -1)
                connection = _owner.Level.TrapConnections[selectedIndex];
            using (var dialog = new AddConnectionDialog(connection))
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _owner.AddTrapConnection(dialog.TileConnection);
                    listBox.SelectedIndex = _owner.Level.TrapConnections.Count - 1;
                    UpdateTitle();
                }
        }

        private void MoveCurrentMonster(int offset)
        {
            _owner.MoveTrapConnection(listBox.SelectedIndex, listBox.SelectedIndex + offset);
        }

        private void RemoveCurrentMonster()
        {
            var index = listBox.SelectedIndex;
            if (index > -1)
            {
                listBox.BeginUpdate();
                _owner.RemoveTrapConnectionAt(index);
                if (index < _owner.Level.TrapConnections.Count)
                    listBox.SelectedIndex = index;
                else if (index > 0)
                    listBox.SelectedIndex = index - 1;
                listBox.EndUpdate();
            }
            UpdateTitle();
        }

        private void UpdateButtonsEnabled()
        {
            addButton.Enabled = _owner.Level.TrapConnections.Count < 25;
            moveUpButton.Enabled = listBox.SelectedIndex > 0;
            moveDownButton.Enabled = listBox.SelectedIndex >= 0 && listBox.SelectedIndex < listBox.Items.Count - 1;
            removeButton.Enabled = listBox.SelectedIndex > -1;
        }

        private void UpdateTitle()
        {
            Text = string.Format(CultureInfo.CurrentCulture, "{0} - Trap Connections ({1} Total)", _owner.Level.Title, _owner.Level.TrapConnections.Count);
        }

        private void Level_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title" || e.PropertyName == "TrapConnections")
                UpdateTitle();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddTrapConnection();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var connection = (TileConnection)e.ListItem;
            e.Value = string.Format(CultureInfo.CurrentCulture, "{0} {1} -> {2} {3}", connection.Source, TileUtilities.GetDescription(_owner.Level.UpperLayer[connection.Source]), connection.Destination, TileUtilities.GetDescription(_owner.Level.UpperLayer[connection.Destination]));
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            MoveCurrentMonster(-1);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            MoveCurrentMonster(1);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveCurrentMonster();
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
                AddTrapConnection();
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                if (_owner.CanUndo)
                    _owner.Undo();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                if (_owner.CanRedo)
                    _owner.Redo();
            }
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
                var current = _owner.Level.TrapConnections[index];
                _owner.CustomTileHighlights.Add(HighlightMarker.FromTrapConnection(current));
            }
            _owner.Invalidate(true);
        }
    }
}
