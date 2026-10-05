using System;

namespace CompilerPascal
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string inputFile = "example.pas";

            InputOutput.Initialize(inputFile);
            if (InputOutput.EndOfFile)
            {
                Console.WriteLine("Файл не найден или пуст.");
                return;
            }

            LexicalAnalyzer lex = new LexicalAnalyzer();
            Parser parser = new Parser(lex);

            parser.Parse();

            LexicalAnalyzer.CheckParenBalance();
            InputOutput.FlushErrors();
            InputOutput.Finish();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}