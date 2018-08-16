using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Contracts;

namespace YD.Services.Abstraction.CommandProcessing
{
    public interface ICommandFactoryService
    {
        ICommandRegister Register { get; set; }

        IEngineCommand CreateCommand(string selector);
    }
}
