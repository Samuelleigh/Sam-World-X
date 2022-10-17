using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

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

        public bool muteVideo = false;
        public bool muteSound = false;


        public List<JigsawLevel> Jigsaws;
        public List<JigsawLevel> StoryJigsaws;
        public List<JigsawLevel> WeridJigsaws;
        public List<JigsawLevel> SandBoxJigsaws;
        public JigsawLevel DebugLevel;

        public int CustomX;
        public int CustomY;
        public PuzzleResolution Customrez;

        public bool customFile;

        public SoundSystem soundsystem;



        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameObject g = GameObject.Find("Jigsaw Level Manager");


         //  //Create a number of empty lists under each jigsaw objects
         //
         //  foreach (JigsawLevel jig in StoryJigsaws) 
         //  {
         //
         //      jig.jigsawLevelActive = new List<JigsawlevelSave>(jig.jigsawLevelDefaults.JigLevels.Count);
         //  
         //  }

            soundsystem = FindObjectOfType<SoundSystem>();

            //checks if any other game objects with this name 
            if (g != null && g != gameObject)
            {

                Destroy(gameObject);
            }

            if (debug == true)
            {
                //DebugLevel = Jigsaws[debugID];
            }

            //for each jigsaw level in each sub list, 

            foreach (JigsawLevel jiglevel in StoryJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    newJig.numberofpuzzles = jigscript.numberOfpuzzles;                  
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

            foreach (JigsawLevel jiglevel in WeridJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    newJig.numberofpuzzles = jigscript.numberOfpuzzles;
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

            foreach (JigsawLevel jiglevel in SandBoxJigsaws)
            {
                foreach (JigsawScriptObject jigscript in jiglevel.jigsawLevelDefaults.JigLevels)
                {
                    JigsawlevelSave newJig = new JigsawlevelSave();
                    newJig.name = jigscript.name;
                    newJig.numberofpuzzles = jigscript.numberOfpuzzles;
                    jiglevel.jigsawLevelActive.Add(newJig);

                }

            }

            FlipMuteVideo();


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

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.M))
            {
                FlipMuteVideo();

            }
          
        }

        public void FlipMuteVideo() 
        {

            muteVideo = !muteVideo;

            AudioSource[] sources = FindObjectOfType<SoundSystem>().gameObject.GetComponents<AudioSource>();

            foreach (AudioSource s in sources)
            {
                s.mute = muteVideo;
            }


            if (FindObjectOfType<PuzzleEnviromentStuff>())
            {

                FindObjectOfType<PuzzleEnviromentStuff>().Mute();
            }

            if (FindObjectOfType<MainMenuMusicController>())
            {
                FindObjectOfType<MainMenuMusicController>().EnableBackgroundMusic();
            }



        
        }

        public void FlipMuteSound() 
        {
            muteSound = !muteSound;

            if (muteSound)
            {


            }
            else 
            {
            
            
            }
        
        }

       
     
        


    }
}


