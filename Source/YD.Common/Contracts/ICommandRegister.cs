using System.Collections.Generic;
using YD.Common.Models;

namespace YD.Common.Contracts
{
    public interface ICommandRegister
    {
        Dictionary<string, EngineCommandInfo<IEngineCommand>> Commands { get; }

        void LoadRegister(ICommandRegister register);
    }
}
