using System.Drawing;

namespace CCTools.CCDesign
{
	public class TileConnectionMarker : HighlightMarker
	{
		public TileConnectionMarker(TileConnection connection, Pen sourcePen, Pen destinationPen, Pen linePen)
		{
			this.connection = connection;
			this.sourcePen = sourcePen;
			this.destinationPen = destinationPen;
			this.linePen = linePen;
		}

		private TileConnection connection;
		public TileConnection Connection
		{
			get { return connection; }
		}

		private Pen sourcePen;
		public Pen SourcePen
		{
			get { return sourcePen; }
		}

		private Pen destinationPen;
		public Pen DestinationPen
		{
			get { return destinationPen; }
		}

		private Pen linePen;
		public Pen LinePen
		{
			get { return linePen; }
		}

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
			return left.connection == right.connection && left.sourcePen == right.sourcePen && left.destinationPen == right.destinationPen && left.linePen == right.linePen;
		}

		public override bool Equals(object obj)
		{
			return obj is TileConnectionMarker && Equals(this, (TileConnectionMarker)obj);
		}

		public override int GetHashCode()
		{
			return connection.GetHashCode() ^ sourcePen.GetHashCode() ^ destinationPen.GetHashCode() ^ linePen.GetHashCode();
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
			graphics.DrawRectangle(sourcePen, sourceRect);
			graphics.DrawRectangle(destinationPen, destinationRect);
			graphics.DrawLine(linePen, sourcePoint, destinationPoint);
		}
	}
}
