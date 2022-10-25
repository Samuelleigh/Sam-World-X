﻿using System.Collections;
using System.Collections.Generic;
using LightShaft.Scripts;
using UnityEngine;
using UnityEngine.Video;
using MovingJigsaw;

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

     
        

    }

    public void Play()
    {
        if (fullscreen)
        {
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        }
        player.autoPlayOnStart = autoPlay;
        player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;
        

        if (autoPlay)
        {
            
            player.Play(url);
            
        }
    }
}
