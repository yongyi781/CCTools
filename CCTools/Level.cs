using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace CCTools
{
	[Serializable]
	public sealed class Level : ICloneable, INotifyPropertyChanged
	{
		public const string ChipEditMapSectFormat = "CHIPEDIT MAPSECT";
		public const string LevelDataFormat = "CC Level Data";

		const ushort FIELD_MAPDETAIL = 1;
		const byte
			FIELD_TITLE = 3,
			FIELD_TRAPCONNECTIONS = 4,
			FIELD_CLONECONNECTIONS = 5,
			FIELD_PASSWORD = 6,
			FIELD_HINT = 7,
			FIELD_MONSTERLOCATIONS = 10;

		#region Constructor

		public Level()
		{
			upperLayer.Changed += new EventHandler(upperLayer_Changed);
			lowerLayer.Changed += new EventHandler(lowerLayer_Changed);
			trapConnections.ListChanged += new ListChangedEventHandler(trapConnections_ListChanged);
			cloneConnections.ListChanged += new ListChangedEventHandler(cloneConnections_ListChanged);
			monsterLocations.ListChanged += new ListChangedEventHandler(monsterLocations_ListChanged);
		}

		#endregion

		#region Events

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		internal void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
			if (parent != null && e.PropertyName != "UpperLayer" && e.PropertyName != "LowerLayer")
				parent.OnLevelChanged(this);
		}

		#endregion

		#endregion

		#region Properties

		public int Index
		{
			get { return parent == null ? -1 : parent.IndexOf(this); }
		}

		private int timeLimit;
		public int TimeLimit
		{
			get { return timeLimit; }
			set
			{
				if (value < 0 || value > 65535)
					throw new ArgumentOutOfRangeException("value");
				if (timeLimit != value)
				{
					timeLimit = value;
					OnPropertyChanged(new PropertyChangedEventArgs("TimeLimit"));
				}
			}
		}

		private int chipCount;
		public int ChipCount
		{
			get { return chipCount; }
			set
			{
				if (value < 0 || value > 65535)
					throw new ArgumentOutOfRangeException("value");
				if (chipCount != value)
				{
					chipCount = value;
					OnPropertyChanged(new PropertyChangedEventArgs("ChipCount"));
				}
			}
		}

		private TileMap upperLayer = new TileMap();
		public TileMap UpperLayer
		{
			get { return upperLayer; }
		}

		private TileMap lowerLayer = new TileMap();
		public TileMap LowerLayer
		{
			get { return lowerLayer; }
		}

		private TileConnectionCollection trapConnections = new TileConnectionCollection(25);
		public TileConnectionCollection TrapConnections
		{
			get { return trapConnections; }
		}

		private TileConnectionCollection cloneConnections = new TileConnectionCollection(31);
		public TileConnectionCollection CloneConnections
		{
			get { return cloneConnections; }
		}

		private string title;
		public string Title
		{
			get { return title; }
			set
			{
				if (value != null && value.Length >= 255)
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLess, "Title length", 255));
				value = Encoding.Default.GetString(Encoding.Default.GetBytes(value));
				if (title != value)
				{
					title = value;
					OnPropertyChanged(new PropertyChangedEventArgs("Title"));
				}
			}
		}

		private string password;
		public string Password
		{
			get { return password; }
			set
			{
				if (value != null && value.Length > 9)
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLessOrEqual, "Password length", 9));
				value = Encoding.Default.GetString(Encoding.Default.GetBytes(value));
				if (password != value)
				{
					password = value;
					OnPropertyChanged(new PropertyChangedEventArgs("Password"));
				}
			}
		}

		private string hint;
		public string Hint
		{
			get { return hint; }
			set
			{
				if (value != null && value.Length >= byte.MaxValue)
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLess, "Password", byte.MaxValue));
				value = Encoding.Default.GetString(Encoding.Default.GetBytes(value));
				if (hint != value)
				{
					hint = value;
					OnPropertyChanged(new PropertyChangedEventArgs("Hint"));
				}
			}
		}

		private TileLocationCollection monsterLocations = new TileLocationCollection(127);
		public TileLocationCollection MonsterLocations
		{
			get { return monsterLocations; }
		}

		internal LevelSet parent;
		public LevelSet Parent
		{
			get { return parent; }
		}

		#endregion

		#region Methods

		public void Draw(Graphics graphics, Tileset tileset, LayerMode layerMode, Rectangle rect, int tileSize)
		{
			foreach (var location in TileRectangle.AllTiles.ToArray())
				tileset.DrawTile(graphics, location, upperLayer[location], lowerLayer[location], layerMode, tileSize);
			if (tileset != Tileset.Preview && tileSize >= 32)
			{
				DrawConnections(graphics, trapConnections, Brushes.Brown, tileSize);
				DrawConnections(graphics, cloneConnections, Brushes.Red, tileSize);
			}
		}

		public void Draw(Graphics graphics, Tileset tileset, LayerMode layerMode, TileRectangle sourceRect, int tileSize)
		{
			for (int y = 0; y <= sourceRect.Height; y++)
				for (int x = 0; x <= sourceRect.Width; x++)
					tileset.DrawTile(graphics, new TileLocation(x, y), upperLayer[x + sourceRect.X, y + sourceRect.Y], lowerLayer[x + sourceRect.X, y + sourceRect.Y], layerMode, tileSize);
		}

		public static string GenerateRandomPassword(int length)
		{
			var chArray = new char[length];
			var random = new Random();
			for (int i = 0; i < length; i++)
				chArray[i] = (char)random.Next('A', '[');
			return new string(chArray);
		}

		public TileLocation[] PopulateMonsters()
		{
			var list = new List<TileLocation>();
			for (int y = 0; y < 32; y++)
				for (int x = 0; x < 32; x++)
					if (TileUtilities.IsMonster(upperLayer[x, y], lowerLayer[x, y]))
						list.Add(new TileLocation(x, y));
			return list.ToArray();
		}

		public override string ToString()
		{
			return parent != null ? string.Format(CultureInfo.CurrentCulture, "{0} - {1}", parent.IndexOf(this) + 1, title) : title;
		}

		public void SurroundWithWalls()
		{
			for (int i = 0; i < 32; i++)
			{
				upperLayer[i, 0] = Tile.Wall;
				upperLayer[i, 31] = Tile.Wall;
				upperLayer[0, i] = Tile.Wall;
				upperLayer[31, i] = Tile.Wall;
			}
		}

		#region ICloneable Members

		public Level Clone()
		{
			return new Level
			{
				timeLimit = timeLimit,
				chipCount = chipCount,
				title = title,
				password = password,
				hint = hint,
				upperLayer = upperLayer.Clone(),
				lowerLayer = lowerLayer.Clone(),
				monsterLocations = monsterLocations.Clone(),
				trapConnections = trapConnections.Clone(),
				cloneConnections = cloneConnections.Clone()
			};
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		public void Read(BinaryReader reader)
		{
			var offsetToNextLevel = reader.ReadUInt16() + reader.BaseStream.Position;
			reader.BaseStream.Seek(sizeof(ushort), SeekOrigin.Current);		// Level number
			timeLimit = reader.ReadUInt16();
			chipCount = reader.ReadUInt16();
			var paramsRead = new bool[7];
			while (reader.BaseStream.Position < offsetToNextLevel)
			{
				var field = reader.ReadByte();

				if (!paramsRead[0] && field == FIELD_MAPDETAIL)
				{
					reader.BaseStream.Seek(sizeof(byte), SeekOrigin.Current);	// Field is WORD for some reason; skip a byte
					upperLayer.Read(reader);
					lowerLayer.Read(reader);
					reader.BaseStream.Seek(sizeof(ushort), SeekOrigin.Current);	// Skip 'bytes to end of level'
					paramsRead[0] = true;
				}
				else if (!paramsRead[1] && field == FIELD_TITLE)
				{
					var length = reader.ReadByte();
					title = new string(reader.ReadChars(length), 0, length - 1);
					paramsRead[1] = true;
				}
				else if (!paramsRead[2] && field == FIELD_TRAPCONNECTIONS)
				{
					var length = reader.ReadByte();
					var buffer = reader.ReadBytes(length);
					trapConnections.Clear();
					for (int i = 0; i < length; i += 10)
						trapConnections.Add(new TileConnection(buffer[i], buffer[i + 2], buffer[i + 4], buffer[i + 6]));
					paramsRead[2] = true;
				}
				else if (!paramsRead[3] && field == FIELD_CLONECONNECTIONS)
				{
					var length = reader.ReadByte();
					var buffer = reader.ReadBytes(length);
					cloneConnections.Clear();
					for (int i = 0; i < length; i += 8)
						cloneConnections.Add(new TileConnection(buffer[i], buffer[i + 2], buffer[i + 4], buffer[i + 6]));
					paramsRead[3] = true;
				}
				else if (!paramsRead[4] && field == FIELD_PASSWORD)
				{
					var buffer = reader.ReadBytes(reader.ReadByte());
					password = DecodePassword(buffer);
					paramsRead[4] = true;
				}
				else if (!paramsRead[5] && field == FIELD_HINT)
				{
					var length = reader.ReadByte();
					hint = new string(reader.ReadChars(length), 0, length - 1);
					paramsRead[5] = true;
				}
				else if (!paramsRead[6] && field == FIELD_MONSTERLOCATIONS)
				{
					var length = reader.ReadByte();
					var buffer = reader.ReadBytes(length);
					monsterLocations.Clear();
					for (int i = 0; i < length; i += 2)
						monsterLocations.Add(new TileLocation(buffer[i], buffer[i + 1]));
					paramsRead[6] = true;
				}
			}
		}

		public void Write(BinaryWriter writer, int levelIndex)
		{
			if (title != null && title.Length >= 255)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLess, "Title length", 255));
			if (hint != null && hint.Length >= 255)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLess, "Hint length", 255));
			if (password != null && password.Length > 9)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLessOrEqual, "Password length", 9));
			if (trapConnections.Count > 25)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLessOrEqual, "Trap connection count", 25));
			if (cloneConnections.Count > 31)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLessOrEqual, "Clone connection count", 31));
			if (monsterLocations.Count > 127)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLessOrEqual, "Monster location count", 127));

			var upperLayerBytes = upperLayer.GetBytes();
			var lowerLayerBytes = lowerLayer.GetBytes();
			var titleBytes = GetBytes(title);
			var hintBytes = GetBytes(hint);
			var passwordBytes = EncodePasswordWithLength(password);

			var trapConnectionsByteCount = trapConnections.Count;
			trapConnectionsByteCount = trapConnectionsByteCount + trapConnectionsByteCount + trapConnectionsByteCount + trapConnectionsByteCount + trapConnectionsByteCount;
			trapConnectionsByteCount += trapConnectionsByteCount;

			var trapConnectionsBytes = new byte[trapConnectionsByteCount + 1];
			trapConnectionsBytes[0] = (byte)trapConnectionsByteCount;
			for (int i = 0; i < trapConnections.Count; i++)
			{
				var offset = i + i + i + i + i;
				offset += offset + 1;
				var connection = trapConnections[i];
				var source = connection.Source;
				var destination = connection.Destination;
				trapConnectionsBytes[offset] = (byte)source.X;
				trapConnectionsBytes[offset + 2] = (byte)source.Y;
				trapConnectionsBytes[offset + 4] = (byte)destination.X;
				trapConnectionsBytes[offset + 6] = (byte)destination.Y;
			}

			var cloneConnectionsByteCount = cloneConnections.Count;
			cloneConnectionsByteCount += cloneConnectionsByteCount;
			cloneConnectionsByteCount += cloneConnectionsByteCount;
			cloneConnectionsByteCount += cloneConnectionsByteCount;
			var cloneConnectionsBytes = new byte[cloneConnectionsByteCount + 1];
			cloneConnectionsBytes[0] = (byte)cloneConnectionsByteCount;
			for (int i = 0; i < cloneConnections.Count; i++)
			{
				var offset = i + i;
				offset += offset;
				offset += offset + 1;
				var connection = cloneConnections[i];
				var source = connection.Source;
				var destination = connection.Destination;
				cloneConnectionsBytes[offset] = (byte)source.X;
				cloneConnectionsBytes[offset + 2] = (byte)source.Y;
				cloneConnectionsBytes[offset + 4] = (byte)destination.X;
				cloneConnectionsBytes[offset + 6] = (byte)destination.Y;
			}

			var monsterLocationsByteCount = monsterLocations.Count;
			monsterLocationsByteCount += monsterLocationsByteCount;
			var monsterLocationsBytes = new byte[monsterLocationsByteCount + 1];
			monsterLocationsBytes[0] = (byte)monsterLocationsByteCount;
			for (int i = 0; i < monsterLocations.Count; i++)
			{
				var offset = i + i + 1;
				var location = monsterLocations[i];
				monsterLocationsBytes[offset] = (byte)location.X;
				monsterLocationsBytes[offset + 1] = (byte)location.Y;
			}

			var topLength = (ushort)(upperLayerBytes.Length + lowerLayerBytes.Length + 14);
			var offsetToNextLevel = topLength;
			if (titleBytes.Length > 1)
				offsetToNextLevel += (ushort)(titleBytes.Length + 1);
			if (hintBytes.Length > 1)
				offsetToNextLevel += (ushort)(hintBytes.Length + 1);
			if (passwordBytes.Length > 1)
				offsetToNextLevel += (ushort)(passwordBytes.Length + 1);
			if (trapConnectionsBytes.Length > 1)
				offsetToNextLevel += (ushort)(trapConnectionsBytes.Length + 1);
			if (cloneConnectionsBytes.Length > 1)
				offsetToNextLevel += (ushort)(cloneConnectionsBytes.Length + 1);
			if (monsterLocationsBytes.Length > 1)
				offsetToNextLevel += (ushort)(monsterLocationsBytes.Length + 1);

			writer.Write(offsetToNextLevel);

			writer.Write((ushort)(levelIndex + 1));
			writer.Write((ushort)timeLimit);
			writer.Write((ushort)chipCount);

			writer.Write(FIELD_MAPDETAIL);
			writer.Write((ushort)upperLayerBytes.Length);
			writer.Write(upperLayerBytes);
			writer.Write((ushort)lowerLayerBytes.Length);
			writer.Write(lowerLayerBytes);

			writer.Write((ushort)(offsetToNextLevel - topLength));

			if (titleBytes.Length > 1)
			{
				writer.Write(FIELD_TITLE);
				writer.Write(titleBytes);
			}
			if (hintBytes.Length > 1)
			{
				writer.Write(FIELD_HINT);
				writer.Write(hintBytes);
			}
			if (passwordBytes.Length > 1)
			{
				writer.Write(FIELD_PASSWORD);
				writer.Write(passwordBytes);
			}
			if (trapConnectionsBytes.Length > 1)
			{
				writer.Write(FIELD_TRAPCONNECTIONS);
				writer.Write(trapConnectionsBytes);
			}
			if (cloneConnectionsBytes.Length > 1)
			{
				writer.Write(FIELD_CLONECONNECTIONS);
				writer.Write(cloneConnectionsBytes);
			}
			if (monsterLocationsBytes.Length > 1)
			{
				writer.Write(FIELD_MONSTERLOCATIONS);
				writer.Write(monsterLocationsBytes);
			}
		}

		static byte[] GetBytes(string value)
		{
			if (string.IsNullOrEmpty(value))
				return new byte[0];

			var bytes = Encoding.Default.GetBytes(value);
			if (bytes.Length >= byte.MaxValue)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Argument_MustBeLess, "String", byte.MaxValue), "value");
			var result = new byte[bytes.Length + 2];
			result[0] = (byte)(bytes.Length + 1);
			for (int i = 0; i < bytes.Length; i++)
				result[i + 1] = bytes[i];
			return result;
		}

		static string DecodePassword(byte[] bytes)
		{
			if (bytes == null || bytes.Length <= 0)
				return string.Empty;

			var array = new byte[bytes.Length - 1];
			for (int i = 0; i < array.Length; i++)
				array[i] = bytes[i];
			var chars = Encoding.Default.GetChars(array);
			for (int i = 0; i < chars.Length; i++)
				chars[i] ^= '';
			return new string(chars);
		}

		static byte[] EncodePasswordWithLength(string value)
		{
			if (string.IsNullOrEmpty(value))
				return new byte[0];

			var chars = value.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
				chars[i] ^= '';
			var bytes = Encoding.Default.GetBytes(chars);
			var result = new byte[bytes.Length + 2];
			result[0] = (byte)(bytes.Length + 1);
			for (int i = 0; i < bytes.Length; i++)
				result[i + 1] = bytes[i];
			return result;
		}

		private void DrawConnections(Graphics graphics, TileConnectionCollection collection, Brush fillBrush, int tileSize)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				var source = collection[i].Source;
				var destination = collection[i].Destination;

				Rectangle sourceStringRect;
				Rectangle destinationStringRect;
				if (i >= 9)
				{
					sourceStringRect = new Rectangle((source.X + 1) * tileSize - 16, (source.Y + 1) * tileSize - 12, 15, 10);
					destinationStringRect = new Rectangle((destination.X + 1) * tileSize - 16, (destination.Y + 1) * tileSize - 12, 15, 10);
				}
				else
				{
					sourceStringRect = new Rectangle((source.X + 1) * tileSize - 14, (source.Y + 1) * tileSize - 12, 10, 10);
					destinationStringRect = new Rectangle((destination.X + 1) * tileSize - 14, (destination.Y + 1) * tileSize - 12, 10, 10);
				}
				graphics.FillEllipse(fillBrush, sourceStringRect);
				graphics.DrawString((i + 1).ToString(CultureInfo.CurrentCulture), new Font("Segoe UI", 6, FontStyle.Bold), Brushes.White, sourceStringRect.X + 2, sourceStringRect.Y);
				graphics.FillEllipse(fillBrush, destinationStringRect);
				graphics.DrawString((i + 1).ToString(CultureInfo.CurrentCulture), new Font("Segoe UI", 6, FontStyle.Bold), Brushes.White, destinationStringRect.X + 2, destinationStringRect.Y);
			}
		}

		private void cloneConnections_ListChanged(object sender, ListChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("CloneConnections"));
		}

		private void lowerLayer_Changed(object sender, EventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("LowerLayer"));
		}

		private void monsterLocations_ListChanged(object sender, ListChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("MonsterLocations"));
		}

		private void upperLayer_Changed(object sender, EventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("UpperLayer"));
		}

		private void trapConnections_ListChanged(object sender, ListChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("TrapConnections"));
		}

		#endregion
	}
}
