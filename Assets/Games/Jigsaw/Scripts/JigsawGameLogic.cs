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
        public JigsawlevelSave CustomLevel;
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
        public int numberOfPuzzles;

        private bool tempinProgress;

        private void Awake()
        {
            loadingscreen.SetActive(true);
            levels = FindObjectOfType<JigLevelManager>();
            ui = FindObjectOfType<UIMaster>();
            tempinProgress = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].inProgress;
        }

        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine(LoadingScreen());
            Level = levels.Jigsaws[levels.playID].jigsawLevelDefaults.JigLevels[levels.altID];
            CustomLevel = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID];

            if (levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles != null)
            {
                solvedAmountText.text = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles.Count + "/" + Level.numberOfpuzzles;
            }


            if (levels.debug) { ui.SwitchLayer(1); LoadLevel(Level); }
            else { ui.SwitchLayer(1); LoadLevel(Level); }

            ShiftAllPieces(ShiftDirection.no);
        }


        public void LoadLevel(JigsawScriptObject level)
        {
            Debug.Log(Xpieces);
            SceneManager.LoadScene(level.SceneName, LoadSceneMode.Additive);

            if (CustomLevel.customMode)
            {
              
                Xpieces = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].XCustom;
                Ypieces = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].YCustom;
                puzzleRez = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].puzzleResolution;
                levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].customMode = true;
                numberOfPuzzles = levels.Jigsaws[levels.playID].jigsawLevelDefaults.JigLevels[levels.altID].numberOfpuzzles;
                

                Debug.Log(Xpieces);

            }
            else
            {
                
                Xpieces = level.Xpieces;
                Ypieces = level.Ypieces;
                puzzleRez = level.puzzleResolution;
                levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].customMode = false;
                numberOfPuzzles = levels.Jigsaws[levels.playID].jigsawLevelDefaults.JigLevels[levels.altID].numberOfpuzzles;
                Debug.Log(Xpieces);
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
            Debug.Log("Resoultion X" + resoultionX + " X Pieces:" + Xpieces);

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
           

                    //set the puzzle piece ID
                    newpiece.GetComponent<JigsawPieceScript>().ID = i;

                    //Set the size of the piece to be cell
                    newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeY);
                    newpiece.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeX);
                    newpiece.GetComponent<JigsawPieceScript>().SetMask(x, y, cellSizeX, cellSizeY, I, boardSize);
                    newpiece.GetComponentInChildren<RawImage>().texture = r;


                    PuzzlePieces.Add(newpiece);
                }

            }

            GridLayoutGroup glg;
            glg = snapboard.GetComponent<GridLayoutGroup>();
            glg.constraint = GridLayoutGroup.Constraint.FixedRowCount;  
            glg.constraintCount = Ypieces;

            if (Ypieces == 1)
            {
                glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                glg.constraintCount = Xpieces;
            }

            for (int i = 0; i < totalPieces; i++)
            {

                GameObject snap = Instantiate(snapObj, snapboard.transform);
                Debug.Log(x);
                snap.GetComponent<SnapPiece>().x = i - (y * Xpieces);
                snap.GetComponent<SnapPiece>().y = Mathf.CeilToInt(i / Xpieces);


                snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSizeX);
                snap.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSizeY);

            }

 

            //randomize Piece Starting Location
            for (int i = 0; i < totalPieces * 2; i++)
            {
                int randomIndex = Random.Range(0, box.transform.childCount);
                PuzzlePieces[randomIndex].transform.SetAsLastSibling();
            }


            Invoke("MoveCamera", 1f);
            Invoke("TurnOfflayoutGroup", 1f);
            

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


            //mark level as in Progress
            levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].inProgress = true;
            levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].completed = false;

            //If no jigsaw piece info, then add each piece to the active save file
            //SetupSavePostions();


            if (levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles != null)
            {

                solvedpuzzles = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles;
            }
            else 
            {
                solvedpuzzles = new List<int>(1);
            }
            
            
            Debug.Log(solvedpuzzles.Count);

            for (int i = 0; i < solvedpuzzles.Count; i++) 
            {
                Winlevel(true, solvedpuzzles[i]);
            
            }


          
           

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
            float xOffset;
            float yOffset;
     
            float totalX = 0;

            //Randomizing the location 
            for (int i = 0; i < PuzzlePieces.Count; i++)
            {
                xOffset = Random.Range(-50, 50);
                yOffset = Random.Range(-50, 50);
                add = new Vector3(xOffset, yOffset, 0);

                if (!tempinProgress)
                {
                    box.transform.GetChild(i).transform.position += add;
        
                }
                else
                {
                    for (int b = 0; b < levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece.Count; b++)
                    {
                        JigsawPieceSavePostion piecesave = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece[i];
                        GameObject jigpiece = PuzzlePieces[i];

                        if (!levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece[i].inDrawer)
                        {
                            jigpiece.transform.SetParent(ui.MasterLayers[1].transform);
                            jigpiece.GetComponent<RectTransform>().anchoredPosition = new Vector2(piecesave.freeX, piecesave.freeY);
                        }
                        else 
                        {
                            jigpiece.GetComponent<RectTransform>().anchoredPosition = new Vector2(piecesave.freeX, piecesave.freeY);
                        }
                  
                    }
                }

                if (PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.x > totalX) 
                {

                    totalX = PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.x;
                    box.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalX + cellSizeX / 2 + 10);


                }

               //if (box.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x > totalX)
               //{
               //    totalX = box.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x;
               //    box.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalX + cellSizeX / 2 + 10);
               //
               //}


            }

            //Save Start Jigsaw location for piece reset.

            foreach (GameObject jig in PuzzlePieces)
            {
                jig.GetComponent<JigsawPieceScript>().SaveStartTransform();
            }

            playergizmos.GetComponent<HorizontalLayoutGroup>().enabled = false;
            playergizmos.GetComponent<ContentSizeFitter>().enabled = false;

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

            SetupSavePostions();
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

            Debug.Log(win);

            if (win == true)
            {

                Debug.Log(puzzleWon);



                if (!solvedpuzzles.Contains(puzzleWon))
                {

                    solvedpuzzles.Add(puzzleWon);
                    puzzlessolved++;
                    solvedAmountText.text = puzzlessolved + "/" + Level.numberOfpuzzles;

                    if (solvedpuzzles.Count == Level.numberOfpuzzles)
                    {
                        Debug.Log("solved puzzle count " + solvedpuzzles.Count + " number of puzzles " + Level.numberOfpuzzles);
                        congrats.SetActive(win);
                        levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].completed = win;

                    }
                    else
                    {
                        Debug.Log("solved puzzle count " + solvedpuzzles.Count + " number of puzzles " + Level.numberOfpuzzles);
                        lastSolvedPuzzle = puzzleWon;
                        ComfirmClear.SetActive(true);
                    }
                }
            }
       
            
        }

        public void FixSolvedPuzzle() 
        {
        
        
        }


        public void BackToMenu()
        {
            if (ComfirmClear.activeSelf == true)
            {
                RemoveCompletedPieces();
            }

            levels.customMode = false;
            levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].completed = win;
            levels.debug = false;
            List<JigsawlevelSave> tempbool = new List<JigsawlevelSave>();
         

             for (int i = 0; i < levels.StoryJigsaws.Count; i++) { tempbool.Add(levels.StoryJigsaws[i].jigsawLevelActive[levels.altID]); }
             for (int i = 0; i < levels.WeridJigsaws.Count; i++) { tempbool.Add(levels.WeridJigsaws[i].jigsawLevelActive[levels.altID]); }
             for (int i = 0; i < levels.SandBoxJigsaws.Count; i++) { tempbool.Add(levels.SandBoxJigsaws[i].jigsawLevelActive[levels.altID]); }


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
            }


            for (int i = 0; i < temp.Count; i++)
            {
                temp[IDList[i]].transform.SetAsLastSibling();
            }


            for (int i = 0; i < Level.numberOfpuzzles; i++)
            {
                CheckWin(i);
            }

            if (shift != ShiftDirection.no)
            {
                UpdateSavePostions();
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

            UpdateSavePostions();
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
            UpdateSavePostions();

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

        public void SetupSavePostions() 
        {
            if (levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece.Count == 0)
            {
                for (int i = 0; i < PuzzlePieces.Count; i++)
                {
                    JigsawPieceSavePostion piecepostion = new JigsawPieceSavePostion();

                    piecepostion.JigId = i;
                    piecepostion.destroyed = false;
                    piecepostion.placed = false;
                    piecepostion.inDrawer = true;
                    piecepostion.freeX = PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.x;
                    piecepostion.freeY = PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.y;
                    piecepostion.placedX = -1;
                    piecepostion.placedY = -1;

                    levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece.Add(piecepostion);

                }

            }
            else
            {
                ApplyLoadPieceLocations();
            }

          //  UpdateSavePostions();

        }

        public void UpdateSavePostions() 
        {
            Debug.Log("Update Postion");

            for (int i = 0; i < PuzzlePieces.Count; i++)
            {
                JigsawPieceSavePostion piecepostion = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece[i];

                piecepostion.JigId = i;
                //piecepostion.destroyed = PuzzlePieces[i].activeSelf
                piecepostion.inDrawer = PuzzlePieces[i].transform.parent == box.transform;
                piecepostion.freeX = PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.x;
                piecepostion.freeY = PuzzlePieces[i].GetComponent<RectTransform>().anchoredPosition.y;


                if (PuzzlePieces[i].activeSelf && PuzzlePieces[i].transform.parent.GetComponent<SnapPiece>())
                {
                    SnapPiece s = PuzzlePieces[i].transform.parent.GetComponent<SnapPiece>();
                    piecepostion.placedX = s.x;
                    piecepostion.placedY = s.y;
                    piecepostion.placed = true;
                }
                else 
                {
                    piecepostion.placedX = -1;
                    piecepostion.placedY = -1;
                    piecepostion.placed = false;
                }
 
                levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece[i] = piecepostion;
            }

            levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles = solvedpuzzles;
        }

        public void ApplyLoadPieceLocations() 
        {

            Debug.Log("Apply save postion");

            for (int i = 0; i < levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece.Count; i++)
            {
                JigsawPieceSavePostion piecesave = levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].savePiece[i];
                GameObject jigpiece = PuzzlePieces[i];

                if (piecesave.destroyed)
                {
                    jigpiece.SetActive(false);
                }

                if (piecesave.inDrawer)
                {
                    jigpiece.transform.SetParent(box.transform);
                }

                if (!piecesave.inDrawer && !piecesave.placed)
                {

                    jigpiece.transform.SetParent(ui.MasterLayers[1].transform);
                }

                if (levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles.Contains(piecesave.JigId) && levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles[levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles.Count -1] != piecesave.JigId) 
                {
                    jigpiece.SetActive(false);
                
                }

                if (piecesave.placed)
                {
                    Debug.Log("piece was placed");

                    foreach (GameObject g in temp)
                    {
                        Debug.Log(g.GetComponent<SnapPiece>().x);

                        if (g.GetComponent<SnapPiece>().x == piecesave.placedX && g.GetComponent<SnapPiece>().y == piecesave.placedY)
                        {
                            Debug.Log("apply snap piece from save");
                            jigpiece.GetComponent<JigsawPieceDrag>().SnapToPiece(g);
                        }

                    }

                }
                else 
                {
                  //  Debug.Log("WWW");
                  //  jigpiece.GetComponent<RectTransform>().anchoredPosition.Set(piecesave.freeX, piecesave.freeY);
                }

            }


            


        }

        public void ApplyFinshedPuzzleNumber() 
        {

            if (levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].inProgress && levels.Jigsaws[levels.playID].jigsawLevelActive[levels.altID].solvedPuzzles.Count > 0) 
            {

                Debug.Log("");


            
            }
        }



    }
}

