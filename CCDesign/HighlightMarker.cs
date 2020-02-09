using System.Drawing;

namespace CCTools.CCDesign
{
	public abstract class HighlightMarker
	{
		public abstract void Invalidate(LevelMapEditor editor);
		public abstract void Draw(Graphics graphics, int tileSize);

		public static TileLocationMarker FromMonster(TileLocation location)
		{
			return new TileLocationMarker(location, Pens.Red);
		}

		public static TileConnectionMarker FromTrapConnection(TileConnection connection)
		{
			return new TileConnectionMarker(connection, Pens.Blue, Pens.Magenta, Pens.Brown);
		}

		public static TileConnectionMarker FromCloneConnection(TileConnection connection)
		{
			return new TileConnectionMarker(connection, Pens.Blue, Pens.Red, Pens.Red);
		}

		public static TileConnectionMarker FromTeleportConnection(TileConnection connection)
		{
			return new TileConnectionMarker(connection, Pens.Cyan, Pens.Cyan, Pens.Cyan);
		}
	}
}
