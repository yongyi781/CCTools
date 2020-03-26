using System.Drawing;

namespace CCTools.CCDesign
{
    public class TileLocationMarker : HighlightMarker
    {
        public TileLocationMarker(TileLocation location, Pen pen)
        {
            this.location = location;
            this.Pen = pen;
        }

        private TileLocation location;
        public TileLocation Location
        {
            get { return location; }
        }

        public Pen Pen { get; }

        public static bool operator ==(TileLocationMarker left, TileLocationMarker right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TileLocationMarker left, TileLocationMarker right)
        {
            return !Equals(left, right);
        }

        public static bool Equals(TileLocationMarker left, TileLocationMarker right)
        {
            return left.location == right.location && left.Pen == right.Pen;
        }

        public override bool Equals(object obj)
        {
            var tileLocationMarker = obj as TileLocationMarker;
            return tileLocationMarker != null && Equals(this, tileLocationMarker);
        }

        public override int GetHashCode()
        {
            return location.GetHashCode() ^ Pen.GetHashCode();
        }

        public override void Invalidate(LevelMapEditor editor)
        {
            editor.Invalidate(location);
        }

        public override void Draw(Graphics graphics, int tileSize)
        {
            var rect = location.ToRectangle(tileSize);
            rect.Inflate(-1, -1);
            graphics.DrawRectangle(Pen, rect);
        }
    }
}
