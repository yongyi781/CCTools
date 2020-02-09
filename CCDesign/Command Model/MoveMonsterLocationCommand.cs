namespace CCTools.CCDesign
{
	public class MoveMonsterLocationCommand : Command
	{
		public MoveMonsterLocationCommand(int oldIndex, int newIndex)
		{
			this.oldIndex = oldIndex;
			this.newIndex = newIndex;
		}

		private int oldIndex = -1;
		public int OldIndex
		{
			get { return oldIndex; }
		}

		private int newIndex = -1;
		public int NewIndex
		{
			get { return newIndex; }
		}

		public override string Name
		{
			get { return "Add Trap Connection"; }
		}

		public override void Do()
		{
			if (oldIndex > -1 && oldIndex < Owner.Level.MonsterLocations.Count && newIndex > -1 && newIndex < Owner.Level.MonsterLocations.Count)
				Owner.Level.MonsterLocations.Move(oldIndex, newIndex);
		}

		public override void Undo()
		{
			if (oldIndex > -1 && oldIndex < Owner.Level.MonsterLocations.Count && newIndex > -1 && newIndex < Owner.Level.MonsterLocations.Count)
				Owner.Level.MonsterLocations.Move(newIndex, oldIndex);
		}
	}
}
