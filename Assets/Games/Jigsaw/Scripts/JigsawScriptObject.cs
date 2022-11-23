using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using MovingJigsaw;

public enum PuzzleResolution {_800x800, _1248x702,_450x800, Custom};



namespace MovingJigsaw
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Jigsaw")]
    [System.Serializable]
    public class JigsawScriptObject : ScriptableObject
    {

        public new string name;
        public int Xpieces;
        public int Ypieces;
        public PuzzleResolution puzzleResolution;
        private int XCustom = 1;
        private int YCustom = 1;
        public string SceneName;
        public int numberOfpuzzles = 1;
        public List<int> cameraID;
        public bool enableShift = true;
        public string style;
        public int difficulty;
        public bool centerAligned = false;
        public bool allowCustomFile = true;
        public VideoClip videoClip;
        public Texture puzzleTexture;
        public bool Customizable = true;
        public JigGameMode gameMode;
        [TextArea]
        public string Notes;

    }
}