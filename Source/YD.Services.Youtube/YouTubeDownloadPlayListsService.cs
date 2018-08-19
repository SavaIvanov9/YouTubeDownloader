using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Services.Abstraction;
using YD.Services.Abstraction.UI;
using YD.Services.Abstraction.Youtube;
using YoutubeExplode;

namespace YD.Services.Youtube
{
    [Serializable]
    public class YouTubeDownloadPlayListsService : BaseYoutubeDownloadService, IEngineCommand, IYouTubeDownloadPlayListsService
    {
        private string _downloadFolderName => "downloaded-playlists";
        private string _downloadSubFolderName => $"{CommandStarted:dd-MM-yyyy hh;mm;ss tt}";

        public YouTubeDownloadPlayListsService(
            IUIService uiService,
            IMp3ConverterService mp3ConverterService,
            ICustomProgressBarService customProgressBarService) : base(
                uiService,
                mp3ConverterService,
                customProgressBarService)
        {
        }

        public override string Name => "Youtube download playlists";
        protected override string LinksFileName => "youtube-playlists-to-download.txt";

        public void Dispose()
        {
        }

        protected override void ProcessLinks(string[] links, string format)
        {
            var tasksToWait = new List<Task>();
            long playListCounter = 0;

            foreach (var link in links)
            {
                playListCounter++;
                var client = new YoutubeClient();
                uiService.WriteHeader($@"[{playListCounter}] Processing link ""{link}""");

                try
                {
                    var id = YoutubeClient.ParsePlaylistId(link);
                    var playlist = client.GetPlaylistAsync(id).GetAwaiter().GetResult();

                    long videoCounter = 0;

                    foreach (var video in playlist.Videos)
                    {
                        videoCounter++;
                        var directoryToSaveVideo = $"../{_downloadFolderName}/{_downloadSubFolderName} - {playlist.Title}";
                        var logPrefix = $"{playListCounter}.{videoCounter}";

                        ProcessVideo(format, video.Id, client, logPrefix, ref tasksToWait, directoryToSaveVideo);
                    }
                }
                catch (Exception ex)
                {
                    uiService.WriteOutput($"[{playListCounter}] {ex.Message}", true);
                }
            }

            if (tasksToWait.Any())
            {
                uiService.WriteOutput($"Converting files, please wait...");
                Task.WaitAll(tasksToWait.ToArray());
                uiService.WriteOutput($"Converting files done.");
            }
        }
    }
}
