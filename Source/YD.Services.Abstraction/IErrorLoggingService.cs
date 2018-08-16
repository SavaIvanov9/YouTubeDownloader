using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Models;

namespace YD.Services.Abstraction
{
    public interface IErrorLoggingService
    {
        bool IsEnabled { get; set; }

        string LogError(Error error);
    }
}
