using System;

namespace CCTools.CCDesign
{
	public class AddTrapConnectionCommand : Command
	{
		public AddTrapConnectionCommand(TileConnection connection)
		{
			this.connection = connection;
		}

		public AddTrapConnectionCommand(int index, TileConnection connection)
		{
			this.index = index;
			this.connection = connection;
		}

		public AddTrapConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

		public AddTrapConnectionCommand(int index, TileLocation source, TileLocation destination) : this(index, new TileConnection(source, destination)) { }

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
			get { return "Add Trap Connection"; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.TrapConnections.Count)
				Owner.Level.TrapConnections.Insert(index, connection);
			else
				Owner.Level.TrapConnections.Add(connection);
			foreach (var trapConnection in Owner.Level.TrapConnections)
				Owner.Invalidate(trapConnection);
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			if (Owner.Level.TrapConnections.Remove(connection))
			{
				Owner.Invalidate(connection);
				foreach (var trapConnection in Owner.Level.TrapConnections)
					Owner.Invalidate(trapConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
