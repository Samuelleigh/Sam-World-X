using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;
using MovingJigsaw;

namespace MovingJigsaw
{
    public class JigsawSceneObject : MonoBehaviour
    {

        public JigLevelManager manager;
        public JigsawGameLogic gm;
        public VideoPlayer vid;

        public Texture m_MainTexture;
        Renderer m_Renderer;

        private void Awake()
        {
            manager = FindObjectOfType<JigLevelManager>();
            gm = FindObjectOfType<JigsawGameLogic>();

        }

        void Start()
        {

            if (manager.customFile && !manager.customMode) 
            {

                manager.customFile = false;
                manager.path = "";
            }


            if (manager.customFile == false)
            {
                if (gameObject.GetComponent<VideoPlayer>() == null)
                {
                  // Debug.Log("Ok");
                    vid = gameObject.AddComponent<VideoPlayer>();
                }
                else
                {
                //    Debug.Log("Test");
                    vid = gameObject.GetComponent<VideoPlayer>();
                }

               // Debug.Log("df");

                if (gm.Level.videoClip)
                {
                    vid.SetDirectAudioMute(0, manager.muteVideo);
                    vid.clip = gm.Level.videoClip;
                    vid.audioOutputMode = VideoAudioOutputMode.None;
                    vid.isLooping = true;
                }
                else if (gm.Level.puzzleTexture)
                {
                    m_MainTexture = gm.Level.puzzleTexture;
                    m_Renderer = GetComponent<MeshRenderer>();

                    m_Renderer.material.EnableKeyword("_NORMALMAP");
                    m_Renderer.material.EnableKeyword("_EMISSION");
                    m_Renderer.material.EnableKeyword("_METALLICGLOSSMAP");

                    Material mat = new Material(Shader.Find("Unlit/Texture"));
                    mat.SetTexture("_MainTex", m_MainTexture);
                    mat.SetTexture("_EMISSION", m_MainTexture);

                    m_Renderer.material = mat;
                }
            }


            if (manager.customFile == true && manager.customMode == true)
            {

                if (Path.GetExtension(manager.path) == ".mp4")
                {
                    if (gameObject.GetComponent<VideoPlayer>() == null)
                    {
                        vid = gameObject.AddComponent<VideoPlayer>();
                    }
                    else
                    {
                        vid = gameObject.GetComponent<VideoPlayer>();
                    }

                    Renderer rend = GetComponent<Renderer>();

                    rend.material.EnableKeyword("_NORMALMAP");
                    rend.material.EnableKeyword("_EMISSION");
                    rend.material.EnableKeyword("_METALLICGLOSSMAP");
                    rend.material = new Material(Shader.Find("Unlit/Texture"));

                    vid.SetDirectAudioMute(0, true);
                    vid.targetMaterialRenderer = gameObject.GetComponent<Renderer>();
                    vid.audioOutputMode = VideoAudioOutputMode.None;
                    vid.source = VideoSource.Url;
                    vid.isLooping = true;

                    vid.url = manager.path;

                }

                if (Path.GetExtension(manager.path) == ".png" || Path.GetExtension(manager.path) == ".PNG")
                {

                    StartCoroutine(GetImageFile());

                    if (gameObject.GetComponent<VideoPlayer>() == true)
                    {
                        vid = gameObject.AddComponent<VideoPlayer>();
                        Destroy(vid);
                    }

                    StartCoroutine(GetImageFile());

                }

            }

        }

        IEnumerator GetImageFile()
        {
            WWW www = new WWW(manager.path);
            while (!www.isDone)
                yield return null;
            m_MainTexture = www.texture;

            m_Renderer = GetComponent<MeshRenderer>();

            m_Renderer.material.EnableKeyword("_NORMALMAP");
            m_Renderer.material.EnableKeyword("_EMISSION");
            m_Renderer.material.EnableKeyword("_METALLICGLOSSMAP");


            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.SetTexture("_MainTex", m_MainTexture);
            mat.SetTexture("_EMISSION", m_MainTexture);

            m_Renderer.material = mat;

        }  
    }
}