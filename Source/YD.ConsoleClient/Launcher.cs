using System;
using YD.Core;
using YD.InversionOfControl;

namespace YD.ConsoleClient
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var resolver = new ConsoleDependecyResolver();
            var engine = resolver.Create<Engine>();

            var configuration = new ConsoleConfiguration();
            var commandsRegister = new ConsoleCommandRegister();

            engine.ExecuteOnStartup(configuration.OnStartup)
                .ExecuteOnExit(configuration.OnExit)
                .Start(commandsRegister, true);

            while (configuration.IsRunning)
            {
                engine.ProcessInput(Console.ReadLine());
            }
        }
    }
}
