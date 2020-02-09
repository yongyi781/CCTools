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
			this.index = index;
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

		private int index = -1;
		public int Index
		{
			get { return index; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.MonsterLocations.Count)
			{
				location = Owner.Level.MonsterLocations[index];
				Owner.Level.MonsterLocations.RemoveAt(index);
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
