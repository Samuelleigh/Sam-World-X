using System.Collections;
using System.Collections.Generic;
using LightShaft.Scripts;
using MovingJigsaw;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeSimplified : MonoBehaviour
{
    private YoutubePlayer player;

    public string url;
    public bool autoPlay = true;
    public bool fullscreen = true;
    private VideoPlayer videoPlayer;

    public VideoPlayer audioPlayer;

    public JigLevelManager manager;
    public bool test = false;

    public int seconds;
    public int minutes;
    public int hours;

    public int startTimeSeconds;

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        player = GetComponentInChildren<YoutubePlayer>();
        player.videoPlayer = videoPlayer;
    }

    private void Start()
    {

        if (manager)
        {

            seconds = manager.startseconds;
            hours = manager.starthours;
            minutes = manager.startminutes;

            startTimeSeconds = seconds + (minutes * 60) + (hours * 3600);

            if (manager.path.StartsWith("https:") && manager.customFile && manager.customMode)
            {
                url = manager.path;
                Debug.Log(startTimeSeconds);
                player.startFromSecond = true;
                player.startFromSecondTime = startTimeSeconds;
                Play();
            }
        }

        if (test) { Play(); }

        Invoke("ForceOnSkipOnDrop", 2f);



        player.dontForceFullscreen = true;
      //  Play();
    }

    public void Play()
    {
        if (fullscreen)
        {
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            videoPlayer.aspectRatio = VideoAspectRatio.FitInside;
            videoPlayer.targetCamera = player.mainCamera;
            
        }
        player.autoPlayOnStart = autoPlay;
        player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;

        if(autoPlay)
            player.Play(url);
    }

    public void ForceOnSkipOnDrop()
    {
        videoPlayer.skipOnDrop = true;

    }

    public void Update()
    {
        videoPlayer.skipOnDrop = true;
    }

}
