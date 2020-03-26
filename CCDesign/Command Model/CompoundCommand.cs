using System.Collections.Generic;

namespace CCTools.CCDesign
{
    public class CompoundCommand : Command
    {
        private readonly Stack<Command> commands = new Stack<Command>();
        private CompoundCommand tempCompoundCommand;

        public CompoundCommand(string name)
        {
            this.name = name;
        }

        public int Count
        {
            get { return commands.Count; }
        }

        public bool IsInCompoundMode
        {
            get { return tempCompoundCommand != null; }
        }

        private readonly string name;
        public override string Name
        {
            get { return name; }
        }

        public void Add(Command command)
        {
            if (tempCompoundCommand != null)
                tempCompoundCommand.Add(command);
            else
            {
                commands.Push(command);
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

        public void Clear()
        {
            commands.Clear();
        }

        public override void Do()
        {
            foreach (var command in commands)
                command.Do();
        }

        public void EndCompoundCommand()
        {
            if (IsInCompoundMode && tempCompoundCommand.IsInCompoundMode)
                tempCompoundCommand.EndCompoundCommand();
            else
            {
                if (tempCompoundCommand.Count > 0)
                    commands.Push(tempCompoundCommand);
                tempCompoundCommand = null;
            }
        }

        public override void Undo()
        {
            foreach (var command in commands)
                command.Undo();
        }
    }
}
