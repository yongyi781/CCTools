using System.Collections.Generic;

namespace CCTools.CCDesign
{
	public class AddMonsterRangeCommand : Command
	{
		private IEnumerable<TileLocation> monsterLocations;

		public AddMonsterRangeCommand(IEnumerable<TileLocation> monsterLocations)
		{
			this.monsterLocations = monsterLocations;
		}

		public override string Name
		{
			get { return "Clear Monster Locations"; }
		}

		public override void Do()
		{
			Owner.Level.MonsterLocations.AddRange(monsterLocations);
		}

		public override void Undo()
		{
			foreach (var location in monsterLocations)
				Owner.Level.MonsterLocations.Remove(location);
		}
	}
}
