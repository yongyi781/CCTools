namespace CCTools.CCDesign
{
    public class MoveCloneConnectionCommand : Command
    {
        public MoveCloneConnectionCommand(int oldIndex, int newIndex)
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
            if (OldIndex > -1 && OldIndex < Owner.Level.CloneConnections.Count && NewIndex > -1 && NewIndex < Owner.Level.CloneConnections.Count)
            {
                Owner.Level.CloneConnections.Move(OldIndex, NewIndex);
                foreach (var cloneConnection in Owner.Level.CloneConnections)
                    Owner.Invalidate(cloneConnection);
            }
            Owner.UpdateTileCoordinatesAndHighlights();
        }

        public override void Undo()
        {
            if (OldIndex > -1 && OldIndex < Owner.Level.CloneConnections.Count && NewIndex > -1 && NewIndex < Owner.Level.CloneConnections.Count)
            {
                Owner.Level.CloneConnections.Move(NewIndex, OldIndex);
                foreach (var cloneConnection in Owner.Level.CloneConnections)
                    Owner.Invalidate(cloneConnection);
            }
            Owner.UpdateTileCoordinatesAndHighlights();
        }
    }
}
