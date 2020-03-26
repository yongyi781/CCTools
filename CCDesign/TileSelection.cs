using System.Collections.Generic;
using System.Drawing;

namespace CCTools.CCDesign
{
    public class TileSelection
    {
        private readonly List<TileRectangle> _rectangles = new List<TileRectangle>();

        public TileSelection() { }
        public TileSelection(IEnumerable<TileRectangle> collection)
        {
            _rectangles.AddRange(collection);
        }

        public void AddRectangle(TileRectangle rect)
        {
            _rectangles.Add(rect);
        }

        public void ClampToBounds()
        {
            for (int i = 0; i < _rectangles.Count; i++)
            {
                var rectangle = _rectangles[i];
                if (rectangle.X < 0)
                    rectangle.X = 0;
                if (rectangle.Y < 0)
                    rectangle.Y = 0;
                if (rectangle.Right > 31)
                    rectangle.Width = 32 - rectangle.X;
                if (rectangle.Bottom > 31)
                    rectangle.Height = 32 - rectangle.Y;
                _rectangles[i] = rectangle;
            }
        }

        public Region ToRegion(int tileSize)
        {
            var r = new Region();
            foreach (var rectangle in _rectangles)
                r.Union(rectangle.ToRectangle(tileSize));
            return r;
        }
    }
}
