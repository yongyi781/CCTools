using System;

namespace CCTools.CCDesign
{
	public class RemoveCloneConnectionCommand : Command
	{
		public RemoveCloneConnectionCommand(TileConnection connection)
		{
			this.connection = connection;
		}

		public RemoveCloneConnectionCommand(int index)
		{
			this.index = index;
		}

		public RemoveCloneConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

		private TileConnection connection;
		public TileConnection Connection
		{
			get { return connection; }
		}

		private int index = -1;
		public int Index
		{
			get { return index; }
		}

		public override string Name
		{
			get { return "Remove Clone Connection"; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.CloneConnections.Count)
			{
				connection = Owner.Level.CloneConnections[index];
				Owner.Level.CloneConnections.RemoveAt(index);
			}
			else
				Owner.Level.CloneConnections.Remove(connection);
			Owner.Invalidate(connection);
			foreach (var cloneConnection in Owner.Level.CloneConnections)
				Owner.Invalidate(cloneConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			Owner.Level.CloneConnections.Add(connection);
			foreach (var cloneConnection in Owner.Level.CloneConnections)
				Owner.Invalidate(cloneConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
