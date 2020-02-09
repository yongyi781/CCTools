using System.Drawing;

namespace CCTools.CCDesign
{
	public class ChangeTileCommand : Command
	{
		public ChangeTileCommand(bool lowerLayer, TileLocation location, Tile oldTile, Tile newTile)
		{
			this.lowerLayer = lowerLayer;
			this.location = location;
			this.oldTile = oldTile;
			this.newTile = newTile;
		}

		public override string Name
		{
			get { return "Change Tile"; }
		}

		private bool lowerLayer;
		public bool LowerLayer
		{
			get { return lowerLayer; }
		}

		private TileLocation location;
		public TileLocation Location
		{
			get { return location; }
		}

		private Tile oldTile;
		public Tile OldTile
		{
			get { return oldTile; }
		}

		private Tile newTile;
		public Tile NewTile
		{
			get { return newTile; }
		}

		public override void Do()
		{
			if (lowerLayer)
			{
				if (Owner.Level.LowerLayer[location] != newTile)
				{
					Owner.Level.LowerLayer[location] = newTile;
					Owner.IsChanged = true;
				}
			}
			else
			{
				if (Owner.Level.UpperLayer[location] != newTile)
				{
					Owner.Level.UpperLayer[location] = newTile;
					Owner.IsChanged = true;
				}
			}
			if (Owner.CurrentTileLocation == location)
			{
				Owner.UpdateTileDescription();
				Owner.UpdateTileCoordinatesAndHighlights();
			}
			if (!lowerLayer || (Owner.LayerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer)
				Owner.Invalidate(location);
		}

		public override void Undo()
		{
			if (lowerLayer)
			{
				if (Owner.Level.LowerLayer[location] != oldTile)
				{
					Owner.Level.LowerLayer[location] = oldTile;
					Owner.IsChanged = true;
				}
			}
			else
			{
				if (Owner.Level.UpperLayer[location] != oldTile)
				{
					Owner.Level.UpperLayer[location] = oldTile;
					Owner.IsChanged = true;
				}
			}
			if (Owner.CurrentTileLocation == location)
			{
				Owner.UpdateTileDescription();
				Owner.UpdateTileCoordinatesAndHighlights();
			}
			if (!lowerLayer || (Owner.LayerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer)
				Owner.Invalidate(location);
		}
	}
}
