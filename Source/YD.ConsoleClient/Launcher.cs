using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            while (true)
            {
                engine.ProcessInput(Console.ReadLine());
            }
        }
    }
}
