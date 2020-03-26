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
            Index = index;
        }

        public RemoveTrapConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

        private TileConnection connection;
        public TileConnection Connection
        {
            get { return connection; }
        }

        public int Index { get; } = -1;

        public override string Name
        {
            get { return "Add Trap Connection"; }
        }

        public override void Do()
        {
            if (Index > -1 && Index < Owner.Level.TrapConnections.Count)
            {
                connection = Owner.Level.TrapConnections[Index];
                Owner.Level.TrapConnections.RemoveAt(Index);
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
