using System.Collections.Generic;
using System.Threading.Tasks;
using YD.Common.Contracts;

namespace YD.Services.Abstraction.CommandProcessing
{
    public interface ICommandProcessingService
    {
        bool IsProcessing { get; }

        void LoadCommands(ICommandRegister commandRegister);

        IEnumerable<string> ProcessCommand(string commandSelector, string commandParams, bool displayCommandName);

        Task<IEnumerable<string>> ProcessCommandAsync(string commandSelector, string commandParams, bool displayCommandName);
    }
}
