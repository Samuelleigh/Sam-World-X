using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace MovingJigsaw
{
    public class JigLevelManager : MonoBehaviour
    {
        public int debugID;
        public bool debug = true;
        public int playID;
        public int altID = 0;
        public string path;
        public bool customMode;


        public List<JigsawLevel> Jigsaws;
        public JigsawLevel DebugLevel;

        public int CustomX;
        public int CustomY;
        public PuzzleResolution Customrez;

        public bool customFile;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameObject g = GameObject.Find("Jigsaw Level Manager");

            //checks if any other game objects with this name 
            if (g != null && g != gameObject)
            {

                Destroy(gameObject);
            }


            if (debug == true)
            {
                //DebugLevel = Jigsaws[debugID];
            }


        }


        public void Start()
        {

        }
    }
}


