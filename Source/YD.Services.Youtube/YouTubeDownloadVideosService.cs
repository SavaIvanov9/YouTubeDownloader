using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Common.Extentions;
using YD.Services.Abstraction;
using YD.Services.Abstraction.UI;
using YD.Services.Abstraction.Youtube;
using YoutubeExplode;

namespace YD.Services.Youtube
{
    [Serializable]
    public class YouTubeDownloadVideosService : BaseYoutubeDownloadService, IEngineCommand, IYouTubeDownloadVideosService
    {
        private string _downloadFolderName => "downloaded-files";
        private string _downloadSubFolderName => $"{CommandStarted:dd-MM-yyyy hh;mm;ss tt}";

        public YouTubeDownloadVideosService(
            IUIService uiService,
            IMp3ConverterService mp3ConverterService,
            ICustomProgressBarService customProgressBarService) : base(
                uiService,
                mp3ConverterService,
                customProgressBarService)
        {
        }

        public override string Name => "Youtube download files";
        protected override string LinksFileName => "youtube-links-to-download.txt";

        //public IYouTubeDownloadVideosService Clone()
        //{
        //    return this.DeepClone();
        //}

        public void Dispose()
        {
        }

        protected override void ProcessLinks(string[] links, string format)
        {
            var tasksToWait = new List<Task>();
            long counter = 0;

            foreach (var link in links)
            {
                counter++;
                var client = new YoutubeClient();
                uiService.WriteHeader($@"[{counter}] Processing link ""{link}""");
                var id = YoutubeClient.ParseVideoId(link);
                var directoryToSaveVideo = $"../{_downloadFolderName}/{_downloadSubFolderName}";
                ProcessVideo(format, id, client, $"{counter}", ref tasksToWait, directoryToSaveVideo);
            }

            uiService.WriteOutput($"Converting files, please wait...");
            Task.WaitAll(tasksToWait.ToArray());
            uiService.WriteOutput($"Converting files done.");
        }
    }
}
