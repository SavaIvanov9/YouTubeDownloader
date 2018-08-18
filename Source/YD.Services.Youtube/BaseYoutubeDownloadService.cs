using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Extentions;
using YD.Services.Abstraction;
using YD.Services.Abstraction.UI;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YD.Services.Youtube
{
    [Serializable]
    public abstract class BaseYoutubeDownloadService : ICloneable
    {
        protected readonly IUIService uiService;
        private readonly IMp3ConverterService mp3ConverterService;
        private readonly ICustomProgressBarService customProgressBarService;
        protected DateTime CommandStarted;

        protected BaseYoutubeDownloadService(
            IUIService uiService,
            IMp3ConverterService mp3ConverterService,
            ICustomProgressBarService customProgressBarService)
        {
            this.uiService = uiService;
            this.mp3ConverterService = mp3ConverterService;
            this.customProgressBarService = customProgressBarService;
        }

        public virtual string Name => "Youtube command";

        protected abstract string LinksFileName { get; }

        public IEnumerable<string> Execute(string commandParams)
        {
            CommandStarted = DateTime.Now;

            if (commandParams == "init")
            {
                if (!File.Exists($"../{LinksFileName}"))
                {
                    using (File.Create($"../{LinksFileName}"))
                    {
                        return new[] { $"{LinksFileName} file initialized." };
                    }
                }

                return new[] { $"{LinksFileName} file exists." };
            }

            if (File.Exists($"../{LinksFileName}"))
            {
                var links = File.ReadAllLines($"../{LinksFileName}")
                    .Select(x => x.Trim())
                    .ToArray();

                ProcessLinks(links, commandParams);

                return new[] { $"Command completed: {Name}" };
            }
            else
            {
                return new[] { @"File ""youtube-playlists-to-download.txt"" not found!" };
            }
        }

        protected abstract void ProcessLinks(string[] links, string format);

        protected void ProcessVideo(string format, string id, YoutubeClient client, string logPrefix, ref List<Task> tasksToWait, string directoryToSaveVideo)
        {
            try
            {
                var isOutputMp3 = !string.IsNullOrWhiteSpace(format) && format.Trim().ToLower() == "mp3";

                var data = GetVideoData(id, client, isOutputMp3);
                var title = data.Video.Title;
                var titleCleaned = title.CleanFileName();
                var author = data.Video.Author;
                var authorCleaned = data.Video.Author.CleanFileName();
                var ext = data.StreamInfo.Container.GetFileExtension();
                var fileName = $"{authorCleaned} - {titleCleaned}.{ext}";
                var filePath = Path.Combine(directoryToSaveVideo, fileName);

                uiService.WriteOutput($"[{logPrefix}] Downloading: {author} - {title}");
                uiService.WriteOutput($"[{logPrefix}] Progress: ",false);
                DownloadVideo($"{author} - {title}.{ext}", client, data.StreamInfo, directoryToSaveVideo);
                uiService.WriteOutput("");

                if (isOutputMp3 && ext.ToLower() != "mp3")
                {
                    var filePathMp3 = Path.Combine(directoryToSaveVideo, $"{authorCleaned} - {titleCleaned}.mp3");
                    if (filePath != filePathMp3)
                    {
                        tasksToWait.Add(Task.Run(() =>
                        {
                            mp3ConverterService.ConvertToMp3(filePath, filePathMp3);
                            File.Delete(filePath);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                uiService.WriteOutput($"[{logPrefix}] {ex.Message}", true);
            }
        }

        protected void DownloadVideo(string filename, YoutubeClient client, MediaStreamInfo streamInfo, string directoryToSaveVideo)
        {
            Directory.CreateDirectory(directoryToSaveVideo);
            var fileName = filename.CleanFileName();
            var filePath = Path.Combine(directoryToSaveVideo, fileName);

            using (var progressBar = customProgressBarService.CreateProgressBar())
            {
                client.DownloadMediaStreamAsync(streamInfo, filePath, progressBar)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        protected (Video Video, MediaStreamInfo StreamInfo) GetVideoData(string videoId, YoutubeClient client, bool isOutputMp3)
        {
            if (!YoutubeClient.ValidateVideoId(videoId))
            {
                throw new ArgumentException("Invalid video id");
            }

            var streamInfoSet = client.GetVideoMediaStreamInfosAsync(videoId).GetAwaiter().GetResult();

            MediaStreamInfo streamInfo;

            if (isOutputMp3)
            {
                streamInfo = streamInfoSet.Audio.WithHighestBitrate();
            }
            else
            {
                streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            }

            var video = client.GetVideoAsync(videoId)
                .GetAwaiter()
                .GetResult();

            return (video, streamInfo);
        }

        public object Clone()
        {
            return this.DeepClone();
        }
    }
}