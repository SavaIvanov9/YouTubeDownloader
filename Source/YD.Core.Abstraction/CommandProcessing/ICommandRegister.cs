using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Core.Abstraction.CommandProcessing
{
    public interface ICommandRegister
    {
        Dictionary<string, CommandInfo<IAssistantCommand>> Commands { get; }

        void LoadRegister(ICommandRegister register);
    }
}
