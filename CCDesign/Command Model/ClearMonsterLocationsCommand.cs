using System.Collections.Generic;

namespace CCTools.CCDesign
{
	public class ClearMonsterLocationsCommand : Command
	{
		private IEnumerable<TileLocation> _backup;

		public override string Name
		{
			get { return "Clear Monster Locations"; }
		}

		public override void Do()
		{
			_backup = Owner.Level.MonsterLocations.Clone();
			Owner.Level.MonsterLocations.Clear();
		}

		public override void Undo()
		{
			Owner.Level.MonsterLocations.AddRange(_backup);
		}
	}
}
