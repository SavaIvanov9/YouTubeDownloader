﻿using System;

namespace YD.ConsoleClient
{
    internal class ConsoleConfiguration
    {
        private bool isRunning;

        public ConsoleConfiguration()
        {
            isRunning = true;
        }

        public bool IsRunning => isRunning;

        public void OnStartup()
        {
            Console.Title = "Youtube downloader";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            Console.WriteLine("Youtube downloader started.", false);
            Console.WriteLine(@"Enter ""help"" or ""h"" to see available commands.", false);
            Console.WriteLine(@"Enter ""exit"" to close the program.", false);
            Console.WriteLine("Enter command:", false);
        }

        public void OnExit()
        {
            Console.WriteLine("Youtube downloader terminated.", false);
            Console.WriteLine("Press any key to exit...", false);
            Console.ReadKey();
            isRunning = false;
        }
    }
}
