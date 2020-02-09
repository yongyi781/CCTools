namespace CCTools.CCDesign
{
	public abstract class Command
	{
		public abstract string Name { get; }
		public LevelMapEditor Owner { get; set; }

		public abstract void Do();
		public abstract void Undo();
	}
}
