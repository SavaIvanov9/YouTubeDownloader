using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Common.Exceptions
{
    public class CommandNotImplementedException : CustomException
    {
        public CommandNotImplementedException(string commandName) : base($@"Command ""{commandName}"" is not implemented!")
        {
        }
    }
}
