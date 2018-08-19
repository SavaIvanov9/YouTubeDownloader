using System;
using YD.Common.Contracts;
using YD.Common.Enums;
using YD.Common.Exceptions;
using YD.Services.Abstraction.CommandProcessing;
using YD.Services.Abstraction.Youtube;

namespace YD.Services.Core
{
    public class CommandFactoryService : ICommandFactoryService
    {
        private readonly IYouTubeDownloadVideosService youTubeDownloadVideosService;
        private readonly IYouTubeDownloadPlayListsService youTubeDownloadPlayListsService;
        private ICommandRegister register;

        public CommandFactoryService(
            IYouTubeDownloadVideosService youTubeDownloadVideosService,
            IYouTubeDownloadPlayListsService youTubeDownloadPlayListsService)
        {
            this.youTubeDownloadVideosService = youTubeDownloadVideosService;
            this.youTubeDownloadPlayListsService = youTubeDownloadPlayListsService;
        }

        public ICommandRegister Register
        {
            get
            {
                return this.register;
            }

            set
            {
                this.register = value;
            }
        }

        public IEngineCommand CreateCommand(string selector)
        {
            if (this.register == null)
            {
                throw new ArgumentNullException($"{nameof(ICommandRegister)} cannot be null");
            }

            if (!this.register.Commands.ContainsKey(selector))
            {
                throw new CommandNotImplementedException(selector);
            }

            var commandInfo = this.register.Commands[selector];

            switch (commandInfo.CommanType)
            {
                case EngineCommandType.DownloadVideos:
                    return (IEngineCommand)youTubeDownloadVideosService.Clone();
                case EngineCommandType.DownloadPlaylists:
                    return (IEngineCommand)youTubeDownloadPlayListsService.Clone();
                default:
                    throw new CommandNotImplementedException(selector);
            }
        }
    }
}
