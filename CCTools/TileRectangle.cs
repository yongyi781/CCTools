using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

namespace CCTools
{
	[Serializable]
	public struct TileRectangle
	{
		public static readonly TileRectangle Empty = new TileRectangle();
		public static readonly TileRectangle AllTiles = new TileRectangle(0, 0, 32, 32);

		public TileRectangle(TileLocation location, int width, int height) : this(location.X, location.Y, width, height) { }

		public TileRectangle(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		private int x;
		public int X
		{
			get { return x; }
			set { x = value; }
		}

		private int y;
		public int Y
		{
			get { return y; }
			set { y = value; }
		}

		private int width;
		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		private int height;
		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		public int Left
		{
			get { return x; }
		}

		public int Right
		{
			get { return x + width - 1; }
		}

		public int Top
		{
			get { return y; }
		}

		public int Bottom
		{
			get { return y + height - 1; }
		}

		public TileRectangle ActualRectangle
		{
			get
			{
				int x, y, width, height;
				if (this.width < 0)
				{
					width = -this.width;
					x = this.x - width + 1;
				}
				else
				{
					width = this.width;
					x = this.x;
				}
				if (this.height < 0)
				{
					height = -this.height;
					y = this.y - height + 1;
				}
				else
				{
					height = this.height;
					y = this.y;
				}
				return new TileRectangle(x, y, width, height);
			}
		}

		public bool IsEmpty
		{
			get { return width == 0 && height == 0; }
		}

		public static bool operator ==(TileRectangle left, TileRectangle right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TileRectangle left, TileRectangle right)
		{
			return !Equals(left, right);
		}

		public bool Contains(TileLocation location)
		{
			return Contains(location.X, location.Y);
		}

		public bool Contains(int x, int y)
		{
			return this.x <= x && x < this.x + width && this.y <= y && y < this.y + height;
		}

		public static bool Equals(TileRectangle left, TileRectangle right)
		{
			return left.x == right.x && left.y == right.y && left.width == right.width && left.height == right.height;
		}

		public override bool Equals(object obj)
		{
			return obj is TileRectangle && Equals(this, (TileRectangle)obj);
		}

		public override int GetHashCode()
		{
			return x ^ (y << 13 | y >> 19) ^ (width << 26 | width >> 6) ^ (height << 7 | height >> 25);
		}

		public static TileRectangle FromLTRB(int left, int top, int right, int bottom)
		{
			if (left > right && top > bottom)
				return new TileRectangle(left, top, right - left - 1, bottom - top - 1);
			if (left > right)
				return new TileRectangle(left, top, right - left - 1, bottom - top + 1);
			if (top > bottom)
				return new TileRectangle(left, top, right - left + 1, bottom - top - 1);
			return new TileRectangle(left, top, right - left + 1, bottom - top + 1);
		}

		public static TileRectangle ScaleFromRectangle(Rectangle rect, int tileSize)
		{
			return TileRectangle.FromLTRB(rect.Left / tileSize, rect.Top / tileSize, (rect.Right - 1) / tileSize, (rect.Bottom - 1) / tileSize);
		}

		public Rectangle ToRectangle(int tileSize)
		{
			var rect = ActualRectangle;
			return new Rectangle(rect.x * tileSize, rect.y * tileSize, rect.width * tileSize, rect.height * tileSize);
		}

		public TileLocation[,] ToArray()
		{
			var result = new TileLocation[width, height];
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
					result[x, y] = new TileLocation(x + this.x, y + this.y);
			return result;
		}
	}
}
