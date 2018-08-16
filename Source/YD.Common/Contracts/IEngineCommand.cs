using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Common.Contracts
{
    public interface IEngineCommand : IDisposable
    {
        string Name { get; }

        IEnumerable<string> Execute(string commandParams);
    }
}