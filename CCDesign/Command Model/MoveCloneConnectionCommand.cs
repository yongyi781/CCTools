namespace CCTools.CCDesign
{
	public class MoveCloneConnectionCommand : Command
	{
		public MoveCloneConnectionCommand(int oldIndex, int newIndex)
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
			if (oldIndex > -1 && oldIndex < Owner.Level.CloneConnections.Count && newIndex > -1 && newIndex < Owner.Level.CloneConnections.Count)
			{
				Owner.Level.CloneConnections.Move(oldIndex, newIndex);
				foreach (var cloneConnection in Owner.Level.CloneConnections)
					Owner.Invalidate(cloneConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}

		public override void Undo()
		{
			if (oldIndex > -1 && oldIndex < Owner.Level.CloneConnections.Count && newIndex > -1 && newIndex < Owner.Level.CloneConnections.Count)
			{
				Owner.Level.CloneConnections.Move(newIndex, oldIndex);
				foreach (var cloneConnection in Owner.Level.CloneConnections)
					Owner.Invalidate(cloneConnection);
			}
			Owner.UpdateTileCoordinatesAndHighlights();
		}
	}
}
