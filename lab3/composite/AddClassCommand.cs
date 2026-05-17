namespace Composite
{
    public class AddClassCommand : ICommand
    {
        private readonly LightElementNode _element;
        private readonly string _className;

        public AddClassCommand(LightElementNode element, string className)
        {
            _element = element;
            _className = className;
        }

        public void Execute()
        {
            _element.ApplyClass(_className); // викликає метод із хуком з минулої фічі
        }

        public void Undo()
        {
            if (_element.CssClasses.Contains(_className))
            {
                _element.CssClasses.Remove(_className);
                System.Console.WriteLine($"[Команда Undo]: Клас \"{_className}\" успішно видалено з тегу <{_element.TagName}>.");
            }
        }
    }
}