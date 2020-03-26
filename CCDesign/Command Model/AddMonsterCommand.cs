namespace CCTools.CCDesign
{
    public class AddMonsterCommand : Command
    {
        public AddMonsterCommand(TileLocation location)
        {
            this.location = location;
        }

        public AddMonsterCommand(int index, TileLocation location)
        {
            Index = index;
            this.location = location;
        }

        public override string Name
        {
            get { return "Add Monster"; }
        }

        public int Index { get; } = -1;

        private TileLocation location;
        public TileLocation Location
        {
            get { return location; }
        }

        public override void Do()
        {
            if (Index > -1 && Index < Owner.Level.MonsterLocations.Count)
                Owner.Level.MonsterLocations.Insert(Index, location);
            else
                Owner.Level.MonsterLocations.Add(location);
        }

        public override void Undo()
        {
            Owner.Level.MonsterLocations.Remove(location);
        }
    }
}
