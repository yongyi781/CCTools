using System;
using System.Globalization;
using System.Drawing;

namespace CCTools
{
	[Serializable]
	public struct TileLocation
	{
		public static readonly TileLocation Invalid = new TileLocation(-1, -1);

		public TileLocation(int x, int y)
		{
			this.x = x;
			this.y = y;
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

		public static TileLocation operator +(TileLocation left, TileLocation right)
		{
			return new TileLocation(left.x + right.x, left.y + right.y);
		}

		public static TileLocation operator -(TileLocation left, TileLocation right)
		{
			return new TileLocation(left.x - right.x, left.y - right.y);
		}

		public static bool operator ==(TileLocation left, TileLocation right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TileLocation left, TileLocation right)
		{
			return !Equals(left, right);
		}

		public static bool Equals(TileLocation left, TileLocation right)
		{
			return left.x == right.x && left.y == right.y;
		}

		public override bool Equals(object obj)
		{
			return obj is TileLocation && Equals(this, (TileLocation)obj);
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		public bool IsValid()
		{
			return x >= 0 && x < 32 && y >= 0 && y < 32;
		}

		public Rectangle ToRectangle(int tileSize)
		{
			return new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", x, y);
		}
	}
}
