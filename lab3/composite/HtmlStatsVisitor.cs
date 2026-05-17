using System;

namespace Composite
{
    public class HtmlStatsVisitor : ILightVisitor
    {
        public int ElementCount { get; private set; } = 0;
        public int TextCount { get; private set; } = 0;

        public void Visit(LightElementNode element)
        {
            ElementCount++; 
        }

        public void Visit(LightTextNode textNode)
        {
            TextCount++; 
        }

        public void PrintReport()
        {
            Console.WriteLine($"[Звіт Відвідувача]: Всього знайдено HTML-тегів: {ElementCount}");
            Console.WriteLine($"[Звіт Відвідувача]: Всього знайдено текстових блоків: {TextCount}");
        }
    }
}