using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Common.Models;

namespace YD.Common.Contracts
{
    public interface ICommandRegister
    {
        Dictionary<string, EngineCommandInfo<IEngineCommand>> Commands { get; }

        void LoadRegister(ICommandRegister register);
    }
}
