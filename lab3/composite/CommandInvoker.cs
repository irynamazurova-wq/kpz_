using System.Collections.Generic;

namespace Composite
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _history = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_history.Count > 0)
            {
                ICommand lastCommand = _history.Pop();
                lastCommand.Undo();
            }
            else
            {
                System.Console.WriteLine("[Система]: Історія команд порожня, нічого скасовувати!");
            }
        }
    }
}