using System;

namespace CCTools.CCDesign
{
	public class RemoveTrapConnectionCommand : Command
	{
		public RemoveTrapConnectionCommand(TileConnection connection)
		{
			this.connection = connection;
		}

		public RemoveTrapConnectionCommand(int index)
		{
			this.index = index;
		}

		public RemoveTrapConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

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
			get { return "Add Trap Connection"; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.TrapConnections.Count)
			{
				connection = Owner.Level.TrapConnections[index];
				Owner.Level.TrapConnections.RemoveAt(index);
			}
			else
				Owner.Level.TrapConnections.Remove(connection);
			Owner.Invalidate(connection);
			foreach (var trapConnection in Owner.Level.TrapConnections)
				Owner.Invalidate(trapConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			Owner.Level.TrapConnections.Add(connection);
			foreach (var trapConnection in Owner.Level.TrapConnections)
				Owner.Invalidate(trapConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
