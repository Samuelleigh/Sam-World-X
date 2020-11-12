using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.IO;
using UnityEditor;

namespace MovingJigsaw
{

    public enum ShiftDirection { up, down, left, right, no }

    public class JigsawGameLogic : MonoBehaviour
    {
        public bool Debugbool;
        public JigLevelManager levels;
        public UIMaster ui;
        public List<RenderTexture> renderTexture = new List<RenderTexture>();

        //jigsaw scriptable object stuff
        public JigsawScriptObject Level;
        public int totalPieces;

        public float cellSizeX;
        public float cellSizeY;

        public GameObject piece;
        public GameObject snapObj;
        public GameObject snapboard;
        public GameObject nonPieces;
        public GameObject box;
        public GridLayout layout;
        public GameObject shiftFrame;
        public Canvas can;

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
        private List<int> solvedpuzzles = new List<int>();

        private int lastSolvedPuzzle;

        public GameObject ComfirmClear;

        public Vector2 boardSize;
        public int resoultionX;
        public int resoultionY;

        public GameObject loadingscreen;

        public VideoClip resourceVideo;

        public GameObject playergizmos;
        public GameObject ScrollViewStuff;

        public int Xpieces;
        public int Ypieces;
        public PuzzleResolution puzzleRez;

        private void Awake()
        {
            loadingscreen.SetActive(true);
            levels = FindObjectOfType<JigLevelManager>();
            ui = FindObjectOfType<UIMaster>();
        }

        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine(LoadingScreen());
            Level = levels.Jigsaws[levels.playID].jigsawLevelInfo.JigLevels[levels.altID];
            solvedAmountText.text = puzzlessolved + "/" + Level.numberOfpuzzles;


            if (levels.debug) { ui.SwitchLayer(1); LoadLevel(Level); }
            else { ui.SwitchLayer(1); LoadLevel(Level); }

            //   puzzleCam = GameObject.Find("Puzzle Camera");
            ShiftAllPieces(ShiftDirection.no);
        }

        // Update is called once per frame
        void Update()
        {

        }



        public void LoadLevel(JigsawScriptObject level)
        {

            SceneManager.LoadScene(level.SceneName, LoadSceneMode.Additive);



            if (levels.customMode)
            {
                Xpieces = levels.CustomX;
                Ypieces = levels.CustomY;
                puzzleRez = levels.Customrez;

            }
            else
            {
                Xpieces = level.Xpieces;
                Ypieces = level.Ypieces;
                puzzleRez = level.puzzleResolution;

            }

            //Set up the Render texture
            switch (puzzleRez)
            {
                case PuzzleResolution._800x800:
                    resoultionX = 800;
                    resoultionY = 800;
                    break;
                case PuzzleResolution._1248x702:
                    resoultionX = 1248;
                    resoultionY = 702;
                    break;
                case PuzzleResolution._450x800:
                    resoultionX = 450;
                    resoultionY = 800;
                    break;
            }

            //new render texture for each puzzle

            for (int i = 0; i < puzzleCam.Count; i++)
            {
                puzzleCam[i].GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
                puzzleCam[i].GetComponent<Camera>().targetTexture.Release();
                puzzleCam[i].GetComponent<Camera>().targetTexture = new RenderTexture(resoultionX, resoultionY, 24, RenderTextureFormat.Default);
                renderTexture.Add(puzzleCam[i].GetComponent<Camera>().targetTexture);
            }

            //if cetner alighned
            boardSize = new Vector2(resoultionX, resoultionY);


            //turn on features
            if (level.enableShift == false)
            {
                shiftFrame.SetActive(false);
            }

            totalPieces = Xpieces * Ypieces;
            cellSizeX = resoultionX / Xpieces;
            cellSizeY = resoultionY / Ypieces;



            snapboard.GetComponent<RectTransform>().sizeDelta = boardSize;
            intialPuzzleGrid.cellSize = new Vector2(cellSizeX, cellSizeY);
            intialSnapGrid.cellSize = new Vector2(cellSizeX, cellSizeY);
            //for the number of pieces instantiate each piece, set the cell cordinate during this

            int x = 0;
            int y = 0;

            // puzzleCam.transform.position = GameObject.Find(level.cameraID.ToString()).transform.position;

            for (int I = 0; I < level.numberOfpuzzles; I++)
            {
                RenderTexture r = renderTexture[I];

                Debug.Log(totalPieces);

                for (int i = 0; i < totalPieces; i++)
                {
                    //Spawn puzzle pieces
                    GameObject newpiece = Instantiate(piece, box.transform);
                    y = Mathf.CeilToInt(i / Xpieces);
                    x = i - (y * Xpieces);
                    //  Debug.Log(x + " " + y);

                    //set the puzzle piece ID
                    newpiece.GetComponent<JigsawPieceScript>().ID = i;

                    //Set the size of the piece to be cell
                    newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeY);
                    newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeX);
                    newpiece.GetComponent<JigsawPieceScript>().SetMask(x, y, cellSizeX, cellSizeY, I, boardSize);
                    newpiece.GetComponentInChildren<RawImage>().texture = r;


                    PuzzlePieces.Add(newpiece);
                }

                // Invoke("TurnOfflayoutGroup", 0.1f);

            }

            GridLayoutGroup glg;
            glg = snapboard.GetComponent<GridLayoutGroup>();
            glg.constraint = GridLayoutGroup.Constraint.FixedRowCount;  //**
            glg.constraintCount = Ypieces;

            if (Ypieces == 1)
            {
                glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                glg.constraintCount = Xpieces;
            }

            for (int i = 0; i < totalPieces; i++)
            {

                GameObject snap = Instantiate(snapObj, snapboard.transform);
                snap.GetComponent<SnapPiece>().x = x;
                snap.GetComponent<SnapPiece>().y = y;


                snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeX);
                snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeY);

            }

            //second Puzzle 


            //randomize 
            for (int i = 0; i < totalPieces * 2; i++)
            {

                int randomIndex = Random.Range(0, box.transform.childCount);
                PuzzlePieces[randomIndex].transform.SetAsLastSibling();


            }




            Invoke("MoveCamera", 1f);


            Invoke("TurnOfflayoutGroup", 1f);

            //  if (level.centerAligned) 
            // {
            //   Vector3 v = new Vector3(can.GetComponent<RectTransform>().rect.width / 2, can.GetComponent<RectTransform>().rect.height / 2, 0);
            //   nonPieces.GetComponentInParent<RectTransform>().position = v;


            // }

            if (resoultionY < 800)
            {
                resoultionY = 800;
            }

            if (resoultionX > 800)
            {
                ScrollViewStuff.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1750 - resoultionX);
            }

            ScrollViewStuff.GetComponent<ScrollRect>().horizontal = false;
            ScrollViewStuff.GetComponent<ScrollRect>().vertical = false;

        }


        public void CheckWin(int puzzlebeingchecked)
        {

            int numofpieces = totalPieces;
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

            float totalX = 0;

            //Randomizing the location 
            for (int i = 0; i < PuzzlePieces.Count; i++)
            {


                xbleep = Random.Range(-50, 50);
                ybleep = Random.Range(-50, 50);
                // yoffset = 600 * (Mathf.FloorToInt(i / (Level.Xpieces * Level.Ypieces)));
                add = new Vector3(xbleep, ybleep, 0);



                box.transform.GetChild(i).transform.position += add;

                if (box.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x > totalX)
                {
                    totalX = box.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x;
                    box.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalX + cellSizeX / 2 + 10);
                   // Debug.Log(totalX);
                }

            }



            //if (Level.centerAligned)
            //{
            //    Vector3 v = new Vector3(can.GetComponent<RectTransform>().rect.width / 2, can.GetComponent<RectTransform>().rect.height / 2, 0);
            //    nonPieces.GetComponentInParent<RectTransform>().position = v;

            //    RectTransform r;
            //    for (int i = 0; i < PuzzlePieces.Count; i++)
            //    {

            //        r = PuzzlePieces[i].GetComponent<RectTransform>();

            //        Debug.Log(r.position.x);
            //        if (r.position.x < 1350)
            //        {
            //           Debug.Log(PuzzlePieces[i].transform.position.x - 800);
            //            r.position = new Vector3(r.position.x - 800, r.position.y,0);

            //            ///   rect.Set(r.rect.x - 800, r.rect.y, r.rect.width, r.rect.height);

            //        }
            //    }

            //}

            foreach (GameObject jig in PuzzlePieces)
            {
                jig.GetComponent<JigsawPieceScript>().SaveStartTransform();

            }


            //player Gizmo Section

            playergizmos.GetComponent<HorizontalLayoutGroup>().enabled = false;
            playergizmos.GetComponent<ContentSizeFitter>().enabled = false;

            Debug.Log(playergizmos.transform.childCount);

            Transform[] t = new Transform[3];

            for (int i = 0; i < playergizmos.transform.childCount; i++)
            {
                t[i] = playergizmos.transform.GetChild(i);

            }

            for (int i = 0; i < t.Length; i++)
            {
                t[i].SetParent(ui.MasterLayers[1].transform);
                t[i].SetAsLastSibling();



            }








        }

        public void MoveCamera()
        {
            //move puzzle camera to the correct camera
            PuzzleEnviromentStuff p = FindObjectOfType<PuzzleEnviromentStuff>();

            for (int i = 0; i < Level.numberOfpuzzles; i++)
            {
                puzzleCam[i].transform.SetPositionAndRotation(p.Cameras[Level.cameraID[i]].transform.position, p.Cameras[Level.cameraID[i]].transform.rotation);
                puzzleCam[i].transform.parent = p.Cameras[Level.cameraID[i]].transform;
                puzzleCam[i].GetComponent<Camera>().orthographic = p.Cameras[Level.cameraID[i]].GetComponent<Camera>().orthographic;
                puzzleCam[i].GetComponent<Camera>().orthographic = p.Cameras[Level.cameraID[i]].GetComponent<Camera>().orthographic;
                puzzleCam[i].GetComponent<Camera>().nearClipPlane = p.Cameras[Level.cameraID[i]].GetComponent<Camera>().nearClipPlane;
            }
        }

        public void Winlevel(bool win, int puzzleWon)
        {

            if (win == true)
            {
                if (!solvedpuzzles.Contains(puzzleWon))
                {

                    solvedpuzzles.Add(puzzleWon);
                    puzzlessolved++;

                    solvedAmountText.text = puzzlessolved + "/" + Level.numberOfpuzzles;

                    if (puzzlessolved == Level.numberOfpuzzles)
                    {
                        congrats.SetActive(win);
                        levels.Jigsaws[levels.playID].completed = win;

                    }
                    else
                    {
                        lastSolvedPuzzle = puzzleWon;
                        ComfirmClear.SetActive(true);
                    }
                }
            }


        }


        public void BackToMenu()
        {

            levels.customMode = false;
            levels.Jigsaws[levels.playID].completed = win;
            levels.debug = false;

            List<bool> tempbool = new List<bool>();

            for (int i = 0; i < levels.Jigsaws.Count; i++) { tempbool.Add(levels.Jigsaws[i].completed); }



            JigSave.SavePlayer(tempbool);

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
                    if (IDList[i] % Xpieces == 0)
                    {
                        IDList[i] = IDList[i] - Xpieces;
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

                for (int i = 0; i < IDList.Count; i += Xpieces)
                {
                    IDList[i] += Xpieces;
                }
            }

            if (shift == ShiftDirection.up)
            {
                Debug.Log("da");
                for (int i = 0; i < IDList.Count; i++)
                {
                    //decrement  location 
                    IDList[i] += Xpieces;
                    //if increment causes the piece to skip to next line, push it back the current number
                    if ((IDList[i]) >= (Xpieces * Ypieces))
                    {
                        IDList[i] -= Xpieces * Ypieces;
                    }
                }
            }

            if (shift == ShiftDirection.down)
            {

                for (int i = 0; i < IDList.Count; i++)
                {
                    //decrement  location 
                    IDList[i] -= Xpieces;
                    //if increment causes the piece to skip to next line, push it back the current number
                    if (IDList[i] < 0)
                    {
                        IDList[i] += Xpieces * Ypieces;
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


            for (int i = 0; i < Level.numberOfpuzzles; i++)
            {
                CheckWin(i);
            }

        }

        public void RemoveCompletedPieces()
        {

            for (int i = 0; i < (Xpieces * Ypieces); i++)
            {

                PuzzlePieces[i + (Xpieces * Ypieces) * lastSolvedPuzzle].transform.parent = null;
                PuzzlePieces[i + (Xpieces * Ypieces) * lastSolvedPuzzle].SetActive(false);
            }

            ComfirmClear.SetActive(false);

        }

        public void Clean()
        {
            for (int i = 0; i < PuzzlePieces.Count; i++)
            {

                if (PuzzlePieces[i].transform.parent.name != "SnapPiece(Clone)")
                {
                    PuzzlePieces[i].GetComponent<JigsawPieceScript>().InstantPutBack();


                }


            }

        }

        public void ClearBoard()
        {

            SceneManager.LoadScene("JigsawMain", LoadSceneMode.Single);
        }

        public IEnumerator LoadingScreen()
        {
            yield return new WaitForSeconds(1);
            loadingscreen.SetActive(false);

        }
    }
}

