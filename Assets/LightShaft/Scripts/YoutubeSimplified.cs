using System.Collections;
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

            

            if (manager.path.StartsWith("https:") && manager.customFile && manager.customMode)
            {
                url = manager.path;
                
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

        if(autoPlay)
            player.Play(url);
    }
}
