using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Services.Abstraction.UI
{
    public interface IUIService
    {
        void WriteOutput(string output, bool addEmptyLine = true);
        void WriteOutput(IEnumerable<string> output, bool addEmptyLine = true);
        void RenderScreen(StringBuilder screen);
    }
}
