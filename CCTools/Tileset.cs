using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CCTools
{
	public class Tileset
	{
		#region Fields

		public static readonly Tileset Default = new Tileset(Properties.Resources.DefaultTileset);
		public static readonly Tileset Preview = new Tileset(Properties.Resources.PreviewTileset);

		private Bitmap[] tiles = new Bitmap[224];

		#endregion

		#region Constructor

		public Tileset(Bitmap defaultBitmap, Bitmap overlayBitmap)
		{
			tiles = new Bitmap[224];
			int i = 0;
			for (int x = 0; x < 7; x++)
				for (int y = 0; y < 16; y++)
				{
					tiles[i] = defaultBitmap.Clone(new Rectangle(x * 32, y * 32, 32, 32), defaultBitmap.PixelFormat);
					tiles[i++ + 112] = overlayBitmap.Clone(new Rectangle(x * 32, y * 32, 32, 32), overlayBitmap.PixelFormat);
				}
		}

		public Tileset(byte[] tilesetData)
		{
			using (var stream = new MemoryStream(tilesetData))
			{
				BinaryFormatter f = new BinaryFormatter();
				tiles = (Bitmap[])f.Deserialize(stream);
			}
		}

		#endregion

		#region Methods

		public Bitmap GetBitmap(Tile tile)
		{
			if (tile > Tile.AndChipE)
			{
				var result = new Bitmap(32, 32);
				using (var g = Graphics.FromImage(result))
					g.FillRectangle(Brushes.Black, 0, 0, 32, 32);
				return result;
			}
			else if (tile >= Tile.AndBugN)
			{
				var result = new Bitmap(32, 32);
				using (var g = Graphics.FromImage(result))
				{
					g.FillRectangle(Brushes.Black, 0, 0, 32, 32);
					g.DrawImage(tiles[(int)tile + 16], 0, 0, 32, 32);
				}
				return result;
			}
			else if (tile >= Tile.OrBugN)
			{
				var result = new Bitmap(32, 32);
				using (var g = Graphics.FromImage(result))
				{
					g.FillRectangle(Brushes.White, 0, 0, 32, 32);
					g.DrawImage(tiles[(int)tile + 64], 0, 0, 32, 32);
				}
				return result;
			}
			else
				return tiles[(int)tile];
		}

		public void DrawBitmap(Graphics graphics, TileLocation location, Tile tile, int tileSize, bool transparent)
		{
			if (tile > Tile.AndChipE)
				graphics.FillRectangle(Brushes.Black, location.ToRectangle(tileSize));
			else if (tile >= Tile.AndBugN)
			{
				graphics.FillRectangle(Brushes.Black, location.ToRectangle(tileSize));
				graphics.DrawImage(tiles[(int)tile + 16], location.ToRectangle(tileSize));
			}
			else if (tile >= Tile.OrBugN)
			{
				graphics.FillRectangle(Brushes.White, location.ToRectangle(tileSize));
				graphics.DrawImage(tiles[(int)tile + 64], location.ToRectangle(tileSize));
			}
			else
				graphics.DrawImage(tiles[transparent ? (int)tile + 112 : (int)tile], location.ToRectangle(tileSize));
		}

		public void DrawTile(Graphics graphics, TileLocation location, Tile upperTile, Tile lowerTile, LayerMode layerMode, int tileSize)
		{
			if ((layerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer)
				DrawBitmap(graphics, location, lowerTile, tileSize, false);
			if ((layerMode & LayerMode.UpperLayer) == LayerMode.UpperLayer)
				DrawBitmap(graphics, location, upperTile, tileSize, lowerTile != Tile.Floor && (layerMode & LayerMode.LowerLayer) == LayerMode.LowerLayer);
		}

		#endregion
	}
}
