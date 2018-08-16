using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Services.Abstraction;

namespace YD.Services
{
    public class ConsoleLoggingService : ILoggingService
    {
        private static readonly object ConsoleLock = new object();

        public void Write(string input, bool addNewLine = false)
        {
            lock (ConsoleLock)
            {
                if (addNewLine)
                {
                    Console.Write($"{input}{Environment.NewLine}");
                }
                else
                {
                    Console.Write(input);
                }
            }
        }

        public void WriteLine(string input, bool addNewLine = false)
        {
            lock (ConsoleLock)
            {
                if (addNewLine)
                {
                    Console.WriteLine($"{input}{Environment.NewLine}");
                }
                else
                {
                    Console.WriteLine(input);
                }
            }
        }

        public void LogHeader(string input, bool addNewLine = false)
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