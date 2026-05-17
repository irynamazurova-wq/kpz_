using System;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("[МКР] Створення дерева та хуки життєвого циклу");

            LightElementNode list = new LightElementNode("ul", "block", "paired");
            list.ApplyClass("menu-list");

            LightElementNode item1 = new LightElementNode("li", "block", "paired");
            item1.ApplyClass("menu-item");
            item1.Children.Add(new LightTextNode("Головна"));

            LightElementNode item2 = new LightElementNode("li", "block", "paired");
            item2.ApplyClass("menu-item");
            item2.Children.Add(new LightTextNode("Про нас"));

            LightElementNode image = new LightElementNode("img", "inline", "single");
            image.ApplyClass("logo");

            list.Children.Add(item1);
            list.Children.Add(item2);
            list.Children.Add(image);

            Console.WriteLine("\n[МКР] Рендеринг HTML сторінки");
            Console.WriteLine(list.OuterHTML);

            Console.ReadKey();
        }
    }
}