using YD.Common.Contracts;

namespace YD.Services.Abstraction.CommandProcessing
{
    public interface ICommandFactoryService
    {
        ICommandRegister Register { get; set; }

        IEngineCommand CreateCommand(string selector);
    }
}
