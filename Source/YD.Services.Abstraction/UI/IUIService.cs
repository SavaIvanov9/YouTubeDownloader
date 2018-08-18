using System.Collections.Generic;
using System.Text;

namespace YD.Services.Abstraction.UI
{
    public interface IUIService
    {
        void WriteOutput(string output, bool addEmptyLine = true);
        void WriteOutput(IEnumerable<string> output, bool addEmptyLine = true);
        void WriteHeader(string input, bool addNewLine = false);
        void RenderScreen(StringBuilder screen);
    }
}
