using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Services.Abstraction.UI
{
    public interface ICustomProgressBarService : IProgress<double>, IDisposable
    {
    }
}
