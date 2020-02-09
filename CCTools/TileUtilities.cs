using System;

namespace CCTools
{
	public static class TileUtilities
	{
		public static string GetDescription(Tile tile)
		{
			if (tile > Tile.AndChipE)
				return "Unknown (0x" + ((int)tile).ToString("X") + ")";
			if (tile >= Tile.AndBugN)
				return Properties.Resources.ResourceManager.GetString("Description_" + Enum.Format(typeof(Tile), tile - 96, "G"), Properties.Resources.Culture) + " (AND)";
			if (tile >= Tile.OrBugN)
				return Properties.Resources.ResourceManager.GetString("Description_" + Enum.Format(typeof(Tile), tile - 48, "G"), Properties.Resources.Culture) + " (OR)";
			return Properties.Resources.ResourceManager.GetString("Description_" + Enum.Format(typeof(Tile), tile, "G"), Properties.Resources.Culture);
		}

		public static bool IsEmpty(Tile upperTile, Tile lowerTile)
		{
			return (upperTile | lowerTile) == Tile.Floor;
		}

		public static bool IsMonster(Tile upperTile, Tile lowerTile)
		{
			return upperTile >= Tile.BugN && upperTile <= Tile.ParameciumE && lowerTile != Tile.CloningMachine;
		}

		// Specifies if the tile overlays by a small centered 20x20 square.
		public static bool IsOverlayUnder(Tile tile)
		{
			return tile == Tile.Floor || tile == Tile.Wall || tile == Tile.Water ||
				(tile >= Tile.Block && tile <= Tile.BlueWallReal) || tile == Tile.ToggleDoorClosed ||
				tile == Tile.ToggleDoorOpen || tile == Tile.HiddenWall || tile == Tile.Gravel ||
				(tile >= Tile.CloningMachine && tile <= Tile.Splash) ||
				(tile >= Tile.Unused38 && tile <= Tile.ChipSwimE);
		}

		public static bool IsPanel(Tile tile)
		{
			return (tile >= Tile.PanelN && tile <= Tile.PanelE) || tile == Tile.PanelSE;
		}
	}
}
