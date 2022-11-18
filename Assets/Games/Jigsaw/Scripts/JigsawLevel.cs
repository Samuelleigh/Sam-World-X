using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace MovingJigsaw
{

    public enum JigGameMode {Normal_Mode,Time_Attack,Battle,Boss_Rush,Limited_Moves,One_at_a_Time,Werid_Mode_1,Werid_Mode_2,Werid_Mode_3 }

    [System.Serializable]
    public class JigsawLevel
    {
      
        public JigLevelScriptObject jigsawLevelDefaults;
        public List<JigsawlevelSave> jigsawLevelActive = new List<JigsawlevelSave>();
   
    }

    public class TimeToComplete 
    {
        public string time;
        public string name;
        public string levelname;
        public string date;

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
        public List<int> solvedPuzzles = new List<int>();
        //  public int numberOfPuzzles;
        public JigGameMode gameMode;
        public List<TimeToComplete> timetoCompleteList;

    }
}