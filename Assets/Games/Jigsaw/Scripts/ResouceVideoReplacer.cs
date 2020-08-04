using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ResouceVideoReplacer : MonoBehaviour
{

    public List<VideoPlayer> videoplayers;
    public JigsawGameLogic jiglogic;


    private void Awake()
    {
        jiglogic = FindObjectOfType<JigsawGameLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (jiglogic.levels.path != "") 
        {

            foreach (VideoPlayer v in videoplayers) 
            {
                v.source = VideoSource.Url;
                v.url = jiglogic.levels.path;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
