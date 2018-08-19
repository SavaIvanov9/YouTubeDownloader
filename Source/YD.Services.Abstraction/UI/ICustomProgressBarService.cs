using System;

namespace YD.Services.Abstraction.UI
{
    public interface ICustomProgressBarService : IProgress<double>, IDisposable
    {
        ICustomProgressBarService CreateProgressBar();
    }
}
