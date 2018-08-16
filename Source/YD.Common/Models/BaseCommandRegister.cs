using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Contracts;

namespace YD.Common.Models
{
    public abstract class BaseCommandRegister : ICommandRegister
    {
        protected BaseCommandRegister()
        {
            Commands = new Dictionary<string, EngineCommandInfo<IEngineCommand>>();
            LoadCommands();
        }

        public Dictionary<string, EngineCommandInfo<IEngineCommand>> Commands { get; }

        public virtual void LoadRegister(ICommandRegister register)
        {
            foreach (var item in register.Commands)
            {
                Commands.Add(item.Key, item.Value);
            }
        }

        protected abstract void LoadCommands();
    }
}
