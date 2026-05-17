using System;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("МКР: СТВОРЕННЯ HTML СТРУКТУРИ ДЛЯ АНАЛІЗУ");
            
            LightElementNode list = new LightElementNode("ul", "block", "paired");
            LightElementNode item1 = new LightElementNode("li", "block", "paired");
            item1.Children.Add(new LightTextNode("Головна"));

            LightElementNode item2 = new LightElementNode("li", "block", "paired");
            item2.Children.Add(new LightTextNode("Про нас"));

            LightElementNode image = new LightElementNode("img", "inline", "single");

            list.Children.Add(item1);
            list.Children.Add(item2);
            list.Children.Add(image);

            Console.WriteLine("\nМКР: ЗАПУСК ПАТЕРНУ ВІДВІДУВАЧ");
            
            HtmlStatsVisitor statsVisitor = new HtmlStatsVisitor();

            list.Accept(statsVisitor);

            statsVisitor.PrintReport();

            Console.ReadKey();
        }
    }
}