using System.Collections.Generic;

namespace CCTools.CCDesign
{
	public class UndoRedoManager
	{
		private readonly Stack<Command> undoStack = new Stack<Command>();
		private readonly Stack<Command> redoStack = new Stack<Command>();
		private CompoundCommand tempCompoundCommand;

		public UndoRedoManager(LevelMapEditor owner)
		{
			this.Owner = owner;
		}

		public bool CanUndo
		{
			get { return undoStack.Count > 0; }
		}

		public bool CanRedo
		{
			get { return redoStack.Count > 0; }
		}

		public bool IsInCompoundMode
		{
			get { return tempCompoundCommand != null; }
		}

		public LevelMapEditor Owner { get; }

		public Command NextUndoCommand
		{
			get
			{
				if (undoStack.Count == 0)
					return null;
				return undoStack.Peek();
			}
		}

		public Command NextRedoCommand
		{
			get
			{
				if (redoStack.Count == 0)
					return null;
				return redoStack.Peek();
			}
		}

		public string UndoName
		{
			get
			{
				if (undoStack.Count > 0)
					return undoStack.Peek().Name;
				else
					return string.Empty;
			}
		}

		public string RedoName
		{
			get
			{
				if (redoStack.Count > 0)
					return redoStack.Peek().Name;
				else
					return string.Empty;
			}
		}

		public void Add(Command command)
		{
			if (command == null)
				return;
			if (tempCompoundCommand != null)
				tempCompoundCommand.Add(command);
			else
			{
				redoStack.Clear();
				undoStack.Push(command);
				command.Owner = Owner;
			}
		}

		public void BeginCompoundCommand(string name)
		{
			if (IsInCompoundMode)
				tempCompoundCommand.BeginCompoundCommand(name);
			else
				tempCompoundCommand = new CompoundCommand(name) { Owner = Owner };
		}

		public void EndCompoundCommand()
		{
			if (IsInCompoundMode && tempCompoundCommand.IsInCompoundMode)
				tempCompoundCommand.EndCompoundCommand();
			else
			{
				redoStack.Clear();
				if (tempCompoundCommand.Count > 0)
					undoStack.Push(tempCompoundCommand);
				tempCompoundCommand = null;
			}
		}

		public void Do(Command command)
		{
			if (command == null)
				return;
			Add(command);
			command.Do();
		}

		public void Undo()
		{
			var command = undoStack.Pop();
			command.Undo();
			redoStack.Push(command);
		}

		public void Redo()
		{
			var command = redoStack.Pop();
			command.Do();
			undoStack.Push(command);
		}
	}
}
