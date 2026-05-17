#nullable disable
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Proxy
{
    public interface ISmartTextReader
    {
        char[][] ReadTextFile(string filePath);
    }

    public class SmartTextReader : ISmartTextReader
    {
        public char[][] ReadTextFile(string filePath)
        {
            if (!File.Exists(filePath)) return new char[0][];
            string[] lines = File.ReadAllLines(filePath);
            char[][] result = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }
            return result;
        }
    }

    public class SmartTextChecker : ISmartTextReader
    {
        private readonly ISmartTextReader _reader;

        public SmartTextChecker(ISmartTextReader reader)
        {
            _reader = reader;
        }

        public char[][] ReadTextFile(string filePath)
        {
            Console.WriteLine($"[Лог] Успішне відкриття файлу: {filePath}");
            char[][] result = _reader.ReadTextFile(filePath);
            Console.WriteLine($"[Лог] Файл {filePath} успішно прочитано та закрито.");
            
            if (result != null)
            {
                int totalChars = 0;
                foreach (var row in result) totalChars += row.Length;
                Console.WriteLine($"[Статистика] Загальна кількість рядків: {result.Length}, символів: {totalChars}");
            }
            return result;
        }
    }

    public class SmartTextReaderLocker : ISmartTextReader
    {
        private readonly ISmartTextReader _reader;
        private readonly Regex _regex;

        public SmartTextReaderLocker(ISmartTextReader reader, string pattern)
        {
            _reader = reader;
            _regex = new Regex(pattern);
        }

        public char[][] ReadTextFile(string filePath)
        {
            if (_regex.IsMatch(filePath))
            {
                Console.WriteLine("Access denied!");
                return null;
            }
            return _reader.ReadTextFile(filePath);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string regularFile = "public_data.txt";
            string secretFile = "secret_password.txt";

            File.WriteAllText(regularFile, "Hello World\nLine two text.");
            File.WriteAllText(secretFile, "Top secret content");

            ISmartTextReader baseReader = new SmartTextReader();
            ISmartTextReader checkerProxy = new SmartTextChecker(baseReader);
            ISmartTextReader lockerProxy = new SmartTextReaderLocker(checkerProxy, @"secret.*\.txt");

            Console.WriteLine("--- Спроба читання відкритого файлу ---");
            lockerProxy.ReadTextFile(regularFile);

            Console.WriteLine("\n--- Спроба читання захищеного файлу ---");
            lockerProxy.ReadTextFile(secretFile);

            Console.ReadKey();
        }
    }
}