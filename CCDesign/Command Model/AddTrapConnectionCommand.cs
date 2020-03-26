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
            Index = index;
            this.connection = connection;
        }

        public AddTrapConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

        public AddTrapConnectionCommand(int index, TileLocation source, TileLocation destination) : this(index, new TileConnection(source, destination)) { }

        public int Index { get; } = -1;

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
            if (Index > -1 && Index < Owner.Level.TrapConnections.Count)
                Owner.Level.TrapConnections.Insert(Index, connection);
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
