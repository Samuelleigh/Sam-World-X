using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace MovingJigsaw
{
    [System.Serializable]
    public class JigsawLevel
    {
      
        public JigLevelScriptObject jigsawLevelDefaults;
        public List<JigsawlevelSave> jigsawLevelActive = new List<JigsawlevelSave>();
   

    }

    [System.Serializable]
    public class JigsawPieceSavePostion 
    {

        public int JigId;
        public bool placed;
        public bool inDrawer;
        public bool destroyed;
        public float freeX, freeY;
        public int placedX, placedY;

   
    }

    [System.Serializable]
    public class JigsawlevelSave 
    { 
        public string name;
        public bool completed = false;
        public bool loadFromResource = false;
        public bool inProgress = false;
        public bool customMode = false;
        public PuzzleResolution puzzleResolution;
        public int XCustom = 1;
        public int YCustom = 1;
        public List<JigsawPieceSavePostion> savePiece = new List<JigsawPieceSavePostion>();
        public string pathURL;
        public int numberofpuzzles = 1;
        public List<int> solvedPuzzles;
      //  public int numberOfPuzzles;
 
 

    }
}