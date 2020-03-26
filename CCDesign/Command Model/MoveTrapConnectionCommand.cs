namespace CCTools.CCDesign
{
    public class MoveTrapConnectionCommand : Command
    {
        public MoveTrapConnectionCommand(int oldIndex, int newIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public int OldIndex { get; } = -1;
        public int NewIndex { get; } = -1;

        public override string Name
        {
            get { return "Add Trap Connection"; }
        }

        public override void Do()
        {
            if (OldIndex > -1 && OldIndex < Owner.Level.TrapConnections.Count && NewIndex > -1 && NewIndex < Owner.Level.TrapConnections.Count)
            {
                Owner.Level.TrapConnections.Move(OldIndex, NewIndex);
                foreach (var trapConnection in Owner.Level.TrapConnections)
                    Owner.Invalidate(trapConnection);
            }
            Owner.UpdateTileCoordinatesAndHighlights();
        }

        public override void Undo()
        {
            if (OldIndex > -1 && OldIndex < Owner.Level.TrapConnections.Count && NewIndex > -1 && NewIndex < Owner.Level.TrapConnections.Count)
            {
                Owner.Level.TrapConnections.Move(NewIndex, OldIndex);
                foreach (var trapConnection in Owner.Level.TrapConnections)
                    Owner.Invalidate(trapConnection);
            }
            Owner.UpdateTileCoordinatesAndHighlights();
        }
    }
}
