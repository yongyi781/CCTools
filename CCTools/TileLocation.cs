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
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static TileLocation operator +(TileLocation left, TileLocation right)
        {
            return new TileLocation(left.X + right.X, left.Y + right.Y);
        }

        public static TileLocation operator -(TileLocation left, TileLocation right)
        {
            return new TileLocation(left.X - right.X, left.Y - right.Y);
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
            return left.X == right.X && left.Y == right.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is TileLocation && Equals(this, (TileLocation)obj);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public bool IsValid()
        {
            return X >= 0 && X < 32 && Y >= 0 && Y < 32;
        }

        public Rectangle ToRectangle(int tileSize)
        {
            return new Rectangle(X * tileSize, Y * tileSize, tileSize, tileSize);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", X, Y);
        }
    }
}
