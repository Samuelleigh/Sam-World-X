using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace MovingJigsaw
{
    [System.Serializable]
    public class JigsawLevel
    {
        public string name;
        public JigLevelScriptObject jigsawLevelInfo;
        public bool completed = false;
        public bool loadFromResource = false;
        public bool inProgress = false;
        public List<JigsawPieceSavePostion> savePiece = new List<JigsawPieceSavePostion>();
    }

    [System.Serializable]
    public class JigsawPieceSavePostion 
    {

        public int JigId;
        public bool placed;
        public int freeX, freeY;
        public int placedX, placedY;


    
    }
}