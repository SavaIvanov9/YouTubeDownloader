using YD.Common.Contracts;
using YD.Common.Enums;
using YD.Common.Models;

namespace YD.ConsoleClient
{
    internal class ConsoleCommandRegister : BaseCommandRegister
    {
        protected override void LoadCommands()
        {
            Commands.Add("dv", new EngineCommandInfo<IEngineCommand>
            {
                Name = "Youtube files download",
                CommandFormat = "[selector] [parameter]",
                CommanType = EngineCommandType.DownloadVideos,
                CommandDescription =
                    @"Download videos from youtube. Store the links in ""youtube-links-to-download.txt"" each on new row. Files will be stored in ""downloaded-files"" folder. Add ""-mp3"" as optional parameter to convert output in mp3 format. Add ""-init"" as optional parameter to initialize input file.",
            });

            Commands.Add("dp", new EngineCommandInfo<IEngineCommand>
            {
                Name = "Youtube download playlists",
                CommandFormat = "[selector] [parameter]",
                CommanType = EngineCommandType.DownloadPlaylists,
                CommandDescription =
                    @"Download videos from playlist in youtube. Store the links of playlists in ""youtube-playlists-to-download.txt"" each on new row. Files will be saved in ""downloaded-playlists"" folder. Add ""-mp3"" as optional parameter to convert output in mp3 format. Add ""-init"" as optional parameter to initialize input file.",
            });
        }
    }
}