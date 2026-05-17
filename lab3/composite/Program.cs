using System;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("МКР: СТВОРЕННЯ ЕЛЕМЕНТА");
            LightElementNode div = new LightElementNode("div", "block", "paired");
            div.ApplyClass("container");
            div.Children.Add(new LightTextNode("Контент сайту"));

            Console.WriteLine("\nМКР: РЕНДЕРИНГ У ВИДИМОМУ СТАНІ");
            Console.WriteLine(div.OuterHTML);

            Console.WriteLine("\nМКР: ПЕРЕМИКАННЯ В ПРИХОВАНИЙ СТАН");
            div.SetState(new HiddenState());

            Console.WriteLine("\n[Рендеринг після зміни стану]:");
            Console.WriteLine(div.OuterHTML);

            Console.WriteLine("\nМКР: ПОВЕРНЕННЯ У ВИДИМИЙ СТАН ===");
            div.SetState(new VisibleState());
            Console.WriteLine(div.OuterHTML);

            Console.ReadKey();
        }
    }
}