using System.Windows.Forms;

namespace CCTools.CCDesign
{
    public class PathMakerEditorTool : EditorTool
    {
        protected override void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
        {
            if (editor.LayerMode == LayerMode.LowerLayer)
                modifierKeys |= Keys.Shift;
            if (!InitializeLocations(editor, out TileLocation lastLocation, out TileLocation secondLastLocation))
            {
                if (modifierKeys == Keys.Shift)
                    editor.SetLowerLayerTile(location, editor.LeftTile);
                else
                    editor.SetUpperLayerTile(location, editor.LeftTile);
                editor.TileLocationHistory.Add(location);
                return;
            }
            var tile = editor.LeftTile;
            var previousTile = Control.ModifierKeys == Keys.Shift ? editor.Level.LowerLayer[lastLocation] : editor.Level.UpperLayer[lastLocation];
            UpdateTiles(location, ref tile, lastLocation, ref previousTile, secondLastLocation);
            editor.LeftTile = tile;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || editor.LayerMode == LayerMode.LowerLayer)
            {
                editor.SetLowerLayerTile(lastLocation, previousTile);
                editor.SetLowerLayerTile(location, tile);
            }
            else
            {
                editor.SetUpperLayerTile(lastLocation, previousTile);
                editor.SetUpperLayerTile(location, tile);
            }
            editor.TileLocationHistory.Add(location);
        }

        protected override void OnRightMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
        {
            if (editor.LayerMode == LayerMode.LowerLayer)
                modifierKeys |= Keys.Shift;
            if (!InitializeLocations(editor, out TileLocation lastLocation, out TileLocation secondLastLocation))
            {
                if (modifierKeys == Keys.Shift)
                    editor.SetLowerLayerTile(location, editor.RightTile);
                else
                    editor.SetUpperLayerTile(location, editor.RightTile);
                editor.TileLocationHistory.Add(location);
                return;
            }
            var tile = editor.RightTile;
            var previousTile = Control.ModifierKeys == Keys.Shift ? editor.Level.LowerLayer[lastLocation] : editor.Level.UpperLayer[lastLocation];
            UpdateTiles(location, ref tile, lastLocation, ref previousTile, secondLastLocation);
            editor.RightTile = tile;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || editor.LayerMode == LayerMode.LowerLayer)
            {
                editor.SetLowerLayerTile(lastLocation, previousTile);
                editor.SetLowerLayerTile(location, tile);
            }
            else
            {
                editor.SetUpperLayerTile(lastLocation, previousTile);
                editor.SetUpperLayerTile(location, tile);
            }
            editor.TileLocationHistory.Add(location);
        }

        static bool InitializeLocations(LevelMapEditor editor, out TileLocation location, out TileLocation previous)
        {
            location = TileLocation.Invalid;
            previous = TileLocation.Invalid;
            if (editor.TileLocationHistory.Count > 0)
            {
                location = editor.TileLocationHistory[editor.TileLocationHistory.Count - 1];
                if (editor.TileLocationHistory.Count > 1)
                    previous = editor.TileLocationHistory[editor.TileLocationHistory.Count - 2];
            }
            else
                return false;
            return true;
        }

        static void UpdateTiles(TileLocation location, ref Tile tile, TileLocation previous, ref Tile previousTile, TileLocation secondPrevious)
        {
            switch (tile)
            {
                case Tile.ForceFloorS:
                case Tile.ForceFloorN:
                case Tile.ForceFloorE:
                case Tile.ForceFloorW:
                    if (location.X > previous.X)
                        previousTile = Tile.ForceFloorE;
                    else if (location.X < previous.X)
                        previousTile = Tile.ForceFloorW;
                    else if (location.Y > previous.Y)
                        previousTile = Tile.ForceFloorS;
                    else if (location.Y < previous.Y)
                        previousTile = Tile.ForceFloorN;
                    tile = previousTile;
                    break;
                case Tile.Ice:
                case Tile.IceNE:
                case Tile.IceNW:
                case Tile.IceSE:
                case Tile.IceSW:
                    if (secondPrevious != TileLocation.Invalid)
                    {
                        if (location.X > previous.X && previous.Y < secondPrevious.Y || location.Y > previous.Y && previous.X < secondPrevious.X)
                            previousTile = Tile.IceNW;
                        else if (location.X < previous.X && previous.Y < secondPrevious.Y || location.Y > previous.Y && previous.X > secondPrevious.X)
                            previousTile = Tile.IceNE;
                        else if (location.X < previous.X && previous.Y > secondPrevious.Y || location.Y < previous.Y && previous.X > secondPrevious.X)
                            previousTile = Tile.IceSE;
                        else if (location.X > previous.X && previous.Y > secondPrevious.Y || location.Y < previous.Y && previous.X < secondPrevious.X)
                            previousTile = Tile.IceSW;
                        else
                            previousTile = Tile.Ice;
                    }
                    else
                        previousTile = Tile.Ice;
                    tile = Tile.Ice;
                    break;
                default:
                    break;
            }
        }
    }
}
