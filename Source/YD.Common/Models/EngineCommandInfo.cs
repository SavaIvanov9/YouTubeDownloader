using YD.Common.Contracts;
using YD.Common.Enums;

namespace YD.Common.Models
{
    public class EngineCommandInfo<T> where T : IEngineCommand
    {
        public EngineCommandType CommanType { get; set; }

        public string Name { get; set; }

        public string CommandDescription { get; set; }

        public string CommandFormat { get; set; }
    }
}
