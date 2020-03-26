using System.Drawing;

namespace CCTools.CCDesign
{
    public class ChangeTileCommand : Command
    {
        public ChangeTileCommand(bool lowerLayer, TileLocation location, Tile oldTile, Tile newTile)
        {
            LowerLayer = lowerLayer;
            this.location = location;
            OldTile = oldTile;
            NewTile = newTile;
        }

        public override string Name
        {
            get { return "Change Tile"; }
        }

        public bool LowerLayer { get; }

        private TileLocation location;
        public TileLocation Location
        {
            get { return location; }
        }

        public Tile OldTile { get; }
        public Tile NewTile { get; }

        public override void Do()
        {
            if (LowerLayer)
            {
                if (Owner.Level.LowerLayer[location] != NewTile)
                {
                    Owner.Level.LowerLayer[location] = NewTile;
                    Owner.IsChanged = true;
                }
            }
            else
            {
                if (Owner.Level.UpperLayer[location] != NewTile)
                {
                    Owner.Level.UpperLayer[location] = NewTile;
                    Owner.IsChanged = true;
                }
            }
            if (Owner.CurrentTileLocation == location)
            {
                Owner.UpdateTileDescription();
                Owner.UpdateTileCoordinatesAndHighlights();
            }
            if (!LowerLayer || (Owner.LayerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer)
                Owner.Invalidate(location);
        }

        public override void Undo()
        {
            if (LowerLayer)
            {
                if (Owner.Level.LowerLayer[location] != OldTile)
                {
                    Owner.Level.LowerLayer[location] = OldTile;
                    Owner.IsChanged = true;
                }
            }
            else
            {
                if (Owner.Level.UpperLayer[location] != OldTile)
                {
                    Owner.Level.UpperLayer[location] = OldTile;
                    Owner.IsChanged = true;
                }
            }
            if (Owner.CurrentTileLocation == location)
            {
                Owner.UpdateTileDescription();
                Owner.UpdateTileCoordinatesAndHighlights();
            }
            if (!LowerLayer || (Owner.LayerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer)
                Owner.Invalidate(location);
        }
    }
}
