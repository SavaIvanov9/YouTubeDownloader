# YouTube Downloader

## Description:
Tool for downloading videos and playlists from YouTube and converting them in mp3 format if needed.
I needed tool like this and I have decided to implement it for fun :) Currently it has console client interface. In future I will develop some
shiny desktop, web and mobile versions.

## Available commands:

### -Download videos
Download multiple videos from youtube. Store the links in "youtube-links-to-download.txt" each on new row. Files will be stored in 
"downloaded-files" folder. Add "-mp3" as optional parameter to convert output in mp3 format. Add "-init" as optional parameter to initialize input file.

### -Download playlists
Download videos from multiple playlists in youtube. Store the links of playlists in "youtube-playlists-to-download.txt" each on new row. Files
will be saved in "downloaded-playlists" folder. Add "-mp3" as optional parameter to convert output in mp3 format. Add "-init" as optional parameter to initialize input file.
