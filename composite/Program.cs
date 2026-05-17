using System;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            LightElementNode list = new LightElementNode("ul", "block", "paired");
            list.CssClasses.Add("menu-list");

            LightElementNode item1 = new LightElementNode("li", "block", "paired");
            item1.CssClasses.Add("menu-item");
            item1.Children.Add(new LightTextNode("Головна"));

            LightElementNode item2 = new LightElementNode("li", "block", "paired");
            item2.CssClasses.Add("menu-item");
            item2.Children.Add(new LightTextNode("Про нас"));

            LightElementNode image = new LightElementNode("img", "inline", "single");
            image.CssClasses.Add("logo");

            list.Children.Add(item1);
            list.Children.Add(item2);
            list.Children.Add(image);

            Console.WriteLine("--- Тестування LightHTML (Компонувальник) ---");
            Console.WriteLine($"Кількість дочірніх елементів: {list.ChildrenCount}");
            Console.WriteLine("\n[Виведення InnerHTML]:");
            Console.WriteLine(list.InnerHTML);
            Console.WriteLine("\n[Виведення OuterHTML]:");
            Console.WriteLine(list.OuterHTML);

            Console.ReadKey();
        }
    }
}