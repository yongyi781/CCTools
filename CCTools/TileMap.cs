using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CCTools
{
    [Serializable]
    public sealed class TileMap : IEnumerable<Tile>, ICloneable
    {
        private readonly Tile[] _tiles = new Tile[1024];

        public TileMap() { }

        [CLSCompliant(false)]
        public unsafe TileMap(byte* buffer, ushort length)
        {
            for (int i = 0, position = 0; i < length; i++)
            {
                var b = *buffer++;
                if (b == 0xFF)
                {
                    i += 2;
                    var count = *buffer++;
                    var tile = *buffer++;
                    for (int j = 0; j < count; j++)
                        _tiles[position++] = (Tile)tile;
                }
                else
                    _tiles[position++] = (Tile)b;
            }
        }

        #region Events

        public event EventHandler Changed;
        internal void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        #endregion

        #region Properties

        public Tile this[int index]
        {
            get
            {
                if (index < 0 || index >= 1024)
                    return Tile.Floor;
                return _tiles[index];
            }
            set
            {
                if (index < 0 || index >= 1024)
                    return;
                var oldValue = _tiles[index];
                if (oldValue != value)
                {
                    _tiles[index] = value;
                    OnChanged(EventArgs.Empty);
                }
            }
        }

        public Tile this[int x, int y]
        {
            get { return this[x + y * 32]; }
            set { this[x + y * 32] = value; }
        }

        public Tile this[TileLocation location]
        {
            get { return this[location.X + location.Y * 32]; }
            set { this[location.X + location.Y * 32] = value; }
        }

        #endregion

        #region Methods

        public void CopyTo(TileMap map)
        {
            for (int i = 0; i < 1024; i++)
                map._tiles[i] = _tiles[i];
            map.OnChanged(EventArgs.Empty);
        }

        public int CountTiles(Tile tile)
        {
            int result = 0;
            for (int i = 0; i < 1024; i++)
                if (_tiles[i] == tile)
                    ++result;
            return result;
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                for (int i = 0; i < 1024; i++)
                {
                    var tile = _tiles[i];
                    int j;
                    for (j = 1; i + j < 1024 && _tiles[i + j] == tile; j++) { }
                    if (j > 3)
                    {
                        i += j - 1;
                        byte count = (byte)(j / 0xFF);
                        byte remainder = (byte)(j % 0xFF);
                        for (int k = 0; k < count; k++)
                            stream.Write(new byte[] { 0xFF, 0xFF, (byte)tile }, 0, 3);
                        if (remainder > 0)
                            stream.Write(new byte[] { 0xFF, remainder, (byte)tile }, 0, 3);
                    }
                    else
                        stream.WriteByte((byte)tile);
                }
                return stream.ToArray();
            }
        }

        public void Read(BinaryReader stream)
        {
            var length = stream.ReadUInt16();
            var buffer = stream.ReadBytes(length);

            for (int i = 0, position = 0; i < length; i++)
            {
                var b = buffer[i];
                if (b == 0xFF)
                {
                    var count = buffer[++i];
                    var tile = buffer[++i];
                    for (int j = 0; j < count; j++)
                        _tiles[position++] = (Tile)tile;
                }
                else
                    _tiles[position++] = (Tile)b;
            }

            OnChanged(EventArgs.Empty);
        }

        public TileLocation FindNextTeleport(TileLocation location)
        {
            var teleportCount = CountTiles(Tile.Teleport);
            if (teleportCount <= 0)
                return TileLocation.Invalid;
            if (teleportCount == 1 && this[location] == Tile.Teleport)
                return location;
            var index = location.X + (location.Y * 32);
            for (int i = index - 1; i >= 0; i--)
            {
                if (i == index)
                    return TileLocation.Invalid;
                if (_tiles[i] == Tile.Teleport)
                    return new TileLocation(i % 32, i / 32);
                if (i <= 0)
                    i = 1024;
            }
            return TileLocation.Invalid;
        }

        #region IEnumerable<Tile> Members

        public IEnumerator<Tile> GetEnumerator()
        {
            for (int i = 0; i < 1024; i++)
                yield return _tiles[i];
        }

        #endregion

        #region ICloneable Members

        public TileMap Clone()
        {
            var result = new TileMap();
            for (int i = 0; i < 1024; i++)
                result._tiles[i] = _tiles[i];
            return result;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        #endregion

        #endregion
    }
}
