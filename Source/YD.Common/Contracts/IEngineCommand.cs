using System;
using System.Collections.Generic;

namespace YD.Common.Contracts
{
    public interface IEngineCommand : IDisposable
    {
        string Name { get; }

        IEnumerable<string> Execute(string commandParams);
    }
}