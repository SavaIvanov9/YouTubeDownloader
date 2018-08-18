using System;
using YD.Services.Abstraction.UI;

namespace YD.Services.ConsoleUI
{
    [Serializable]
    public class ConsoleProgressBarService : ICustomProgressBarService
    {
        private const int MaxBars = 25;

        private double _progress;
        private int _barsDrawn;
        private int _barsOffset;

        public ConsoleProgressBarService()
        {
            Initialize();
        }

        public ICustomProgressBarService CreateProgressBar()
        {
            return new ConsoleProgressBarService();
        }

        public void Report(double value)
        {
            _progress = value;
            DrawProgress();
        }

        public void Dispose()
        {
            Console.WriteLine();
        }

        private void Initialize()
        {
            // Create empty progress bar
            Console.Write('[');
            _barsOffset = Console.CursorLeft;
            Console.Write(new string(' ', MaxBars));
            Console.Write(']');
        }

        private void DrawProgress()
        {
            // Draw bars
            var bars = (int)Math.Floor(_progress * MaxBars);
            for (var i = _barsDrawn; i < bars; i++)
            {
                Console.SetCursorPosition(_barsOffset + i, Console.CursorTop);
                Console.Write('-');
            }

            _barsDrawn = bars;

            // Draw text
            Console.SetCursorPosition(_barsOffset + MaxBars + 3, Console.CursorTop);
            Console.Write($"{_progress:P0}");
        }
    }
}