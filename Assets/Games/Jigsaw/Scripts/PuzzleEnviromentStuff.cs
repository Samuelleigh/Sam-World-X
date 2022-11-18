﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using LightShaft.Scripts;

namespace MovingJigsaw
{
    public class PuzzleEnviromentStuff : MonoBehaviour
    {

        public List<GameObject> Cameras;
        public VideoPlayer[] vids;
        public VideoPlayer videoPlayerInChargeOfAudio;
        public JigLevelManager jigManager;
        public bool unmute = true;

        public YoutubePlayer[] youtubePlayers;

        public void Start()
        {
            youtubePlayers = FindObjectsOfType<YoutubePlayer>();
            Invoke("UnmuteStart",0.01f);
           // UnmuteStart();
            jigManager = FindObjectOfType<JigLevelManager>();


            if (!jigManager.customMode) 
            {
                foreach (YoutubePlayer p in youtubePlayers) 
                {
                    p.gameObject.SetActive(false);
                }
            }

            if (jigManager.muteVideo == true) 
            {
                videoPlayerInChargeOfAudio.SetDirectAudioMute(0, true);

            }




        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) 
            {
                Mute();
            
            }
        }


        public void Mute() 
        {
                   
            if (jigManager.muteVideo)
            {
                videoPlayerInChargeOfAudio.SetDirectAudioMute(0, true);
            }
            else
            {
                videoPlayerInChargeOfAudio.SetDirectAudioMute(0, false);
            }

            
        }


        public void UnmuteStart() 
        {
            vids = FindObjectsOfType<VideoPlayer>();
            Debug.Log("ummuteStart");

            if (vids.Length > 0) 
            {

                videoPlayerInChargeOfAudio = vids[0];


                for (int i = 0; i < vids.Length; i++)
                {
                    vids[i].SetDirectAudioMute(0, true);

                }

             //  videoPlayerInChargeOfAudio.gameObject.SetActive(false);
             //  videoPlayerInChargeOfAudio.gameObject.SetActive(true);

                videoPlayerInChargeOfAudio.audioOutputMode = VideoAudioOutputMode.Direct;
                videoPlayerInChargeOfAudio.SetDirectAudioMute(0, false);
                videoPlayerInChargeOfAudio.SetDirectAudioVolume(0, 0.5f);

              // videoPlayerInChargeOfAudio.gameObject.SetActive(false);
              // videoPlayerInChargeOfAudio.gameObject.SetActive(true);

            }




        }

    }
}
