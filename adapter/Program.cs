#nullable disable
using System;
using System.IO;

namespace Adapter
{
    public interface ILogger
    {
        void Log(string message);
        void Error(string message);
        void Warn(string message);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LOG]: {message}");
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARN]: {message}");
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        private string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string text)
        {
            File.AppendAllText(_filePath, text);
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(_filePath, text + Environment.NewLine);
        }
    }

    public class FileLoggerAdapter : ILogger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public void Log(string message)
        {
            _fileWriter.WriteLine($"[LOG]: {message}");
        }

        public void Error(string message)
        {
            _fileWriter.WriteLine($"[ERROR]: {message}");
        }

        public void Warn(string message)
        {
            _fileWriter.WriteLine($"[WARN]: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ILogger consoleLogger = new Logger();
            consoleLogger.Log("Програма працює стабільно.");
            consoleLogger.Warn("Виявлено незначне попередження.");
            consoleLogger.Error("Сталася критична помилка системи.");

            string path = "log.txt";
            if (File.Exists(path)) File.Delete(path);

            FileWriter writer = new FileWriter(path);
            ILogger fileLogger = new FileLoggerAdapter(writer);

            fileLogger.Log("Користувач успішно авторизувався.");
            fileLogger.Warn("Спроба завантаження великого файлу.");
            fileLogger.Error("Не вдалося зберегти зміни в базі.");

            Console.WriteLine("\nВміст створеного файлу log.txt:");
            Console.WriteLine(File.ReadAllText(path));

            Console.ReadKey();
        }
    }
}