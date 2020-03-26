using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
    public class LevelEditorTabPage : TabPage
    {
        #region Fields

        private readonly Form1 _owner;
        internal Level _level;
        internal LevelMapEditor _editor;

        #endregion

        #region Constructor

        public LevelEditorTabPage(Form1 owner, Level level)
            : base()
        {
            _owner = owner;
            _level = level ?? throw new ArgumentNullException("level");
            level.PropertyChanged += new PropertyChangedEventHandler(Level_PropertyChanged);
            _editor = new LevelMapEditor(owner, _level);
            _editor.MouseDown += new MouseEventHandler(Editor_MouseDown);
            _editor.MouseEnter += new EventHandler(Editor_MouseEnter);
            AutoScroll = true;
            Controls.Add(_editor);
            Name = Text = level.ToString();
        }

        #endregion

        #region Properties

        public bool CanUndo
        {
            get { return _editor.CanUndo; }
        }

        public bool CanRedo
        {
            get { return _editor.CanRedo; }
        }

        public string UndoName
        {
            get { return _editor.UndoName; }
        }

        public string RedoName
        {
            get { return _editor.RedoName; }
        }

        public bool CanCopy
        {
            get { return _editor.CanCopy; }
        }

        public bool IsChanged
        {
            get { return _owner.IsChanged; }
            set { _owner.IsChanged = value; }
        }

        public bool IsSelecting
        {
            get { return _editor.IsSelecting; }
        }

        public bool IsSelectionEmpty
        {
            get { return _editor.Selection.IsEmpty; }
            set { if (value) _editor.Selection = TileRectangle.Empty; }
        }

        public LayerMode LayerMode
        {
            get { return _owner.LayerMode; }
        }

        public Tile LeftTile
        {
            get { return _owner.LeftTile; }
            set { _owner.LeftTile = value; }
        }

        public Tile RightTile
        {
            get { return _owner.RightTile; }
            set { _owner.RightTile = value; }
        }

        #endregion

        #region Methods

        public void ClearIntermediateConnections()
        {
            _editor.ClearIntermediateConnections();
        }

        public void Close()
        {
            Dispose();
            if (_owner != null)
                _owner.UpdateRightTabControlVisible();
        }

        public void Cut()
        {
            _editor.Cut();
            Focus();
        }

        public void Copy()
        {
            _editor.Copy();
            Focus();
        }

        public void FillSelection(Tile leftTile, Tile rightTile)
        {
            _editor.FillSelection(leftTile, rightTile);
            Focus();
        }

        public void DeleteSelection()
        {
            _editor.DeleteSelection();
            Focus();
        }

        public void Paste()
        {
            _editor.Paste();
            Focus();
        }

        public void ReplaceAll(Tile oldTile, Tile newTile)
        {
            _editor.ReplaceAll(oldTile, newTile);
            Focus();
        }

        public void SelectAll()
        {
            _editor.SelectAll();
            Focus();
        }

        public void ShowCloneConnectionsDialog()
        {
            _editor.ShowCloneConnectionsDialog();
        }

        public void ShowTrapConnectionsDialog()
        {
            _editor.ShowTrapConnectionsDialog();
        }

        public void ShowMonstersDialog()
        {
            _editor.ShowMonstersDialog();
        }

        public void SwitchToggles()
        {
            _editor.SwitchToggles();
            Focus();
        }

        public void Undo()
        {
            _editor.Undo();
            Focus();
        }

        public void Redo()
        {
            _editor.Redo();
            Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Focus();
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEHWHEEL)
                AutoScrollPosition = new Point(-AutoScrollPosition.X + (m.WParam.ToInt32() > 0 ? 27 : -27), -AutoScrollPosition.Y);
            else
                base.WndProc(ref m);
        }

        internal void UpdateTileSize()
        {
            _editor.UpdateTileSize();
        }

        private void Level_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
                Name = Text = _level.ToString();
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        private void Editor_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }

        #endregion
    }
}
