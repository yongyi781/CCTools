using System;

namespace CCTools.CCDesign
{
	public class AddCloneConnectionCommand : Command
	{
		public AddCloneConnectionCommand(TileConnection connection)
		{
			this.connection = connection;
		}

		public AddCloneConnectionCommand(int index, TileConnection connection)
		{
			this.index = index;
			this.connection = connection;
		}

		public AddCloneConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

		public AddCloneConnectionCommand(int index, TileLocation source, TileLocation destination) : this(index, new TileConnection(source, destination)) { }

		private int index = -1;
		public int Index
		{
			get { return index; }
		}

		private TileConnection connection;
		public TileConnection Connection
		{
			get { return connection; }
		}

		public override string Name
		{
			get { return "Add Clone Connection"; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.CloneConnections.Count)
				Owner.Level.CloneConnections.Insert(index, connection);
			else
				Owner.Level.CloneConnections.Add(connection);
			foreach (var cloneConnection in Owner.Level.CloneConnections)
				Owner.Invalidate(cloneConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			if (Owner.Level.CloneConnections.Remove(connection))
			{
				Owner.Invalidate(connection);
				foreach (var cloneConnection in Owner.Level.CloneConnections)
					Owner.Invalidate(cloneConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
