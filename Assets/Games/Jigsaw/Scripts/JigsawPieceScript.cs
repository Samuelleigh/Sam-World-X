using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawPieceScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int ID;
    public int puzzleID;
    public Vector2Int targetSpace;
    public GameObject child;
    public JigsawGameLogic gm;

    private void Awake()
    {
        gm = FindObjectOfType<JigsawGameLogic>();
    }


    void Start()
    {
        
    }

    public void SetMask(int targetX, int targetY, float cellsizeX, float cellsizeY, int puzzleid) 
    {


        puzzleID = puzzleid;
        targetSpace.y = (targetY);
        targetSpace.x = (targetX);



       float xOffset = (cellsizeX * targetSpace.x);
       float yOffset = (cellsizeY* targetSpace.y);

       float x = child.transform.localPosition.x - xOffset;
       float y = child.transform.localPosition.y + yOffset;

        Vector3 v = new Vector3(x, y, 0);



        child.transform.localPosition = v;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
