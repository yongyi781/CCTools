using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public class LevelMapEditor : Control
	{
		#region Fields

		internal TrapConnectionsDialog _trapConnectionsDialog;
		internal CloneConnectionsDialog _cloneConnectionsDialog;
		internal MonstersDialog _monstersDialog;

		static readonly Pen SelectionPen = new Pen(Color.FromArgb(51, 102, 204), 1);
		static readonly Brush SelectionBrush = new SolidBrush(Color.FromArgb(128, 51, 153, 255));
		static readonly Pen CurrentTilePen = new Pen(Color.FromArgb(51, 153, 255), 1);
		static readonly Brush CurrentTileBrush = new SolidBrush(Color.FromArgb(64, 51, 153, 255));
		private readonly Form1 _owner;
		private readonly UndoRedoManager _history;
		private readonly Collection<HighlightMarker> _tileHighlights = new Collection<HighlightMarker>();
		private readonly Collection<HighlightMarker> _intermediateConnectionHighlights = new Collection<HighlightMarker>();
		private MouseButtons _mouseButtons;
		private TileConnection currentConnection = TileConnection.Invalid;

		#endregion

		#region Constructor

		public LevelMapEditor(Form1 owner, Level level)
		{
			_owner = owner;
			this.Level = level;
			_history = new UndoRedoManager(this);
			Size = new Size(_owner.TileSize * 32, _owner.TileSize * 32);
			TabStop = false;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		#endregion

		#region Properties

		public bool CanUndo
		{
			get { return _history.CanUndo; }
		}

		public bool CanRedo
		{
			get { return _history.CanRedo; }
		}

		public string UndoName
		{
			get { return _history.UndoName; }
		}

		public string RedoName
		{
			get { return _history.RedoName; }
		}

		public bool CanCopy
		{
			get { return selection != TileRectangle.Empty; }
		}

		public static bool CanPaste
		{
			get { return NativeMethods.IsClipboardFormatAvailable(NativeMethods.RegisterClipboardFormat(Level.ChipEditMapSectFormat)); }
		}

		public Collection<HighlightMarker> CustomTileHighlights { get; } = new Collection<HighlightMarker>();

		private TileLocation currentTileLocation = TileLocation.Invalid;
		public TileLocation CurrentTileLocation
		{
			get { return currentTileLocation; }
		}

		public TileLocationCollection TileLocationHistory { get; } = new TileLocationCollection();

		public bool IsChanged
		{
			get { return _owner.IsChanged; }
			set { _owner.IsChanged = value; }
		}

		public bool IsSelecting
		{
			get { return _owner.EditorTool == EditorTool.Select && selection.Width != 0 && selection.Height != 0; }
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

		public Level Level { get; }

		private TileRectangle selection = TileRectangle.Empty;
		public TileRectangle Selection
		{
			get { return selection; }
			set
			{
				if (value.Right > 31)
					value.Width = 32 - value.X;
				if (value.Bottom > 31)
					value.Height = 32 - value.Y;
				if (selection != value)
				{
					var invalidateRect = Rectangle.Union(selection.ToRectangle(_owner.TileSize), value.ToRectangle(_owner.TileSize));
					selection = value;
					if (_owner != null)
						_owner.UpdateCutCopyPasteFillEnabled(this);
					Invalidate(invalidateRect);
				}
			}
		}

		public int TileSize
		{
			get { return _owner.TileSize; }
		}

		#endregion

		#region Methods

		public void Invalidate(TileLocation location)
		{
			Invalidate(location.ToRectangle(_owner.TileSize));
		}

		public void Invalidate(TileConnection connection)
		{
			Invalidate(connection.Source.ToRectangle(_owner.TileSize));
			Invalidate(connection.Destination.ToRectangle(_owner.TileSize));
		}

		public void SetCloneConnection(TileLocation location)
		{
			if (currentConnection.Source == TileLocation.Invalid)
			{
				currentConnection.Source = location;
				_intermediateConnectionHighlights.Add(new TileLocationMarker(location, Pens.Blue));
				Invalidate(location);
			}
			else
			{
				currentConnection.Destination = location;
				_history.Do(new AddCloneConnectionCommand(currentConnection));
				var marker = HighlightMarker.FromCloneConnection(currentConnection);
				_tileHighlights.Add(marker);
				ClearIntermediateConnections();
				marker.Invalidate(this);
			}
		}

		public void SetTrapConnection(TileLocation location)
		{
			if (currentConnection.Source == TileLocation.Invalid)
			{
				currentConnection.Source = location;
				_intermediateConnectionHighlights.Add(new TileLocationMarker(location, Pens.Blue));
				Invalidate(location);
			}
			else
			{
				currentConnection.Destination = location;
				_history.Do(new AddTrapConnectionCommand(currentConnection));
				var marker = HighlightMarker.FromTrapConnection(currentConnection);
				_tileHighlights.Add(marker);
				ClearIntermediateConnections();
				marker.Invalidate(this);
			}
		}

		public void ShowCloneConnectionsDialog()
		{
			if (_cloneConnectionsDialog != null)
				_cloneConnectionsDialog.Activate();
			else
				(_cloneConnectionsDialog = new CloneConnectionsDialog(this)).Show(this);
		}

		public void ShowTrapConnectionsDialog()
		{
			if (_trapConnectionsDialog != null)
				_trapConnectionsDialog.Activate();
			else
				(_trapConnectionsDialog = new TrapConnectionsDialog(this)).Show(this);
		}

		public void ShowMonstersDialog()
		{
			if (_monstersDialog != null)
				_monstersDialog.Activate();
			else
				(_monstersDialog = new MonstersDialog(this)).Show(this);
		}

		public void ClearIntermediateConnections()
		{
			var oldHighlights = new HighlightMarker[_intermediateConnectionHighlights.Count];
			_intermediateConnectionHighlights.CopyTo(oldHighlights, 0);
			_intermediateConnectionHighlights.Clear();
			foreach (var highlight in oldHighlights)
				highlight.Invalidate(this);
			currentConnection = TileConnection.Invalid;
		}

		public void Cut()
		{
			Copy();
			DeleteSelection();
		}

		public void Copy()
		{
			var selectionRect = selection.ActualRectangle;
			if (selectionRect.IsEmpty)
				return;

			var upperLayer = new TileMap();
			var lowerLayer = new TileMap();
			switch (_owner.LayerMode)
			{
				case LayerMode.UpperLayer:
					for (int y = 0; y < selectionRect.Height; y++)
						for (int x = 0; x < selectionRect.Width; x++)
							upperLayer[x, y] = Level.UpperLayer[x + selectionRect.X, y + selectionRect.Y];
					break;
				case LayerMode.LowerLayer:
					for (int y = 0; y < selectionRect.Height; y++)
						for (int x = 0; x < selectionRect.Width; x++)
							lowerLayer[x, y] = Level.LowerLayer[x + selectionRect.X, y + selectionRect.Y];
					break;
				case LayerMode.Both:
					for (int y = 0; y < selectionRect.Height; y++)
						for (int x = 0; x < selectionRect.Width; x++)
						{
							upperLayer[x, y] = Level.UpperLayer[x + selectionRect.X, y + selectionRect.Y];
							lowerLayer[x, y] = Level.LowerLayer[x + selectionRect.X, y + selectionRect.Y];
						}
					break;
				default:
					break;
			}
			var upperBytes = upperLayer.GetBytes();
			var upperLength = upperBytes.Length;
			var lowerBytes = lowerLayer.GetBytes();
			var lowerLength = lowerBytes.Length;

			if (NativeMethods.OpenClipboard(new HandleRef(this, Handle)))
				try
				{
					NativeMethods.EmptyClipboard();

					unsafe
					{
						var size = new IntPtr(sizeof(NativeMethods.CHIPEDIT_MAPSECT) + upperLength + lowerLength + 6);
						var hMem = NativeMethods.GlobalAlloc(NativeMethods.GMEM_MOVEABLE, size);
						if (hMem != IntPtr.Zero)
						{
							var ptr = NativeMethods.GlobalLock(hMem);
							if (ptr != null)
								try
								{
									var mapSect = (NativeMethods.CHIPEDIT_MAPSECT*)ptr;
									mapSect->width = selectionRect.Width;
									mapSect->height = selectionRect.Height;
									mapSect->bytesToEndOfData = upperLength + lowerLength + 14;
									mapSect->reserved1 = 0;
									mapSect->reserved2 = 0;
									mapSect->marker = 1;
									var buffer = (byte*)(&mapSect->marker + 1);
									*(ushort*)buffer = (ushort)upperLength;
									buffer += sizeof(ushort);
									NativeMethods.RtlMoveMemory(buffer, upperBytes, new IntPtr(upperLength));
									buffer += upperLength;
									*(ushort*)buffer = (ushort)lowerLength;
									buffer += sizeof(ushort);
									NativeMethods.RtlMoveMemory(buffer, lowerBytes, new IntPtr(lowerLength));
									buffer += lowerLength;
									*(ushort*)buffer = 0;
								}
								finally { NativeMethods.GlobalUnlock(hMem); }
							NativeMethods.SetClipboardData(NativeMethods.RegisterClipboardFormat(Level.ChipEditMapSectFormat), hMem);
						}
					}
					using (var bmp = new Bitmap(selectionRect.Width * _owner.TileSize, selectionRect.Height * _owner.TileSize))
					{
						using (var g = Graphics.FromImage(bmp))
						{
							g.PixelOffsetMode = PixelOffsetMode.HighQuality;
							g.InterpolationMode = _owner.TileSize >= 32 ? InterpolationMode.NearestNeighbor : InterpolationMode.Default;
							Level.Draw(g, _owner.Tileset, _owner.LayerMode, selectionRect, _owner.TileSize);
						}
						NativeMethods.SetClipboardData(NativeMethods.CF_BITMAP, NativeMethods.GetCompatibleBitmap(_owner, bmp));
					}
				}
				finally { NativeMethods.CloseClipboard(); }
		}

		public void Paste()
		{
			if (NativeMethods.OpenClipboard(new HandleRef(this, Handle)))
				try
				{
					var hMem = NativeMethods.GetClipboardData(NativeMethods.RegisterClipboardFormat(Level.ChipEditMapSectFormat));
					if (hMem != IntPtr.Zero)
					{
						unsafe
						{
							var mapSect = (NativeMethods.CHIPEDIT_MAPSECT*)NativeMethods.GlobalLock(hMem);
							try
							{
								_history.BeginCompoundCommand("Paste");
								var width = mapSect->width;
								var height = mapSect->height;
								var buffer = (byte*)(&mapSect->marker + 1);
								var upperLength = *(ushort*)buffer;
								buffer += sizeof(ushort);
								var upperLayer = new TileMap(buffer, upperLength);
								buffer += upperLength;
								var lowerLength = *(ushort*)buffer;
								buffer += sizeof(ushort);
								var lowerLayer = new TileMap((byte*)buffer, lowerLength);
								buffer += lowerLength;
								switch (_owner.LayerMode)
								{
									case LayerMode.UpperLayer:
										for (int y = 0; y < height; y++)
											for (int x = 0; x < width; x++)
												if (x + selection.X < 32 && y + selection.Y < 32)
													SetUpperLayerTile(new TileLocation(x + selection.X, y + selection.Y), upperLayer[x, y]);
										break;
									case LayerMode.LowerLayer:
										for (int y = 0; y < height; y++)
											for (int x = 0; x < width; x++)
												if (x + selection.X < 32 && y + selection.Y < 32)
													SetLowerLayerTile(new TileLocation(x + selection.X, y + selection.Y), lowerLayer[x, y]);
										break;
									case LayerMode.Both:
										for (int y = 0; y < height; y++)
											for (int x = 0; x < width; x++)
												if (x + selection.X < 32 && y + selection.Y < 32)
												{
													SetUpperLayerTile(new TileLocation(x + selection.X, y + selection.Y), upperLayer[x, y]);
													SetLowerLayerTile(new TileLocation(x + selection.X, y + selection.Y), lowerLayer[x, y]);
												}
										break;
									default:
										break;
								}
								Selection = new TileRectangle(selection.X, selection.Y, width, height);
							}
							finally
							{
								NativeMethods.GlobalUnlock(hMem);
								_history.EndCompoundCommand();
							}
						}
					}
				}
				finally { NativeMethods.CloseClipboard(); }
			Invalidate(new Rectangle(selection.X * _owner.TileSize, selection.Y * _owner.TileSize, selection.Width * _owner.TileSize, selection.Height * _owner.TileSize));
		}

		public void PopulateMonsters()
		{
			var monsters = Level.PopulateMonsters();
			if (monsters.Length > 0)
			{
				_history.BeginCompoundCommand("Populate Monsters");
				_history.Do(new ClearMonsterLocationsCommand());
				_history.Do(new AddMonsterRangeCommand(monsters));
				_history.EndCompoundCommand();
			}
		}

		public void SelectAll()
		{
			Selection = TileRectangle.AllTiles;
		}

		public void Undo()
		{
			_history.Undo();
		}

		public void Redo()
		{
			_history.Redo();
		}

		public void ReplaceAll(Tile oldTile, Tile newTile)
		{
			_history.BeginCompoundCommand("Replace");
			var array = (IsSelecting ? selection.ActualRectangle : TileRectangle.AllTiles).ToArray();
			switch (_owner.LayerMode)
			{
				case LayerMode.UpperLayer:
					foreach (var location in array)
						if (Level.UpperLayer[location] == oldTile)
							SetUpperLayerTile(location, newTile);
					break;
				case LayerMode.LowerLayer:
					foreach (var location in array)
						if (Level.LowerLayer[location] == oldTile)
							SetLowerLayerTile(location, newTile);
					break;
				case LayerMode.Both:
					foreach (var location in array)
					{
						if (Level.UpperLayer[location] == oldTile)
							SetUpperLayerTile(location, newTile);
						if (Level.LowerLayer[location] == oldTile)
							SetLowerLayerTile(location, newTile);
					}
					break;
				default:
					break;
			}
			_history.EndCompoundCommand();
		}

		public void FillSelection(Tile leftTile, Tile rightTile)
		{
			var array = selection.ActualRectangle.ToArray();
			_history.BeginCompoundCommand("Fill");
			switch (_owner.LayerMode)
			{
				case LayerMode.UpperLayer:
					foreach (var location in array)
						SetUpperLayerTile(location, leftTile);
					break;
				case LayerMode.LowerLayer:
					foreach (var location in array)
						SetLowerLayerTile(location, leftTile);
					break;
				case LayerMode.Both:
					foreach (var location in array)
					{
						SetUpperLayerTile(location, leftTile);
						SetLowerLayerTile(location, rightTile);
					}
					break;
				default:
					break;
			}
			_history.EndCompoundCommand();
		}

		public void DeleteSelection()
		{
			var array = selection.ActualRectangle.ToArray();
			_history.BeginCompoundCommand("Delete");
			switch (_owner.LayerMode)
			{
				case LayerMode.UpperLayer:
					foreach (var location in array)
						SetUpperLayerTile(location, Tile.Floor);
					break;
				case LayerMode.LowerLayer:
					foreach (var location in array)
						SetLowerLayerTile(location, Tile.Floor);
					break;
				case LayerMode.Both:
					foreach (var location in array)
					{
						SetUpperLayerTile(location, Tile.Floor);
						SetLowerLayerTile(location, Tile.Floor);
					}
					break;
				default:
					break;
			}
			_history.EndCompoundCommand();
		}

		public void SetLowerLayerTile(TileLocation location, Tile tile)
		{
			var oldLowerTile = Level.LowerLayer[location];
			if (tile == oldLowerTile)
				return;
			var upperTile = Level.UpperLayer[location];
			_history.BeginCompoundCommand("Change Tile");
			_history.Do(new ChangeTileCommand(true, location, oldLowerTile, tile));
			UpdateMonsterLocationsAndConnections(location, upperTile, tile, upperTile, oldLowerTile);
			_history.EndCompoundCommand();
		}

		public void SetUpperLayerTile(TileLocation location, Tile tile)
		{
			var oldUpperTile = Level.UpperLayer[location];
			if (tile == oldUpperTile)
				return;
			var lowerTile = Level.LowerLayer[location];
			_history.BeginCompoundCommand("Change Tile");
			_history.Do(new ChangeTileCommand(false, location, oldUpperTile, tile));
			UpdateMonsterLocationsAndConnections(location, tile, lowerTile, oldUpperTile, lowerTile);
			_history.EndCompoundCommand();
		}

		public void SwitchToggles()
		{
			_history.BeginCompoundCommand("Switch Toggles");
			var array = (IsSelecting ? selection.ActualRectangle : TileRectangle.AllTiles).ToArray();
			switch (_owner.LayerMode)
			{
				case LayerMode.UpperLayer:
					foreach (var location in array)
						if (Level.UpperLayer[location] == Tile.ToggleDoorClosed)
							SetUpperLayerTile(location, Tile.ToggleDoorOpen);
						else if (Level.UpperLayer[location] == Tile.ToggleDoorOpen)
							SetUpperLayerTile(location, Tile.ToggleDoorClosed);
					break;
				case LayerMode.LowerLayer:
					foreach (var location in array)
						if (Level.LowerLayer[location] == Tile.ToggleDoorClosed)
							SetLowerLayerTile(location, Tile.ToggleDoorOpen);
						else if (Level.LowerLayer[location] == Tile.ToggleDoorOpen)
							SetLowerLayerTile(location, Tile.ToggleDoorClosed);
					break;
				case LayerMode.Both:
					foreach (var location in array)
					{
						if (Level.UpperLayer[location] == Tile.ToggleDoorClosed)
							SetUpperLayerTile(location, Tile.ToggleDoorOpen);
						else if (Level.UpperLayer[location] == Tile.ToggleDoorOpen)
							SetUpperLayerTile(location, Tile.ToggleDoorClosed);

						if (Level.LowerLayer[location] == Tile.ToggleDoorClosed)
							SetLowerLayerTile(location, Tile.ToggleDoorOpen);
						else if (Level.LowerLayer[location] == Tile.ToggleDoorOpen)
							SetLowerLayerTile(location, Tile.ToggleDoorClosed);
					}
					break;
				default:
					break;
			}
			_history.EndCompoundCommand();
		}

		public void UpdateLayerMode()
		{
			Invalidate();
		}

		#region Event Handlers

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			_mouseButtons = e.Button;

			var location = new TileLocation(e.X / _owner.TileSize, e.Y / _owner.TileSize);
			if (!location.IsValid())
				return;
			currentTileLocation = location;

			_owner.EditorTool.OnMouseDown(this, location, _mouseButtons, ModifierKeys);

			var upperTile = Level.UpperLayer[location];
			var lowerTile = Level.LowerLayer[location];

			if (upperTile != Tile.Floor || lowerTile != Tile.Floor)
				_owner.ItemDescriptionStatusText = lowerTile == Tile.Floor ? TileUtilities.GetDescription(upperTile) : string.Format(CultureInfo.CurrentCulture, "{0} | {1}", TileUtilities.GetDescription(upperTile), TileUtilities.GetDescription(lowerTile));
			else
				_owner.ItemDescriptionStatusText = string.Empty;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			_owner.ItemDescriptionStatusText = _owner.CoordinatesStatusText = string.Empty;
			currentTileLocation = TileLocation.Invalid;
			_tileHighlights.Clear();
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			var location = new TileLocation(e.X / _owner.TileSize, e.Y / _owner.TileSize);
			if (currentTileLocation == location || !location.IsValid())
				return;
			var previousTileLocation = currentTileLocation;
			currentTileLocation = location;

			_owner.EditorTool.OnMouseMove(this, location, _mouseButtons, ModifierKeys);

			var upperTile = Level.UpperLayer[location];
			var lowerTile = Level.LowerLayer[location];

			if (upperTile != Tile.Floor || lowerTile != Tile.Floor)
				_owner.ItemDescriptionStatusText = lowerTile == Tile.Floor ? TileUtilities.GetDescription(upperTile) : string.Format(CultureInfo.CurrentCulture, "{0} | {1}", TileUtilities.GetDescription(upperTile), TileUtilities.GetDescription(lowerTile));
			else
				_owner.ItemDescriptionStatusText = string.Empty;

			Invalidate(previousTileLocation.ToRectangle(_owner.TileSize));
			Invalidate(location.ToRectangle(_owner.TileSize));

			UpdateTileCoordinatesAndHighlights();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_mouseButtons = MouseButtons;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.InterpolationMode = _owner.TileSize >= 32 ? InterpolationMode.NearestNeighbor : InterpolationMode.Default;

			Level.Draw(e.Graphics, _owner.Tileset, _owner.LayerMode, _owner.TileSize);
			DrawCurrentTile(e.Graphics);
			DrawSelection(e);
			foreach (var highlight in _tileHighlights)
				highlight.Draw(e.Graphics, _owner.TileSize);
			foreach (var highlight in CustomTileHighlights)
				highlight.Draw(e.Graphics, _owner.TileSize);
			foreach (var highlight in _intermediateConnectionHighlights)
				highlight.Draw(e.Graphics, _owner.TileSize);
		}

		private void DrawSelection(PaintEventArgs e)
		{
			if (IsSelecting)
			{
				var rectangle = selection.ActualRectangle.ToRectangle(_owner.TileSize);
				var decrease = Math.Max(_owner.TileSize / 16, 1);
				rectangle.Inflate(-decrease, -decrease);
				e.Graphics.FillRectangle(SelectionBrush, Rectangle.Intersect(rectangle, e.ClipRectangle));
				e.Graphics.DrawRectangle(SelectionPen, rectangle);
			}
		}

		private void DrawCurrentTile(Graphics graphics)
		{
			var rectangle = currentTileLocation.ToRectangle(_owner.TileSize);
			var decrease = Math.Max(_owner.TileSize / 16, 1);
			rectangle.Inflate(-decrease, -decrease);
			graphics.FillRectangle(CurrentTileBrush, rectangle);
			graphics.DrawRectangle(CurrentTilePen, rectangle);
		}

		#endregion

		public void AddTrapConnection(TileConnection connection)
		{
			_history.Do(new AddTrapConnectionCommand(connection));
		}

		public void AddCloneConnection(TileConnection connection)
		{
			_history.Do(new AddCloneConnectionCommand(connection));
		}

		public void RemoveTrapConnection(TileConnection connection)
		{
			_history.Do(new RemoveTrapConnectionCommand(connection));
		}

		public void RemoveTrapConnectionAt(int index)
		{
			if (index >= 0 && index < Level.TrapConnections.Count)
				_history.Do(new RemoveTrapConnectionCommand(index));
		}

		public void RemoveCloneConnection(TileConnection connection)
		{
			_history.Do(new RemoveCloneConnectionCommand(connection));
		}

		public void RemoveCloneConnectionAt(int index)
		{
			if (index >= 0 && index < Level.CloneConnections.Count)
				_history.Do(new RemoveCloneConnectionCommand(index));
		}

		public void AddMonster(TileLocation location)
		{
			_history.Do(new AddMonsterCommand(location));
		}

		public void RemoveMonster(TileLocation location)
		{
			_history.Do(new RemoveMonsterCommand(location));
		}

		public void RemoveMonsterAt(int index)
		{
			if (index >= 0 && index < Level.MonsterLocations.Count)
				_history.Do(new RemoveMonsterCommand(index));
		}

		public void MoveCloneConnection(int oldIndex, int newIndex)
		{
			_history.Do(new MoveCloneConnectionCommand(oldIndex, newIndex));
		}

		public void MoveTrapConnection(int oldIndex, int newIndex)
		{
			_history.Do(new MoveTrapConnectionCommand(oldIndex, newIndex));
		}

		public void MoveMonsterLocation(int oldIndex, int newIndex)
		{
			_history.Do(new MoveMonsterLocationCommand(oldIndex, newIndex));
		}

		internal void UpdateTileSize()
		{
			Size = new Size(_owner.TileSize * 32, _owner.TileSize * 32);
			SelectionPen.Width = Math.Max(_owner.TileSize / 32, 1);
			CurrentTilePen.Width = Math.Max(_owner.TileSize / 32, 1);
			Invalidate();
		}

		internal void UpdateTileDescription()
		{
			var location = currentTileLocation;
			var upperTile = Level.UpperLayer[location];
			var lowerTile = Level.LowerLayer[location];
			if (upperTile != Tile.Floor || lowerTile != Tile.Floor)
				_owner.ItemDescriptionStatusText = lowerTile == Tile.Floor ? TileUtilities.GetDescription(upperTile) : string.Format(CultureInfo.CurrentCulture, "{0} | {1}", TileUtilities.GetDescription(upperTile), TileUtilities.GetDescription(lowerTile));
			else
				_owner.ItemDescriptionStatusText = string.Empty;
		}

		internal void UpdateTileCoordinatesAndHighlights()
		{
			var location = currentTileLocation;
			_owner.CoordinatesStatusText = string.Format(CultureInfo.CurrentCulture, "{0}", location);

			var oldHighlights = new TileConnectionMarker[_tileHighlights.Count];
			_tileHighlights.CopyTo(oldHighlights, 0);
			_tileHighlights.Clear();
			foreach (var highlight in oldHighlights)
				highlight.Invalidate(this);

			var upperTile = Level.UpperLayer[location];
			if (upperTile == Tile.Teleport)
			{
				var nextTeleport = Level.UpperLayer.FindNextTeleport(location);
				if (nextTeleport.IsValid())
				{
					var marker = HighlightMarker.FromTeleportConnection(new TileConnection(location, nextTeleport));
					_tileHighlights.Add(marker);
					_owner.CoordinatesStatusText += string.Format(CultureInfo.CurrentCulture, " → {1}", location, nextTeleport);
					marker.Invalidate(this);
				}
			}

			var cloneDestinations = Level.CloneConnections.GetDestinationsFromSource(location);
			if (cloneDestinations != null && cloneDestinations.Count > 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < cloneDestinations.Count; i++)
				{
					if (i > 0)
						sb.Append(", ");
					var destination = cloneDestinations[i];
					var marker = HighlightMarker.FromCloneConnection(new TileConnection(location, destination));
					_tileHighlights.Add(marker);
					sb.AppendFormat("{0}", destination);
					marker.Invalidate(this);
				}
				_owner.CoordinatesStatusText += string.Format(CultureInfo.CurrentCulture, " → {1}", location, sb);
			}

			var cloneSources = Level.CloneConnections.GetSourcesFromDestination(location);
			if (cloneSources != null && cloneSources.Count > 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < cloneSources.Count; i++)
				{
					if (i > 0)
						sb.Append(", ");
					var source = cloneSources[i];
					var marker = HighlightMarker.FromCloneConnection(new TileConnection(source, location));
					_tileHighlights.Add(marker);
					sb.AppendFormat("{0}", source);
					marker.Invalidate(this);
				}
				_owner.CoordinatesStatusText += string.Format(CultureInfo.CurrentCulture, " ← {1}", location, sb);
			}

			var trapDestinations = Level.TrapConnections.GetDestinationsFromSource(location);
			if (trapDestinations != null && trapDestinations.Count > 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < trapDestinations.Count; i++)
				{
					if (i > 0)
						sb.Append(", ");
					var destination = trapDestinations[i];
					var marker = HighlightMarker.FromTrapConnection(new TileConnection(location, destination));
					_tileHighlights.Add(marker);
					sb.AppendFormat("{0}", destination);
					marker.Invalidate(this);
				}
				_owner.CoordinatesStatusText += string.Format(CultureInfo.CurrentCulture, " → {1}", location, sb);
			}

			var trapSources = Level.TrapConnections.GetSourcesFromDestination(location);
			if (trapSources != null && trapSources.Count > 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < trapSources.Count; i++)
				{
					if (i > 0)
						sb.Append(", ");
					var source = trapSources[i];
					var marker = HighlightMarker.FromTrapConnection(new TileConnection(source, location));
					_tileHighlights.Add(marker);
					sb.AppendFormat("{0}", source);
					marker.Invalidate(this);
				}
				_owner.CoordinatesStatusText += string.Format(CultureInfo.CurrentCulture, " ← {1}", location, sb);
			}
		}

		private void UpdateMonsterLocationsAndConnections(TileLocation location, Tile upperTile, Tile lowerTile, Tile oldUpperTile, Tile oldLowerTile)
		{
			if (!Level.MonsterLocations.Contains(location) && TileUtilities.IsMonster(upperTile, lowerTile))
				_history.Do(new AddMonsterCommand(location));
			if (Level.MonsterLocations.Contains(location) && !TileUtilities.IsMonster(upperTile, lowerTile))
				_history.Do(new RemoveMonsterCommand(location));

			if (!TileUtilities.IsEmpty(oldUpperTile, oldLowerTile) && TileUtilities.IsEmpty(upperTile, lowerTile))
			{
				var trapSources = Level.TrapConnections.GetSourcesFromDestination(location);
				if (trapSources != null && trapSources.Count > 0)
					foreach (var source in trapSources)
						_history.Do(new RemoveTrapConnectionCommand(source, location));
				var trapDestinations = Level.TrapConnections.GetDestinationsFromSource(location);
				if (trapDestinations != null && trapDestinations.Count > 0)
					foreach (var destination in trapDestinations)
						_history.Do(new RemoveTrapConnectionCommand(location, destination));

				var cloneSources = Level.CloneConnections.GetSourcesFromDestination(location);
				if (cloneSources != null && cloneSources.Count > 0)
					foreach (var source in cloneSources)
						_history.Do(new RemoveCloneConnectionCommand(source, location));
				var cloneDestinations = Level.CloneConnections.GetDestinationsFromSource(location);
				if (cloneDestinations != null && cloneDestinations.Count > 0)
					foreach (var destination in cloneDestinations)
						_history.Do(new RemoveCloneConnectionCommand(location, destination));
			}
		}

		#endregion
	}
}
