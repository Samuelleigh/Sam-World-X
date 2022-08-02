using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace MovingJigsaw
{
    public class JigLevelManager : MonoBehaviour
    {
        public bool justOpened = true;
        public int levelsetLastOpened;
        public int debugID;
        public bool debug = true;
        public int playID;
        public int altID = 0;
        public string path;
        public bool customMode;


        public List<JigsawLevel> Jigsaws;
        public List<JigsawLevel> StoryJigsaws;
        public List<JigsawLevel> WeridJigsaws;
        public List<JigsawLevel> SandBoxJigsaws;
        public JigsawLevel DebugLevel;

        public int CustomX;
        public int CustomY;
        public PuzzleResolution Customrez;

        public bool customFile;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameObject g = GameObject.Find("Jigsaw Level Manager");

            foreach (JigsawLevel jig in StoryJigsaws) 
            {

                jig.jigsawLevelActive = new List<JigsawlevelSave>(jig.jigsawLevelDefaults.JigLevels.Count);
            
            }


            //checks if any other game objects with this name 
            if (g != null && g != gameObject)
            {

                Destroy(gameObject);
            }

            if (debug == true)
            {
                //DebugLevel = Jigsaws[debugID];
            }

            foreach (JigsawLevel jiglevel in StoryJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

            foreach (JigsawLevel jiglevel in WeridJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

            foreach (JigsawLevel jiglevel in SandBoxJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

        }

       

        void Start() 
        {

            if (justOpened)
            {
                justOpened = false;
            }
            else 
            {

                JigsawMenu menus = FindObjectOfType<JigsawMenu>();
                
                menus.ChangeLevelSetMode(levelsetLastOpened);
                menus.ChangeUIView(2);
               


            }

            //Instantiate 

          

        }

     
        


    }
}


