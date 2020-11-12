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

    }
}