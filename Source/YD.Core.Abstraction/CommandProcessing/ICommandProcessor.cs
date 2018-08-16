using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Core.Abstraction.CommandProcessing
{
    public interface ICommandProcessor
    {
        bool IsProcessing { get; }

        void LoadCommands(ICommandRegister commandRegister);

        IEnumerable<string> ProcessCommand(string commandSelector, string commandParams, bool displayCommandName);

        Task<IEnumerable<string>> ProcessCommandAsync(string commandSelector, string commandParams, bool displayCommandName);
    }
}
