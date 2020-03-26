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
			this.Index = index;
			this.connection = connection;
		}

		public AddCloneConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

		public AddCloneConnectionCommand(int index, TileLocation source, TileLocation destination) : this(index, new TileConnection(source, destination)) { }

		public int Index { get; } = -1;

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
			if (Index > -1 && Index < Owner.Level.CloneConnections.Count)
				Owner.Level.CloneConnections.Insert(Index, connection);
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
