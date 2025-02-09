﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Reflection;

namespace CCTools.CCDesign
{
    public partial class Form1 : Form
    {
        #region Fields

        private NativeMethods.TIMERPROC _timerProc;
        private readonly LevelSet _levelSet = new LevelSet();
        private string _fileName;

        #endregion

        #region Constructors

        public Form1()
        {
            Initialize();
            var fileName = Properties.Settings.Default.LastOpenedFilePath;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                OpenLevelSet(fileName);
            else
                NewLevelSet();
        }

        public Form1(string fileName)
        {
            Initialize();
            OpenLevelSet(fileName);
        }

        #endregion

        #region Properties

        public string CoordinatesStatusText
        {
            get { return coordinatesStatusLabel.Text; }
            set { coordinatesStatusLabel.Text = value; }
        }

        public string ItemDescriptionStatusText
        {
            get { return itemDescriptionStatusLabel.Text; }
            set { itemDescriptionStatusLabel.Text = value; }
        }

        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                if (isChanged != value)
                {
                    isChanged = value;
                    UpdateTitle();
                }
            }
        }

        public LayerMode LayerMode
        {
            get { return (LayerMode)layerToolStripComboBox.SelectedIndex + 1; }
        }

        private Tile leftTile = Tile.Wall;
        public Tile LeftTile
        {
            get { return leftTile; }
            set
            {
                if (leftTile != value)
                {
                    leftTile = value;
                    leftPictureBox.Image = tileset.GetBitmap(value);
                }
            }
        }

        private Tile rightTile;
        public Tile RightTile
        {
            get { return rightTile; }
            set
            {
                if (rightTile != value)
                {
                    rightTile = value;
                    rightPictureBox.Image = tileset.GetBitmap(value);
                }
            }
        }

        private EditorTool editorTool = EditorTool.Default;
        public EditorTool EditorTool
        {
            get { return editorTool; }
            set
            {
                if (editorTool != value)
                {
                    if (editorTool == EditorTool.Select)
                        foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                            tabPage.IsSelectionEmpty = true;
                    editorTool = value;
                }
            }
        }

        private Tileset tileset = Tileset.Default;
        public Tileset Tileset
        {
            get { return tileset; }
        }

        private int tileSize = 32;
        public int TileSize
        {
            get { return tileSize; }
            set
            {
                if (tileSize != value)
                {
                    tileSize = value;
                    foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                        tabPage.UpdateTileSize();
                }
            }
        }

        #endregion

        #region Methods

        public void MoveCurrentLevel(int offset)
        {
            // Bug workaround
            levelExplorerListBox.Focus();
            _levelSet.Move(levelExplorerListBox.SelectedIndex, levelExplorerListBox.SelectedIndex + offset);
            foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                tabPage.Name = tabPage.Text = tabPage._level.ToString();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Properties.Settings.Default.LastOpenedFilePath = _fileName;
            Properties.Settings.Default.Save();
            base.OnFormClosed(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            e.Cancel = !ConfirmClose();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (rightTabControl.Created)
                UpdatePasteEnabled(rightTabControl.SelectedTab as LevelEditorTabPage);
            NativeMethods.SetTimer(Handle, IntPtr.Zero, 200, _timerProc);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                EditorTool = EditorTool.Default;
                selectMenuItem.Checked = selectToolStripButton.Checked = pathMakerMenuItem.Checked = pathMakerToolStripButton.Checked = trapConnectorMenuItem.Checked = trapConnectorToolStripButton.Checked = cloneConnectorMenuItem.Checked = cloneConnectorToolStripButton.Checked = false;
                RefreshSelectedTabFromTool();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (rightTabControl.TabPages.Count > 0 && fitToScreenMenuItem.Checked)
                FitTileSizeToScreen(rightTabControl.SelectedTab);
        }

        internal void UpdateCutCopyPasteFillEnabled(LevelEditorTabPage tabPage)
        {
            if (tabPage != null)
            {
                cutToolStripButton.Enabled = copyToolStripButton.Enabled = fillToolStripButton.Enabled = editorTool == EditorTool.Select && !tabPage.IsSelectionEmpty;
                pasteToolStripButton.Enabled = LevelMapEditor.CanPaste;
            }
            else
                cutToolStripButton.Enabled = copyToolStripButton.Enabled = pasteToolStripButton.Enabled = fillToolStripButton.Enabled = false;
        }

        internal void UpdateCutCopyPasteFillEnabled(LevelMapEditor editor)
        {
            if (editor != null)
            {
                cutToolStripButton.Enabled = copyToolStripButton.Enabled = fillToolStripButton.Enabled = editorTool == EditorTool.Select && !editor.Selection.IsEmpty;
                pasteToolStripButton.Enabled = LevelMapEditor.CanPaste;
            }
            else
                cutToolStripButton.Enabled = copyToolStripButton.Enabled = pasteToolStripButton.Enabled = fillToolStripButton.Enabled = false;
        }

        internal void UpdatePasteEnabled(LevelEditorTabPage tabPage)
        {
            pasteToolStripButton.Enabled = tabPage != null && LevelMapEditor.CanPaste;
        }

        internal void UpdateRightTabControlVisible()
        {
            if (rightTabControl.TabCount > 0)
            {
                rightTabControl.Visible = true;
                closeTabButton.Visible = true;
                splitContainer.Panel2.BackColor = SystemColors.Control;
            }
            else
            {
                rightTabControl.Visible = false;
                closeTabButton.Visible = false;
                splitContainer.Panel2.BackColor = SystemColors.ControlDark;
            }
        }

        private void CloseLevelSet()
        {
            rightTabControl.TabPages.Clear();
            UpdateRightTabControlVisible();
            _levelSet.Clear();
            _fileName = string.Empty;
        }

        private void CloseSelectedTab()
        {
            var selectedIndex = rightTabControl.SelectedIndex;
            var nextTab = selectedIndex - 1;
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
            {
                selectedTab.Close();
                if (nextTab > -1)
                    rightTabControl.SelectTab(nextTab);
            }
        }

        private bool ConfirmClose()
        {
            if (!isChanged)
                return true;
            DialogResult result;
            if (NativeMethods.IsVista)
            {
                var td = new TaskDialog
                {
                    WindowTitle = Properties.Resources.ProductName,
                    MainInstruction = string.Format(CultureInfo.CurrentCulture, "Do you want to save changes to {0}?", string.IsNullOrEmpty(_fileName) ? Properties.Resources.Untitled : _fileName),
                    CommonButtons = TaskDialogStandardButtons.Cancel
                };
                var save = new TaskDialogButton(DialogResult.Yes, Properties.Resources.Save);
                var dontSave = new TaskDialogButton(DialogResult.No, Properties.Resources.DontSave);
                td.Buttons.Add(save);
                td.Buttons.Add(dontSave);

                result = td.ShowDialog();
            }
            else
                result = MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "Do you want to save changes to {0}?", string.IsNullOrEmpty(_fileName) ? Properties.Resources.Untitled : _fileName), ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, 0);
            if (result == DialogResult.Yes)
                return SaveLevelSet();
            if (result == DialogResult.Cancel)
                return false;
            return true;
        }

        private void CopyCurrentLevel()
        {
            var level = _levelSet[levelExplorerListBox.SelectedIndex];
            if (NativeMethods.OpenClipboard(new HandleRef(this, Handle)))
                try
                {
                    NativeMethods.EmptyClipboard();

                    unsafe
                    {
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            using (var writer = new BinaryWriter(ms))
                                level.Write(writer, levelExplorerListBox.SelectedIndex);
                            bytes = ms.GetBuffer();
                        }
                        var hMem = NativeMethods.GlobalAlloc(NativeMethods.GMEM_MOVEABLE, new IntPtr(bytes.Length));
                        if (hMem != IntPtr.Zero)
                        {
                            var ptr = NativeMethods.GlobalLock(hMem);
                            if (ptr != null)
                                try { NativeMethods.RtlMoveMemory(ptr, bytes, new IntPtr(bytes.Length)); }
                                finally { NativeMethods.GlobalUnlock(hMem); }
                            NativeMethods.SetClipboardData(NativeMethods.RegisterClipboardFormat(Level.LevelDataFormat), hMem);
                        }
                    }
                }
                finally { NativeMethods.CloseClipboard(); }
        }

        private void FitTileSizeToScreen()
        {
            if (rightTabControl.TabPages.Count > 0)
                TileSize = Math.Min(rightTabControl.SelectedTab.Width, rightTabControl.SelectedTab.Height) / 32;
            else
                TileSize = Math.Min(rightTabControl.Width, rightTabControl.Height) / 32;
        }

        private void FitTileSizeToScreen(TabPage tabPage)
        {
            if (tabPage != null)
                TileSize = Math.Min(tabPage.Width, tabPage.Height) / 32;
        }

        private Control GetRealActiveControl()
        {
            var activeControl = ActiveControl;
            var container = activeControl as ContainerControl;
            while (container != null)
            {
                activeControl = container.ActiveControl;
                container = activeControl as ContainerControl;
            }
            return activeControl;
        }

        private void Initialize()
        {
            _timerProc = TimerProc;
            InitializeComponent();
            InitializeToolboxItems();

            if (!File.Exists(Properties.Settings.Default.ChipsChallengeLocation))
                new ChipsLocationForm().ShowDialog();

            levelExplorerListBox.DataSource = _levelSet;
            _levelSet.ListChanged += new ListChangedEventHandler(Levels_ListChanged);
            titleTextBox.DataBindings.Add("Text", _levelSet, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
            passwordTextBox.DataBindings.Add("Text", _levelSet, "Password", true, DataSourceUpdateMode.OnPropertyChanged);
            chipsUpDown.DataBindings.Add("Value", _levelSet, "ChipCount", true, DataSourceUpdateMode.OnPropertyChanged);
            timeLimitUpDown.DataBindings.Add("Value", _levelSet, "TimeLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            hintTextBox.DataBindings.Add("Text", _levelSet, "Hint", true, DataSourceUpdateMode.OnPropertyChanged);
            layerToolStripComboBox.SelectedIndex = 2;

            UpdateTitle();
        }

        private void InitializeToolboxItems()
        {
            leftPictureBox.Image = tileset.GetBitmap(leftTile);
            rightPictureBox.Image = tileset.GetBitmap(rightTile);
        }

        private void InsertNewLevel(int index)
        {
            _levelSet.Insert(index, CreateNewLevel(index));
            foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                tabPage.Name = tabPage.Text = tabPage._level.ToString();
        }

        private void InsertLevel(int index, Level level)
        {
            _levelSet.Insert(index, level);
            foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                tabPage.Name = tabPage.Text = tabPage._level.ToString();
        }

        private Level CreateNewLevel(int index)
        {
            var level = new Level
            {
                Title = string.Format(CultureInfo.CurrentCulture, "Level {0}", index + 1),
                Password = Level.GenerateRandomPassword(4)
            };
            if (Properties.Settings.Default.SurroundNewLevelsWithWalls)
                level.SurroundWithWalls();
            if (Properties.Settings.Default.PlaceChipAtCenter)
                level.UpperLayer[15, 15] = Tile.ChipS;
            return level;
        }

        private void NewLevelSet()
        {
            if (!ConfirmClose())
                return;
            CloseLevelSet();
            var level = CreateNewLevel(0);
            _levelSet.Add(level);
            UpdateTitle();
            rightTabControl.TabPages.Add(new LevelEditorTabPage(this, level));
            UpdateRightTabControlVisible();
            UpdateLevelMenuItemsEnabled();
            IsChanged = false;
        }

        private void OpenLevel(int index)
        {
            var currentLevel = _levelSet[index];
            if (currentLevel == null)
                return;

            var tabPageName = currentLevel.ToString();
            if (rightTabControl.TabPages.ContainsKey(tabPageName))
                rightTabControl.SelectTab(tabPageName);
            else
            {
                if (!IsChanged && rightTabControl.TabCount > 0)
                    rightTabControl.TabPages.Remove(rightTabControl.SelectedTab);

                var tabPage = new LevelEditorTabPage(this, currentLevel);
                var indexes = new int[rightTabControl.TabPages.Count];
                for (int i = 0; i < rightTabControl.TabPages.Count; i++)
                {
                    if (rightTabControl.TabPages[i] is LevelEditorTabPage page)
                        indexes[i] = page._level.Index;
                }
                var j = Array.BinarySearch(indexes, currentLevel.Index);
                if (j < 0)
                    j = ~j;
                rightTabControl.Visible = true;
                closeTabButton.Visible = true;
                splitContainer.Panel2.BackColor = SystemColors.Control;
                rightTabControl.TabPages.Insert(j, tabPage);
                rightTabControl.SelectTab(j);
                levelExplorerListBox.Focus();
                if (fitToScreenMenuItem.Checked && rightTabControl.TabCount == 1)
                    FitTileSizeToScreen();
            }
            UpdateLevelMenuItemsEnabled();
        }

        private void OpenLevelSet()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!ConfirmClose())
                    return;
                rightTabControl.TabPages.Clear();
                UpdateRightTabControlVisible();
                OpenLevelSet(openFileDialog.FileName);
            }
        }

        private void OpenLevelSet(string fileName)
        {
            try
            {
                _fileName = fileName;
                _levelSet.Load(_fileName);
                leftTabControl.SelectTab(1);
                IsChanged = false;
            }
            catch (FileNotFoundException ex) { ReportError(ex); }
        }

        private void PasteLevel()
        {
            var index = levelExplorerListBox.SelectedIndex + 1;
            if (index <= 0)
                index = levelExplorerListBox.Items.Count;
            var level = new Level();
            if (NativeMethods.OpenClipboard(new HandleRef(this, Handle)))
                try
                {
                    var hMem = NativeMethods.GetClipboardData(NativeMethods.RegisterClipboardFormat(Level.LevelDataFormat));
                    unsafe
                    {
                        if (hMem != IntPtr.Zero)
                        {
                            var ptr = NativeMethods.GlobalLock(hMem);
                            if (ptr != null)
                                try
                                {
                                    using (var ms = new UnmanagedMemoryStream((byte*)ptr, NativeMethods.GlobalSize(hMem).ToInt64()))
                                    using (var reader = new BinaryReader(ms))
                                        level.Read(reader);
                                }
                                finally { NativeMethods.GlobalUnlock(hMem); }
                        }
                    }
                }
                finally { NativeMethods.CloseClipboard(); }
            InsertLevel(index, level);
            levelExplorerListBox.SelectedIndex = index;
        }

        private void RefreshSelectedTabFromTool()
        {
            if (editorTool != EditorTool.Select)
                fillToolStripButton.Enabled = cutToolStripButton.Enabled = copyToolStripButton.Enabled = false;
            if (editorTool != EditorTool.TrapConnector || editorTool != EditorTool.CloneConnector)
                foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                    tabPage.ClearIntermediateConnections();
        }

        private void RemoveCurrentLevel()
        {
            _levelSet.RemoveAt(levelExplorerListBox.SelectedIndex);
            foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                if (tabPage._level.Parent == null)
                    tabPage.Close();
        }

        static void ReportError(Exception ex)
        {
            if (NativeMethods.IsVista)
                new TaskDialog
                {
                    WindowTitle = Properties.Resources.ProductName,
                    Content = ex.Message,
                    MainIcon = TaskDialogIcon.Error,
                    CommonButtons = TaskDialogStandardButtons.Close,
                    ExpandedInformation = ex.ToString(),
                    ExpandFooterArea = true
                }.ShowDialog();
            else
                MessageBox.Show(ex.Message, Properties.Resources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
        }

        static void ReportError(string message)
        {
            if (NativeMethods.IsVista)
                new TaskDialog
                {
                    WindowTitle = Properties.Resources.ProductName,
                    MainInstruction = message,
                    MainIcon = TaskDialogIcon.Error,
                    CommonButtons = TaskDialogStandardButtons.Close,
                }.ShowDialog();
            else
                MessageBox.Show(message, Properties.Resources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
        }

        private bool SaveLevelSet()
        {
            if (string.IsNullOrEmpty(_fileName) || (File.Exists(_fileName) && new FileInfo(_fileName).IsReadOnly))
                return SaveLevelSetAs();
            else if (!saveBackgroundWorker.IsBusy)
            {
                Cursor = Cursors.WaitCursor;
                saveBackgroundWorker.RunWorkerAsync();
            }
            return true;
        }

        private bool SaveLevelSetAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _fileName = saveFileDialog.FileName;
                return SaveLevelSet();
            }
            return false;
        }

        private void TimerProc(IntPtr hwnd, int uMsg, IntPtr idEvent, int dwTime)
        {
            if (rightTabControl.TabCount > 0)
                UpdatePasteEnabled(rightTabControl.SelectedTab as LevelEditorTabPage);
        }

        private void UpdateLevelMenuItemsEnabled()
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
            {
                levelExplorerListBox.SelectedIndex = selectedTab._level.Index;
                replaceMenuItem.Enabled = selectToolStripButton.Enabled = pathMakerMenuItem.Enabled = pathMakerToolStripButton.Enabled = trapConnectorMenuItem.Enabled = trapConnectorToolStripButton.Enabled = cloneConnectorMenuItem.Enabled = cloneConnectorToolStripButton.Enabled = monstersMenuItem.Enabled = monstersToolStripButton.Enabled = trapConnectionsMenuItem.Enabled = trapConnectionsToolStripButton.Enabled = cloneConnectionsMenuItem.Enabled = cloneConnectionsToolStripButton.Enabled = selectMenuItem.Enabled = fillMenuItem.Enabled = switchTogglesMenuItem.Enabled = true;
            }
            else
            {
                levelExplorerListBox.SelectedIndex = -1;
                replaceMenuItem.Enabled = selectToolStripButton.Enabled = pathMakerMenuItem.Enabled = pathMakerToolStripButton.Enabled = trapConnectorMenuItem.Enabled = trapConnectorToolStripButton.Enabled = cloneConnectorMenuItem.Enabled = cloneConnectorToolStripButton.Enabled = monstersMenuItem.Enabled = monstersToolStripButton.Enabled = trapConnectionsMenuItem.Enabled = trapConnectionsToolStripButton.Enabled = cloneConnectionsMenuItem.Enabled = cloneConnectionsToolStripButton.Enabled = selectMenuItem.Enabled = fillMenuItem.Enabled = switchTogglesMenuItem.Enabled = false;
            }
        }

        private void UpdateTitle()
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(_fileName))
                sb.Append(Properties.Resources.Untitled);
            else
            {
                sb.Append(Path.GetFileName(_fileName));
                if (File.Exists(_fileName) && new FileInfo(_fileName).IsReadOnly)
                    sb.Append(Properties.Resources.ReadOnly);
            }
            sb.Append(Properties.Resources.AppTitleSuffix);
            if (NativeMethods.IsVista && NativeMethods.IsAdministrator)
                sb.Append(Properties.Resources.Administrator);
            if (isChanged)
                sb.Append("*");
            Text = sb.ToString();
        }

        private void Levels_ListChanged(object sender, ListChangedEventArgs e)
        {
            IsChanged = true;
        }

        #region File Menu

        private void fileMenuItem_Popup(object sender, EventArgs e)
        {
            closeMenuItem.Enabled = closeAllMenuItem.Enabled = rightTabControl.TabCount > 0;
        }

        private void newLevelButton_Click(object sender, EventArgs e)
        {
            int index;
            if (levelExplorerListBox.SelectedIndex == -1)
                index = levelExplorerListBox.Items.Count;
            else
                index = levelExplorerListBox.SelectedIndex + 1;
            InsertNewLevel(index);
            levelExplorerListBox.SelectedIndex = index;
            if (Properties.Settings.Default.AutomaticallyOpenNewLevels)
                OpenLevel(index);
        }

        private void newLevelSetButton_Click(object sender, EventArgs e)
        {
            NewLevelSet();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenLevelSet();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveLevelSet();
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveLevelSetAs();
        }

        private void closeAllMenuItem_Click(object sender, EventArgs e)
        {
            rightTabControl.TabPages.Clear();
            UpdateRightTabControlVisible();
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            CloseSelectedTab();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Edit Menu

        private void editMenuItem_Popup(object sender, EventArgs e)
        {
            undoMenuItem.Text = "Undo";
            redoMenuItem.Text = "Redo";
            replaceMenuItem.Text = string.Format(CultureInfo.CurrentCulture, "Replace '{0}' with '{1}'", TileUtilities.GetDescription(leftTile), TileUtilities.GetDescription(rightTile));

            var activeControl = GetRealActiveControl();

            if (activeControl is TextBoxBase activeTextBox)
            {
                undoMenuItem.Enabled = activeTextBox.CanUndo;
                cutMenuItem.Enabled = copyMenuItem.Enabled = activeTextBox.SelectionLength > 0;
                pasteMenuItem.Enabled = Clipboard.ContainsText();
                deleteMenuItem.Enabled = true;
                selectAllMenuItem.Enabled = true;
            }
            else
                if (rightTabControl.TabCount > 0)
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                {
                    undoMenuItem.Enabled = selectedTab.CanUndo;
                    if (undoMenuItem.Enabled)
                        undoMenuItem.Text += " " + selectedTab.UndoName;
                    redoMenuItem.Enabled = selectedTab.CanRedo;
                    if (redoMenuItem.Enabled)
                        redoMenuItem.Text += " " + selectedTab.RedoName;
                    pasteMenuItem.Enabled = LevelMapEditor.CanPaste;
                    if (selectedTab is LevelEditorTabPage activeLevelTabPage)
                    {
                        bool hasSelection = editorTool == EditorTool.Select && !activeLevelTabPage.IsSelectionEmpty;
                        cutMenuItem.Enabled = copyMenuItem.Enabled = deleteMenuItem.Enabled = fillMenuItem.Enabled = hasSelection;
                    }
                    selectAllMenuItem.Enabled = true;
                }
            }
            if (activeControl is ListBox activeListBox)
            {
                cutMenuItem.Enabled = copyMenuItem.Enabled = deleteMenuItem.Enabled = activeListBox.SelectedIndex >= 0;
                pasteMenuItem.Enabled = NativeMethods.IsClipboardFormatAvailable(NativeMethods.RegisterClipboardFormat(Level.LevelDataFormat));
            }
            if (!undoMenuItem.Enabled)
                undoMenuItem.Text = "Can't Undo";
            if (!redoMenuItem.Enabled)
                redoMenuItem.Text = "Can't Redo";
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
                activeTextBox.Undo();
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                    selectedTab.Undo();
            }
        }

        private void redoMenuItem_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.Redo();
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
                activeTextBox.Cut();
            else
                if (levelExplorerListBox.Focused && levelExplorerListBox.SelectedIndex >= 0)
            {
                CopyCurrentLevel();
                RemoveCurrentLevel();
            }
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                    selectedTab.Cut();
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
                activeTextBox.Copy();
            else
                if (levelExplorerListBox.Focused && levelExplorerListBox.SelectedIndex >= 0)
                CopyCurrentLevel();
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                    selectedTab.Copy();
            }
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
                activeTextBox.Paste();
            else
                if (levelExplorerListBox.Focused)
                PasteLevel();
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                    selectedTab.Paste();
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
            {
                if (activeTextBox.SelectionLength <= 0)
                    activeTextBox.SelectionLength++;
                activeTextBox.SelectedText = string.Empty;
            }
            else
                if (levelExplorerListBox.Focused && levelExplorerListBox.SelectedIndex >= 0)
                RemoveCurrentLevel();
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                    selectedTab.DeleteSelection();
            }
        }

        private void fillButton_Click(object sender, EventArgs e)
        {
            if (editorTool != EditorTool.Select)
                return;
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.FillSelection(leftTile, rightTile);
        }

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
            var activeControl = GetRealActiveControl();
            if (activeControl is TextBoxBase activeTextBox)
                activeTextBox.SelectAll();
            else
            {
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                {
                    selectMenuItem.Checked = selectToolStripButton.Checked = true;
                    EditorTool = EditorTool.Select;
                    selectedTab.SelectAll();
                }
            }
        }

        #endregion

        #region View Menu

        #region Zoom

        private void zoom125MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 4;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom125MenuItem.Checked = true;
        }

        private void zoom25MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 8;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom25MenuItem.Checked = true;
        }

        private void zoom50MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 16;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom50MenuItem.Checked = true;
        }

        private void zoom100MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 32;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom100MenuItem.Checked = true;
        }

        private void zoom200MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 64;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom200MenuItem.Checked = true;
        }

        private void zoom400MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 128;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom400MenuItem.Checked = true;
        }

        private void zoom800MenuItem_Click(object sender, EventArgs e)
        {
            TileSize = 256;
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            zoom800MenuItem.Checked = true;
        }

        private void fitToScreenMenuItem_Click(object sender, System.EventArgs e)
        {
            FitTileSizeToScreen();
            foreach (MenuItem menuItem in zoomMenuItem.MenuItems)
                menuItem.Checked = false;
            fitToScreenMenuItem.Checked = true;
        }

        #endregion

        private void cloneConnectionsMenuItem_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.ShowCloneConnectionsDialog();
        }

        #endregion

        #region Tools Menu

        private void selectButton_Click(object sender, EventArgs e)
        {
            selectMenuItem.Checked = !selectMenuItem.Checked;
            if (sender == selectMenuItem)
                selectToolStripButton.Checked = !selectToolStripButton.Checked;
            pathMakerMenuItem.Checked = pathMakerToolStripButton.Checked = trapConnectorMenuItem.Checked = trapConnectorToolStripButton.Checked = cloneConnectorMenuItem.Checked = cloneConnectorToolStripButton.Checked = false;
            EditorTool = selectToolStripButton.Checked ? EditorTool.Select : EditorTool.Default;
            RefreshSelectedTabFromTool();
        }

        private void pathMakerButton_Click(object sender, EventArgs e)
        {
            pathMakerMenuItem.Checked = !pathMakerMenuItem.Checked;
            if (sender == pathMakerMenuItem)
                pathMakerToolStripButton.Checked = !pathMakerToolStripButton.Checked;
            selectMenuItem.Checked = selectToolStripButton.Checked = trapConnectorMenuItem.Checked = trapConnectorToolStripButton.Checked = cloneConnectorMenuItem.Checked = cloneConnectorToolStripButton.Checked = false;
            EditorTool = pathMakerToolStripButton.Checked ? EditorTool.PathMaker : EditorTool.Default;
            RefreshSelectedTabFromTool();
        }

        private void trapConnectorButton_Click(object sender, EventArgs e)
        {
            trapConnectorMenuItem.Checked = !trapConnectorMenuItem.Checked;
            if (sender == trapConnectorMenuItem)
                trapConnectorToolStripButton.Checked = !trapConnectorToolStripButton.Checked;
            selectMenuItem.Checked = selectToolStripButton.Checked = pathMakerMenuItem.Checked = pathMakerToolStripButton.Checked = cloneConnectorMenuItem.Checked = cloneConnectorToolStripButton.Checked = false;
            EditorTool = trapConnectorToolStripButton.Checked ? EditorTool.TrapConnector : EditorTool.Default;
            RefreshSelectedTabFromTool();
        }

        private void cloneConnectorButton_Click(object sender, EventArgs e)
        {
            cloneConnectorMenuItem.Checked = !cloneConnectorMenuItem.Checked;
            if (sender == cloneConnectorMenuItem)
                cloneConnectorToolStripButton.Checked = !cloneConnectorToolStripButton.Checked;
            selectMenuItem.Checked = selectToolStripButton.Checked = pathMakerMenuItem.Checked = pathMakerToolStripButton.Checked = trapConnectorMenuItem.Checked = trapConnectorToolStripButton.Checked = false;
            EditorTool = cloneConnectorToolStripButton.Checked ? EditorTool.CloneConnector : EditorTool.Default;
            RefreshSelectedTabFromTool();
        }

        private void switchTogglesMenuItem_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.SwitchToggles();
            RefreshSelectedTabFromTool();
        }

        #endregion

        #region Help Menu

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion

        #region Close Tab Button

        private void closeTabButton_Click(object sender, EventArgs e)
        {
            CloseSelectedTab();
        }

        private void closeTabButton_MouseEnter(object sender, EventArgs e)
        {
            closeTabButton.FlatAppearance.BorderColor = Color.FromArgb(102, 153, 204);
        }

        private void closeTabButton_MouseLeave(object sender, EventArgs e)
        {
            closeTabButton.FlatAppearance.BorderColor = SystemColors.Control;
        }

        #endregion

        #region Main Tool Strip

        private void layerToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rightTabControl.TabCount > 0)
            {
                rightTabControl.SelectedTab.Invalidate(true);
                if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTabPage)
                    selectedTabPage.Focus();
            }
        }

        #endregion

        private void countChipsButton_Click(object sender, EventArgs e)
        {
            var index = levelExplorerListBox.SelectedIndex;
            if (index < 0)
                return;
            var current = _levelSet[index];
            if (current == null)
                return;
            var numChips = current.UpperLayer.CountTiles(Tile.Chip) + current.LowerLayer.CountTiles(Tile.Chip);
            chipsUpDown.Value = current.ChipCount = (ushort)numChips;
        }

        private void Levels_CollectionChanged(object sender, EventArgs e)
        {
            foreach (LevelEditorTabPage tabPage in rightTabControl.TabPages)
                if (tabPage._level.Parent == null)
                    tabPage.Close();
            UpdateTitle();
        }

        private void Levels_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            UpdateTitle();
        }

        private void levelBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            titleLabel.Enabled = titleTextBox.Enabled =
                passwordLabel.Enabled = passwordTextBox.Enabled = newPasswordButton.Enabled =
                chipsLabel.Enabled = chipsUpDown.Enabled = countChipsButton.Enabled =
                timeLimitLabel.Enabled = timeLimitUpDown.Enabled = noTimeLimitButton.Enabled =
                hintLabel.Enabled = hintTextBox.Enabled = levelExplorerListBox.SelectedIndex > -1;
        }

        #region Level Explorer Context Menu

        private void levelExplorerContextMenu_Popup(object sender, EventArgs e)
        {
            cutLevelMenuItem.Enabled = copyLevelMenuItem.Enabled = deleteLevelMenuItem.Enabled = levelExplorerListBox.SelectedIndex >= 0;
            pasteLevelMenuItem.Enabled = NativeMethods.IsClipboardFormatAvailable(NativeMethods.RegisterClipboardFormat(Level.LevelDataFormat));
        }

        private void cutLevelMenuItem_Click(object sender, System.EventArgs e)
        {
            if (levelExplorerListBox.SelectedIndex >= 0)
            {
                CopyCurrentLevel();
                RemoveCurrentLevel();
            }
        }

        private void copyLevelMenuItem_Click(object sender, System.EventArgs e)
        {
            if (levelExplorerListBox.SelectedIndex >= 0)
                CopyCurrentLevel();
        }

        private void pasteLevelMenuItem_Click(object sender, System.EventArgs e)
        {
            PasteLevel();
        }

        private void deleteLevelMenuItem_Click(object sender, System.EventArgs e)
        {
            if (levelExplorerListBox.SelectedIndex >= 0)
                RemoveCurrentLevel();
        }

        #endregion

        private void levelExplorerListBox_DoubleClick(object sender, EventArgs e)
        {
            var index = levelExplorerListBox.SelectedIndex;
            if (index <= -1)
                return;
            OpenLevel(index);
        }

        private void levelExplorerListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                levelExplorerListBox.SelectedIndex = levelExplorerListBox.IndexFromPoint(e.Location);
        }

        private void levelExplorerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = levelExplorerListBox.SelectedIndex;
            moveUpToolStripButton.Enabled = selectedIndex > 0;
            moveDownToolStripButton.Enabled = selectedIndex > -1 && selectedIndex < _levelSet.Count - 1;
            removeLevelToolStripButton.Enabled = selectedIndex > -1;
        }

        private void monstersToolStripButton_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.ShowMonstersDialog();
        }

        private void moveDownToolStripButton_Click(object sender, EventArgs e)
        {
            MoveCurrentLevel(1);
        }

        private void moveUpToolStripButton_Click(object sender, EventArgs e)
        {
            MoveCurrentLevel(-1);
        }

        private void newPasswordButton_Click(object sender, EventArgs e)
        {
            var current = _levelSet[levelExplorerListBox.SelectedIndex];
            if (current != null)
                passwordTextBox.Text = current.Password = Level.GenerateRandomPassword(4);
        }

        private void noTimeLimitButton_Click(object sender, EventArgs e)
        {
            var current = _levelSet[levelExplorerListBox.SelectedIndex];
            if (current != null)
                timeLimitUpDown.Value = current.TimeLimit = 0;
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsDialog().ShowDialog();
        }

        private void removeLevelToolStripButton_Click(object sender, EventArgs e)
        {
            RemoveCurrentLevel();
        }

        private void replaceMenuItem_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.ReplaceAll(leftTile, rightTile);
        }

        private void rightTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLevelMenuItemsEnabled();
            var selectedTab = rightTabControl.SelectedTab as LevelEditorTabPage;
            UpdateCutCopyPasteFillEnabled(selectedTab);
            if (fitToScreenMenuItem.Checked)
                FitTileSizeToScreen(selectedTab);
        }

        private void saveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try { _levelSet.Save(_fileName); }
            catch (ArgumentException ex) { e.Result = ex; }
        }

        private void saveBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception ex)
                ReportError(ex);
            else
            {
                UpdateTitle();
                IsChanged = false;
            }
            Cursor = Cursors.Default;
        }

        private void showGameGraphicsMenuItem_Click(object sender, EventArgs e)
        {
            showGameGraphicsMenuItem.Checked = !showGameGraphicsMenuItem.Checked;
            tileset = showGameGraphicsMenuItem.Checked ? Tileset.Preview : Tileset.Default;
            if (rightTabControl.TabCount > 0)
                rightTabControl.SelectedTab.Invalidate(true);
        }

        private void testMenuItem_Click(object sender, EventArgs e)
        {
            try { CCTest.Test(_levelSet, _levelSet[levelExplorerListBox.SelectedIndex]); }
            catch (FileNotFoundException ex) { ReportError(ex.Message); }
            catch (ArgumentException) { ReportError(Properties.Resources.ChipsChallengeNotSet); }
            catch (ApplicationException ex) { ReportError(ex.Message); }
        }

        private void toolboxItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control control && control.Tag != null)
                ItemDescriptionStatusText = TileUtilities.GetDescription((Tile)control.Tag);
        }

        private void toolboxItem_MouseLeave(object sender, EventArgs e)
        {
            ItemDescriptionStatusText = string.Empty;
        }

        private void toolStripContainer_TopToolStripPanel_SizeChanged(object sender, EventArgs e)
        {
            closeTabButton.Location = new Point(ClientSize.Width - 20, toolStripContainer.TopToolStripPanel.Height + 2);
        }

        private void trapConnectionsMenuItem_Click(object sender, EventArgs e)
        {
            if (rightTabControl.SelectedTab is LevelEditorTabPage selectedTab)
                selectedTab.ShowTrapConnectionsDialog();
        }

        #endregion

        private void PalettePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var x = e.X * 7 / palettePictureBox.Width;
            var y = e.Y * 16 / palettePictureBox.Height;
            var tile = (Tile)(16 * x + y);
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                LeftTile = tile;
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
                RightTile = tile;
        }
    }
}
