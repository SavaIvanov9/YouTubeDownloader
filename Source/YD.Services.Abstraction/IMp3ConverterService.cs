using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Services.Abstraction
{
    public interface IMp3ConverterService
    {
        void ConvertToMp3(string source, string target);
    }
}
