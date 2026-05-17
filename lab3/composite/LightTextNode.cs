using System;

namespace Composite
{
    public class LightTextNode : LightNode
    {
        private readonly string _text;

        public LightTextNode(string text)
        {
            _text = text;
            Initialize(); 
        }

        public override string InnerHTML => _text;

        public override void Accept(ILightVisitor visitor)
        {
             visitor.Visit(this);
        }

        public override string OuterHTML
        {
            get
            {
                OnTextRendered(); 
                return _text;
            }
        }

        protected override void OnCreated()
        {
            Console.WriteLine($"[Хук OnCreated]: Створено текстовий вузол з текстом: \"{_text}\"");
        }

        protected override void OnTextRendered()
        {
            Console.WriteLine($"[Хук OnTextRendered]: Текст \"{_text}\" зараз буде виведено на екран.");
        }
    }
}