using System;

namespace CCTools.CCDesign
{
	public class RemoveMonsterCommand : Command
	{
		public RemoveMonsterCommand(TileLocation location)
		{
			this.location = location;
		}

		public RemoveMonsterCommand(int index)
		{
			this.Index = index;
		}

		public override string Name
		{
			get { return "Remove Monster"; }
		}

		private TileLocation location;
		public TileLocation Location
		{
			get { return location; }
		}

		public int Index { get; } = -1;

		public override void Do()
		{
			if (Index > -1 && Index < Owner.Level.MonsterLocations.Count)
			{
				location = Owner.Level.MonsterLocations[Index];
				Owner.Level.MonsterLocations.RemoveAt(Index);
			}
			else
				Owner.Level.MonsterLocations.Remove(location);
		}

		public override void Undo()
		{
			Owner.Level.MonsterLocations.Add(location);
		}
	}
}
