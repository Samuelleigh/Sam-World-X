using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;

public class JigsawSceneObject : MonoBehaviour
{

    public JigLevelManager manager;
    public VideoPlayer vid;

    public Texture m_MainTexture;
    Renderer m_Renderer;

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
        
    }

    


    // Start is called before the first frame update
    void Start()
    {
        

        if (manager.customFile == true && manager.customMode == true) 
        {

            StartCoroutine(GetImageFile());

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

                vid.targetMaterialRenderer = gameObject.GetComponent<Renderer>();
                vid.source = VideoSource.Url;
                vid.isLooping = true;

                vid.url = manager.path;


            }
           
            if ( Path.GetExtension(manager.path) == ".png" || Path.GetExtension(manager.path) == ".PNG")
            {

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


        Material mat = new Material(Shader.Find("Specular"));
        mat.SetTexture("_MainTex", m_MainTexture);

        m_Renderer.material = mat;

        Debug.Log("gg");

        //"C:\Users\sleig\Desktop\adw.png"

        //"C:\Users\sleig\Desktop\adw.png"
    }


    // Update is called once per frame
    void Update()
    {
      //  m_Renderer.material.mainTexture = m_MainTexture;
    }
}
