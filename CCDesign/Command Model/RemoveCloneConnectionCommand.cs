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
            Index = index;
        }

        public RemoveCloneConnectionCommand(TileLocation source, TileLocation destination) : this(new TileConnection(source, destination)) { }

        private TileConnection connection;
        public TileConnection Connection
        {
            get { return connection; }
        }

        public int Index { get; } = -1;

        public override string Name
        {
            get { return "Remove Clone Connection"; }
        }

        public override void Do()
        {
            if (Index > -1 && Index < Owner.Level.CloneConnections.Count)
            {
                connection = Owner.Level.CloneConnections[Index];
                Owner.Level.CloneConnections.RemoveAt(Index);
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
