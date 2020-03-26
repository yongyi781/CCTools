using System;
using System.Globalization;

namespace CCTools
{
    [Serializable]
    public struct TileConnection
    {
        public static readonly TileConnection Invalid = new TileConnection(TileLocation.Invalid, TileLocation.Invalid);

        public TileConnection(TileLocation source, TileLocation destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public TileConnection(int sourceX, int sourceY, int destinationX, int destinationY)
        {
            source = new TileLocation(sourceX, sourceY);
            destination = new TileLocation(destinationX, destinationY);
        }

        private TileLocation source;
        public TileLocation Source
        {
            get { return source; }
            set { source = value; }
        }

        private TileLocation destination;
        public TileLocation Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public static bool operator ==(TileConnection left, TileConnection right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TileConnection left, TileConnection right)
        {
            return !Equals(left, right);
        }

        public static bool Equals(TileConnection left, TileConnection right)
        {
            return left.source == right.source && left.destination == right.destination;
        }

        public override bool Equals(object obj)
        {
            return obj is TileConnection && Equals(this, (TileConnection)obj);
        }

        public override int GetHashCode()
        {
            return source.GetHashCode() ^ destination.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", source, destination);
        }
    }
}
