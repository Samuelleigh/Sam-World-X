using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingJigsaw
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Level")]
    [System.Serializable]

    public class JigLevelScriptObject : ScriptableObject
    {


        public string Name;
        public List<JigsawScriptObject> JigLevels = new List<JigsawScriptObject>(2);

        [TextArea]
        public string Notes;


    }
}
