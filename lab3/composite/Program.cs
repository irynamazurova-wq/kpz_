using System;
using System.Text;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("МКР: Створення елемента");
            LightElementNode button = new LightElementNode("button", "inline", "paired");

            CommandInvoker invoker = new CommandInvoker();

            Console.WriteLine("\nМКР: застосування команд");
            ICommand cmd1 = new AddClassCommand(button, "btn");
            ICommand cmd2 = new AddClassCommand(button, "btn-success");

            invoker.ExecuteCommand(cmd1);
            invoker.ExecuteCommand(cmd2);

            Console.WriteLine("\n[Поточний стан HTML]:");
            Console.WriteLine(button.OuterHTML);

            Console.WriteLine("\nМКР: Скасування останньої дії");
            invoker.UndoLastCommand();

            Console.WriteLine("\n[Стан HTML після скасування]:");
            Console.WriteLine(button.OuterHTML);

            Console.ReadKey();
        }
    }
}