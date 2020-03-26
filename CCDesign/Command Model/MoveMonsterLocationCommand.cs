namespace CCTools.CCDesign
{
    public class MoveMonsterLocationCommand : Command
    {
        public MoveMonsterLocationCommand(int oldIndex, int newIndex)
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
            if (OldIndex > -1 && OldIndex < Owner.Level.MonsterLocations.Count && NewIndex > -1 && NewIndex < Owner.Level.MonsterLocations.Count)
                Owner.Level.MonsterLocations.Move(OldIndex, NewIndex);
        }

        public override void Undo()
        {
            if (OldIndex > -1 && OldIndex < Owner.Level.MonsterLocations.Count && NewIndex > -1 && NewIndex < Owner.Level.MonsterLocations.Count)
                Owner.Level.MonsterLocations.Move(NewIndex, OldIndex);
        }
    }
}
