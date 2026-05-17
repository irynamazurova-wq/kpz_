#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Flyweight
{
    public abstract class LightNode
    {
        public abstract string OuterHTML { get; }
    }

    public class LightTextNode : LightNode
    {
        public string Text { get; }
        public LightTextNode(string text) => Text = text;
        public override string OuterHTML => Text;
    }

    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public string ClosureType { get; }
        public List<LightNode> Children { get; } = new List<LightNode>();

        public LightElementNode(string tagName, string displayType, string closureType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosureType = closureType;
        }

        public override string OuterHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var child in Children) sb.Append(child.OuterHTML);
                return $"<{TagName}>{sb}</{TagName}>";
            }
        }
    }

    public class FlyweightTag
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public string ClosureType { get; }

        public FlyweightTag(string tagName, string displayType, string closureType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosureType = closureType;
        }
    }

    public class FlyweightFactory
    {
        private readonly Dictionary<string, FlyweightTag> _tags = new();

        public FlyweightTag GetTag(string tagName, string displayType, string closureType)
        {
            if (!_tags.ContainsKey(tagName))
            {
                _tags[tagName] = new FlyweightTag(tagName, displayType, closureType);
            }
            return _tags[tagName];
        }
    }

    public class FlyweightElementNode : LightNode
    {
        private readonly FlyweightTag _tag;
        public List<LightNode> Children { get; } = new List<LightNode>();

        public FlyweightElementNode(FlyweightTag tag)
        {
            _tag = tag;
        }

        public override string OuterHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var child in Children) sb.Append(child.OuterHTML);
                return $"<{_tag.TagName}>{sb}</{_tag.TagName}>";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePath = "book.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Помилка: Створіть файл book.txt у папці проєкту!");
                return;
            }

            string[] bookLines = File.ReadAllLines(filePath);

            Console.WriteLine($"--- Успішно завантажено весь текст книги: {bookLines.Length} рядків ---");
            Console.WriteLine("\n--- Демонстрація верстки перших 5 рядків за правилами ---");
            for (int i = 0; i < Math.Min(5, bookLines.Length); i++)
            {
                string line = bookLines[i];
                string tag = "p";
                if (i == 0) tag = "h1";
                else if (line.StartsWith(" ") || line.StartsWith("\t")) tag = "blockquote";
                else if (line.Length < 20 && !string.IsNullOrWhiteSpace(line)) tag = "h2";

                Console.WriteLine($"<{tag}> {line.Trim()} </{tag}>");
            }

            int multipliers = 10; 

            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memBeforeHeavy = GC.GetTotalMemory(true);

            List<LightNode> heavyTree = new List<LightNode>();
            for (int i = 0; i < multipliers; i++)
            {
                for (int j = 0; j < bookLines.Length; j++)
                {
                    string line = bookLines[j];
                    LightElementNode node;
                    
                    if (j == 0 && i == 0) node = new LightElementNode("h1", "block", "paired");
                    else if (line.StartsWith(" ") || line.StartsWith("\t")) node = new LightElementNode("blockquote", "block", "paired");
                    else if (line.Length < 20 && !string.IsNullOrWhiteSpace(line)) node = new LightElementNode("h2", "block", "paired");
                    else node = new LightElementNode("p", "block", "paired");

                    node.Children.Add(new LightTextNode(line));
                    heavyTree.Add(node);
                }
            }
            long memAfterHeavy = GC.GetTotalMemory(true) - memBeforeHeavy;

            heavyTree = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            long memBeforeFlyweight = GC.GetTotalMemory(true);
            FlyweightFactory factory = new FlyweightFactory();
            List<LightNode> flyweightTree = new List<LightNode>();

            for (int i = 0; i < multipliers; i++)
            {
                for (int j = 0; j < bookLines.Length; j++)
                {
                    string line = bookLines[j];
                    FlyweightTag tag;
                    
                    if (j == 0 && i == 0) tag = factory.GetTag("h1", "block", "paired");
                    else if (line.StartsWith(" ") || line.StartsWith("\t")) tag = factory.GetTag("blockquote", "block", "paired");
                    else if (line.Length < 20 && !string.IsNullOrWhiteSpace(line)) tag = factory.GetTag("h2", "block", "paired");
                    else tag = factory.GetTag("p", "block", "paired");

                    FlyweightElementNode node = new FlyweightElementNode(tag);
                    node.Children.Add(new LightTextNode(line));
                    flyweightTree.Add(node);
                }
            }
            long memAfterFlyweight = GC.GetTotalMemory(true) - memBeforeFlyweight;

            Console.WriteLine("\n--- Фінальні результати аналізу пам'яті процесу ---");
            Console.WriteLine($"Обсяг звичайної верстки всього тексту: {memAfterHeavy / 1024.0:F2} KB");
            Console.WriteLine($"Обсяг верстки з Легковаговиком: {memAfterFlyweight / 1024.0:F2} KB");
            Console.WriteLine($"Економія оперативної пам'яті: {((memAfterHeavy - memAfterFlyweight) / (double)memAfterHeavy) * 100:F1}%");

            Console.ReadKey();
        }
    }
}