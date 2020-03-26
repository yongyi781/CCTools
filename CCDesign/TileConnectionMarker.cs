using System.Drawing;

namespace CCTools.CCDesign
{
    public class TileConnectionMarker : HighlightMarker
    {
        public TileConnectionMarker(TileConnection connection, Pen sourcePen, Pen destinationPen, Pen linePen)
        {
            this.connection = connection;
            this.SourcePen = sourcePen;
            this.DestinationPen = destinationPen;
            this.LinePen = linePen;
        }

        private TileConnection connection;
        public TileConnection Connection
        {
            get { return connection; }
        }

        public Pen SourcePen { get; }
        public Pen DestinationPen { get; }
        public Pen LinePen { get; }

        public static bool operator ==(TileConnectionMarker left, TileConnectionMarker right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TileConnectionMarker left, TileConnectionMarker right)
        {
            return !Equals(left, right);
        }

        public static bool Equals(TileConnectionMarker left, TileConnectionMarker right)
        {
            return left.connection == right.connection && left.SourcePen == right.SourcePen && left.DestinationPen == right.DestinationPen && left.LinePen == right.LinePen;
        }

        public override bool Equals(object obj)
        {
            return obj is TileConnectionMarker && Equals(this, (TileConnectionMarker)obj);
        }

        public override int GetHashCode()
        {
            return connection.GetHashCode() ^ SourcePen.GetHashCode() ^ DestinationPen.GetHashCode() ^ LinePen.GetHashCode();
        }

        public override void Invalidate(LevelMapEditor editor)
        {
            editor.Invalidate(Rectangle.Union(connection.Source.ToRectangle(editor.TileSize), connection.Destination.ToRectangle(editor.TileSize)));
        }

        public override void Draw(Graphics graphics, int tileSize)
        {
            var sourceRect = connection.Source.ToRectangle(tileSize);
            var destinationRect = connection.Destination.ToRectangle(tileSize);
            var sourcePoint = new Point((sourceRect.Left + sourceRect.Right) / 2, (sourceRect.Top + sourceRect.Bottom) / 2);
            var destinationPoint = new Point((destinationRect.Left + destinationRect.Right) / 2, (destinationRect.Top + destinationRect.Bottom) / 2);
            sourceRect.Inflate(-1, -1);
            destinationRect.Inflate(-1, -1);
            graphics.DrawRectangle(SourcePen, sourceRect);
            graphics.DrawRectangle(DestinationPen, destinationRect);
            graphics.DrawLine(LinePen, sourcePoint, destinationPoint);
        }
    }
}
