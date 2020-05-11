using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum ShiftDirection { up,down,left,right, no}

public class JigsawGameLogic : MonoBehaviour
{
    public bool Debugbool;
    public JigLevelManager levels;
    public UIMaster ui;
    public JigsawLevel levelPlaying;
    public List<RenderTexture> renderTexture;

    public float cellSizeX;
    public float cellSizeY;
    public GameObject piece;
    public GameObject snapObj;
    public GameObject snapboard;
    public GameObject box;
    public GridLayout layout;
    public GameObject shiftFrame;

    public List<GameObject> PuzzlePieces = new List<GameObject>();
    public bool win = false;

    public GameObject congrats;
    public List<GameObject> puzzleCam;
    public List<GameObject> potentialCameras;
    public TextMeshProUGUI solvedAmountText;

    public GridLayoutGroup intialPuzzleGrid;
    public GridLayoutGroup intialSnapGrid;

    public List<int> deleteme;

    public List<GameObject> temp = new List<GameObject>();

    private int puzzlessolved = 0;

    private void Awake()
    {
        levels = FindObjectOfType<JigLevelManager>();
        ui = FindObjectOfType<UIMaster>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(puzzlessolved);
        solvedAmountText.text = puzzlessolved + "/" + levelPlaying.numberOfpuzzles;
        //Debug.Log(0 % levelPlaying.Xpieces);
        if (levels.debug) { ui.SwitchLayer(1); LoadLevel(levels.DebugLevel); }
        else { ui.SwitchLayer(1); LoadLevel(levels.Jigsaws[levels.playID]); }
        //   puzzleCam = GameObject.Find("Puzzle Camera");
        ShiftAllPieces(ShiftDirection.no);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void LoadLevel(JigsawLevel level)
    {

        levelPlaying = level;

        SceneManager.LoadScene(level.SceneName, LoadSceneMode.Additive);

        //turn on features
        if (level.enableShift == false) 
        {
            shiftFrame.SetActive(false);
        
        }

        //set the sell size
        int numOfPieces = level.Xpieces * level.Ypieces;
        cellSizeX = renderTexture[0].height / level.Xpieces;
        cellSizeY = renderTexture[0].width / level.Ypieces;

        intialPuzzleGrid.cellSize = new Vector2(cellSizeX, cellSizeY);
        intialSnapGrid.cellSize = new Vector2(cellSizeX, cellSizeY);
        //for the number of pieces instantiate each piece, set the cell cordinate during this

        int x = 0;
        int y = 0;

        // puzzleCam.transform.position = GameObject.Find(level.cameraID.ToString()).transform.position;

        for (int I = 0; I < level.numberOfpuzzles; I++)
        {
            RenderTexture r = renderTexture[I];
            

            for (int i = 0; i < numOfPieces; i++)
            {
                //Spawn puzzle pieces
                GameObject newpiece = Instantiate(piece, box.transform);
                y = Mathf.CeilToInt(i / level.Xpieces);
                x = i - (y * level.Xpieces);
              //  Debug.Log(x + " " + y);

                //set the puzzle piece ID
                newpiece.GetComponent<JigsawPieceScript>().ID = i;

                //Set the size of the piece to be cell
                newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeY);
                newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeX);
                newpiece.GetComponent<JigsawPieceScript>().SetMask(x, y, cellSizeX, cellSizeY, I);
                newpiece.GetComponentInChildren<RawImage>().texture = r;

                PuzzlePieces.Add(newpiece);
            }

           // Invoke("TurnOfflayoutGroup", 0.1f);

        }

        for (int i = 0; i < numOfPieces; i++) 
        {

            GameObject snap = Instantiate(snapObj, snapboard.transform);
            snap.GetComponent<SnapPiece>().x = x;
            snap.GetComponent<SnapPiece>().y = y;

            snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeX);
            snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeY);

        }

        //second Puzzle 


        //randomize 
        for (int i = 0; i < numOfPieces * 2; i++)
        {

            int randomIndex = Random.Range(0, box.transform.childCount);
            PuzzlePieces[randomIndex].transform.SetAsLastSibling();


        }

        Invoke("MoveCamera", 0.1f);
        Invoke("TurnOfflayoutGroup", 0.1f);


    }


    public void CheckWin(int puzzlebeingchecked)
    {

        int numofpieces = levelPlaying.Xpieces * levelPlaying.Ypieces;
        int offset = numofpieces * puzzlebeingchecked;
        bool tempwinVar = true;


        Debug.Log(offset);
        for (int i = 0; i < numofpieces; i++)
        {  

            if (PuzzlePieces[i + offset].transform.parent != snapboard.transform.GetChild(i))
            {
                tempwinVar = false;
            }
  
        }

        win = tempwinVar;
        Winlevel(win, puzzlebeingchecked);
       
    }

    public void TurnOfflayoutGroup()
    {

        box.GetComponent<GridLayoutGroup>().enabled = false;


        Vector3 add;
        float xbleep;
        float ybleep;
        float yoffset;

        for (int i = 0; i < PuzzlePieces.Count; i++)
        {
         

            xbleep = Random.Range(-50, 50);
            ybleep = Random.Range(-50, 50);
            yoffset = 700 * (Mathf.FloorToInt(i / (levelPlaying.Xpieces * levelPlaying.Ypieces)));
            add = new Vector3(xbleep, ybleep + yoffset, 0);

           // Debug.Log(yoffset);
            box.transform.GetChild(i).transform.position += add;
        }


    }

    public void MoveCamera()
    {
        //move puzzle camera to the correct camera
        PuzzleEnviromentStuff p = FindObjectOfType<PuzzleEnviromentStuff>();

        for (int i = 0; i < levelPlaying.numberOfpuzzles; i++)
        {
            puzzleCam[i].transform.SetPositionAndRotation(p.Cameras[levelPlaying.cameraID[i]].transform.position, p.Cameras[levelPlaying.cameraID[i]].transform.rotation);
        }
    }

    public void Winlevel(bool win, int puzzleWon)
    {

        if (win == true)
        {
            puzzlessolved++;

            solvedAmountText.text = puzzlessolved + "/" + levelPlaying.numberOfpuzzles;

            if (puzzlessolved == levelPlaying.numberOfpuzzles)
            {
                congrats.SetActive(win);
                levels.Jigsaws[levels.playID].completed = win;

            }
            else 
            {
                for (int i = 0; i < (levelPlaying.Xpieces * levelPlaying.Ypieces); i++) 
                {

                PuzzlePieces[i + (levelPlaying.Xpieces * levelPlaying.Ypieces) * puzzleWon].transform.parent = null;
                PuzzlePieces[i + (levelPlaying.Xpieces * levelPlaying.Ypieces) * puzzleWon].SetActive(false);
                }
            }
        }

        
    }


    public void BackToMenu()
    {

        levels.Jigsaws[levels.playID].completed = win;
        levels.debug = false;

        JigSave.SavePlayer(levels.Jigsaws);

        SceneManager.LoadScene("JigsawMenu", LoadSceneMode.Single);

    }


    public void shift(string i)
    {
        if (i == "up") { ShiftAllPieces(ShiftDirection.up); }
        if (i == "down") { ShiftAllPieces(ShiftDirection.down); }
        if (i == "left") { ShiftAllPieces(ShiftDirection.left); }
        if (i == "right") { ShiftAllPieces(ShiftDirection.right); }

    }

    public void ShiftAllPieces(ShiftDirection shift)
    {
        List<int> IDList = new List<int>();
        deleteme = IDList;

        //set up 
        for (int i = 0; i < snapboard.transform.childCount; i++)
        {
            IDList.Add(i);
        }

        

        if (shift == ShiftDirection.left)
        {

            for (int i = 0; i < IDList.Count; i++)
            {
                //increment  location 
                IDList[i]++;
                //if increment causes the piece to skip to next line, push it back the current number
                if (IDList[i] % levelPlaying.Xpieces == 0)
                {
                    IDList[i] = IDList[i] - levelPlaying.Xpieces;
                }
            }
        }

        if (shift == ShiftDirection.right)
        {
            for (int i = 0; i < IDList.Count; i++)
            {
                //decrement  location 
                IDList[i]--;

            }

            for (int i = 0; i < IDList.Count; i += levelPlaying.Xpieces)
            {
                IDList[i] += levelPlaying.Xpieces;
            }
        }

        if (shift == ShiftDirection.up)
        {
            Debug.Log("da");
            for (int i = 0; i < IDList.Count; i++)
            {
                //decrement  location 
                IDList[i] += levelPlaying.Xpieces;
                //if increment causes the piece to skip to next line, push it back the current number
                if ((IDList[i]) >= (levelPlaying.Xpieces * levelPlaying.Ypieces))
                {
                    IDList[i] -= levelPlaying.Xpieces * levelPlaying.Ypieces;
                }
            }
        }

        if (shift == ShiftDirection.down)
        {

            for (int i = 0; i < IDList.Count; i++)
            {
                //decrement  location 
                IDList[i] -= levelPlaying.Xpieces;
                //if increment causes the piece to skip to next line, push it back the current number
                if (IDList[i] < 0)
                {
                    IDList[i] += levelPlaying.Xpieces * levelPlaying.Ypieces;
                }
            }

        }

  
        temp = new List<GameObject>();

        for (int i = 0; i < snapboard.transform.childCount; i++)
        {
            temp.Add(snapboard.transform.GetChild(i).gameObject);
          //  temp[IDList[i]].transform.SetParent(snapboard.transform);
        }


        for (int i = 0; i < temp.Count; i++) 
        {
           temp[IDList[i]].transform.SetAsLastSibling();
        }


        for (int i = 0; i < levelPlaying.numberOfpuzzles; i++)
        {
            CheckWin(i);
        }
        
    }

    public void ClearBoard() 
    {

        SceneManager.LoadScene("JigsawMain", LoadSceneMode.Single);
    }
}

