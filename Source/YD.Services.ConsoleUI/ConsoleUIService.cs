using System;
using System.Collections.Generic;
using System.Text;
using YD.Services.Abstraction.UI;

namespace YD.Services.ConsoleUI
{
    [Serializable]
    public class ConsoleUIService : IUIService
    {
        private static readonly object ConsoleLock = new object();

        public void RenderScreen(StringBuilder screen)
        {
            Console.Clear();
            Console.WriteLine(screen.ToString());
        }

        public void WriteOutput(string output, bool addEmptyLine = true)
        {
            lock (ConsoleLock)
            {
                Console.WriteLine(output);

                if (addEmptyLine)
                    Console.WriteLine(string.Empty);
            }
        }

        public void WriteOutput(IEnumerable<string> output, bool addEmptyLine = true)
        {
            lock (ConsoleLock)
            {
                foreach (var line in output)
                {
                    Console.WriteLine(line);
                }

                if (addEmptyLine)
                    Console.WriteLine(string.Empty);
            }
        }

        public void WriteHeader(string input, bool addNewLine = false)
        {
            lock (ConsoleLock)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (addNewLine)
                {
                    Console.WriteLine($"{input}{Environment.NewLine}");
                }
                else
                {
                    Console.WriteLine(input);
                }

                Console.ForegroundColor = color;
            }
        }
    }
}