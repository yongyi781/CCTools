using System;

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
			this.index = index;
			this.location = location;
		}

		public override string Name
		{
			get { return "Add Monster"; }
		}

		private int index = -1;
		public int Index
		{
			get { return index; }
		}

		private TileLocation location;
		public TileLocation Location
		{
			get { return location; }
		}

		public override void Do()
		{
			if (index > -1 && index < Owner.Level.MonsterLocations.Count)
				Owner.Level.MonsterLocations.Insert(index, location);
			else
				Owner.Level.MonsterLocations.Add(location);
		}

		public override void Undo()
		{
			Owner.Level.MonsterLocations.Remove(location);
		}
	}
}
