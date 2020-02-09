namespace CCTools.CCDesign
{
	public class MoveTrapConnectionCommand : Command
	{
		public MoveTrapConnectionCommand(int oldIndex, int newIndex)
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
			if (oldIndex > -1 && oldIndex < Owner.Level.TrapConnections.Count && newIndex > -1 && newIndex < Owner.Level.TrapConnections.Count)
			{
				Owner.Level.TrapConnections.Move(oldIndex, newIndex);
				foreach (var trapConnection in Owner.Level.TrapConnections)
					Owner.Invalidate(trapConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			if (oldIndex > -1 && oldIndex < Owner.Level.TrapConnections.Count && newIndex > -1 && newIndex < Owner.Level.TrapConnections.Count)
			{
				Owner.Level.TrapConnections.Move(newIndex, oldIndex);
				foreach (var trapConnection in Owner.Level.TrapConnections)
					Owner.Invalidate(trapConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
