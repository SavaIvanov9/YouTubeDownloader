using YD.Common.Models;

namespace YD.Services.Abstraction
{
    public interface IErrorLoggingService
    {
        bool IsEnabled { get; set; }

        string LogError(Error error);
    }
}
