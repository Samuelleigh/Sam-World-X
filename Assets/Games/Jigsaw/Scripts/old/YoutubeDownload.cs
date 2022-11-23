using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
//using YoutubeExtractor;

//public class YoutubeDownload : MonoBehaviour
//{
//
//    public string link;
//    public IEnumerable<VideoInfo> videoInfos;
//
//    private void Awake()
//    {
//        Debug.Log(videoInfos);
//        videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
//        Debug.Log(videoInfos);
//    }
//
//    // Start is called before the first frame update
//    void Start()
//    {
//        DownloadVideo();
//    }
//
//    public void GetURL() 
//    {
//        videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
//
//    }
//
//    public void DownloadVideo() 
//    {
//        VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
//
//        /*
//         * If the video has a decrypted signature, decipher it
//         */
//        if (video.RequiresDecryption)
//        {
//            DownloadUrlResolver.DecryptDownloadUrl(video);
//        }
//
//        /*
//         * Create the video downloader.
//         * The first argument is the video to download.
//         * The second argument is the path to save the video file.
//         */
//      //  var videoDownloader = new VideoDownloader(video, Path.Combine("D:/Downloads", video.Title + video.VideoExtension));
//
//        // Register the ProgressChanged event and print the current progress
//        videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
//
//        /*
//         * Execute the video downloader.
//         * For GUI applications note, that this method runs synchronously.
//         */
//        videoDownloader.Execute();
//
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
//}
//