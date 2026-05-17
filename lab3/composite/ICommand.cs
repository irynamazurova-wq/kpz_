namespace Composite
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}