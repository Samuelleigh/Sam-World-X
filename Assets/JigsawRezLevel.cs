using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawRezLevel : MonoBehaviour
{

    public RenderTexture bloop;
    public JigsawGameLogic gm;

    public Camera squareCam;
    public Camera horizontalCam;
    public Camera verticleCam;

    public GameObject Square;
    public GameObject Horizontal;
    public GameObject Verticle;


    private void Awake()
    {
        gm = FindObjectOfType<JigsawGameLogic>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BriefPause());

        gm.puzzleCam[0].GetComponent<Camera>().orthographic = true;

        if (gm.Level.puzzleResolution == PuzzleResolution._800x800) 
        {
            if (gm.Level.name == "64 Bit") 
            {
                bloop = new RenderTexture(64, 64, 16,RenderTextureFormat.ARGB32);
            }

            if (gm.Level.name == "16 Bit")
            {
                bloop = new RenderTexture(16, 16, 16, RenderTextureFormat.ARGB32);
            }

            if (gm.Level.name == "8 Bit")
            {
                bloop = new RenderTexture(8, 8, 16, RenderTextureFormat.ARGB32);
            }

            bloop.Create();
            bloop.filterMode = FilterMode.Point;
            squareCam.GetComponent<Camera>().targetTexture = bloop;
            Square.GetComponent<MeshRenderer>().material.mainTexture = bloop;

        }

        if (gm.Level.puzzleResolution == PuzzleResolution._1248x702)
        {
            if (gm.Level.name == "64 Bit")
            {
                bloop = new RenderTexture(114, 64, 16, RenderTextureFormat.ARGB32);
            }

            if (gm.Level.name == "16 Bit")
            {
                bloop = new RenderTexture(28, 16, 16, RenderTextureFormat.ARGB32);

            }

            if (gm.Level.name == "8 Bit")
            {
                bloop = new RenderTexture(14, 8, 16, RenderTextureFormat.ARGB32);
            }

            bloop.Create();
            bloop.filterMode = FilterMode.Point;
            horizontalCam.GetComponent<Camera>().targetTexture = bloop;
            Horizontal.GetComponent<MeshRenderer>().material.mainTexture = bloop;

        }

        if (gm.Level.puzzleResolution == PuzzleResolution._450x800)
        {

            if (gm.Level.name == "64 Bit")
            {
                bloop = new RenderTexture(36, 64, 16, RenderTextureFormat.ARGB32);
            }

            if (gm.Level.name == "16 Bit")
            {
                bloop = new RenderTexture(16, 9, 16, RenderTextureFormat.ARGB32);

            }

            if (gm.Level.name == "8 Bit")
            {
                bloop = new RenderTexture(8, 5, 16, RenderTextureFormat.ARGB32);

            }

            bloop.Create();
            bloop.filterMode = FilterMode.Point;
            verticleCam.GetComponent<Camera>().targetTexture = bloop;
            Verticle.GetComponent<MeshRenderer>().material.mainTexture = bloop;



        }
    }

    public IEnumerator BriefPause() 
    {
        yield return new WaitForSeconds(0.1f);

        if (gm.Level.puzzleResolution == PuzzleResolution._1248x702)
        {
            gm.puzzleCam[0].transform.position = horizontalCam.transform.position;
                
        }

        if (gm.Level.puzzleResolution == PuzzleResolution._450x800)
        {
            gm.puzzleCam[0].transform.position = verticleCam.transform.position;
        }

        if (gm.Level.puzzleResolution == PuzzleResolution._800x800)
        {
            gm.puzzleCam[0].transform.position = squareCam.transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
