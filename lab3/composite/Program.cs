using System;
using System.Collections.Generic;
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

            // === Код МКР
            Console.WriteLine("\n=== [МКР] Тест шаблону Ітератор ===");

            Console.WriteLine("\n[DFS] Обхід в глибину:");
            ILightIterator dfs = new DepthFirstIterator(list);
            while (dfs.HasNext())
            {
                LightNode node = dfs.Next();
                if (node is LightElementNode el)
                    Console.WriteLine($"Тег: <{el.TagName}>");
                else if (node is LightTextNode txt)
                    Console.WriteLine($"   Текст: \"{txt.InnerHTML}\"");
            }

            Console.WriteLine("\n[BFS] Обхід в ширину:");
            ILightIterator bfs = new BreadthFirstIterator(list);
            while (bfs.HasNext())
            {
                LightNode node = bfs.Next();
                if (node is LightElementNode el)
                    Console.WriteLine($"Тег: <{el.TagName}>");
                else if (node is LightTextNode txt)
                    Console.WriteLine($"   Текст: \"{txt.InnerHTML}\"");
            }

            Console.ReadKey();
        }
    }
}