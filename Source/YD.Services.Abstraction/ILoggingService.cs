using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Services.Abstraction
{
    public interface ILoggingService
    {
        void Write(string input, bool addNewLine = false);
        void WriteLine(string input, bool addNewLine = false);
        void LogHeader(string input, bool addNewLine = false);
    }
}
