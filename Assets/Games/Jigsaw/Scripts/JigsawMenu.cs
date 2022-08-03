using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace MovingJigsaw
{
    public class JigsawMenu : MonoBehaviour
    {
        public JigLevelManager manager;
        public GameObject Button;
        public List<GameObject> buts = new List<GameObject>();
        public GameObject contentView;
        public GameObject infoView;
        public List<GameObject> altButtons;
        public GameObject customizeableCheckbox;

        public JigsawScriptObject choosenJigsaw;

        public List<TextMeshProUGUI> InfoText;
        public List<GameObject> infoCustom;
        public TextMeshProUGUI playbuttontext;

        public int loadID = -1;
        public int altLoadID = -1;
        public bool allowsave = false;
        public bool thingselected = false;

        public bool customizeMode = false;
        public GameObject filepathFrame;
        public GameObject CustomInfoFrame;
        public GameObject socialFrame;

        public List<JigsawlevelSave> JigPuzzlesSaves = new List<JigsawlevelSave>();

        public TMP_InputField filePathInput;

        public List<GameObject> UIViews;
        public GameObject goBackButton;
        public GameObject customizationBlocker;
        public GameObject resetbutton;

        public Color inProgressColor;
        public Color SelectColor;
        public Color CompleteColor;

        private Color currentColor;

        private float darkenSelectColorAmount = 0.15f;
    

        private void Awake()
        {
            manager = FindObjectOfType<JigLevelManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            //Happens after jig Level Manager has set up it's time to 

            ChangeUIView(0);
            infoView.SetActive(false);
            filepathFrame.SetActive(false);
            customizeMode = false;

            if (!allowsave) 
            {
                JigSave.SeriouslyDeleteAllSaveFiles();
            }
       
                //saving stuff
                List<JigsawlevelSave> temp = JigSave.LoadPlayer();

                JigPuzzlesSaves = temp;

                for (int i = 0; i < manager.Jigsaws.Count; i++)
                {
                    //manager.Jigsaws[i].completed = temp[i];
                }

                //applys all the save save data to the each sub List


                if (temp != null)
                {


                   //applys all the save save data to the each sub List 

                    foreach (JigsawlevelSave info in JigPuzzlesSaves) 
                    {

                    Debug.Log(info.name);

                        for (int i = 0; i < manager.StoryJigsaws.Count; i++) 
                        {

                       //  Debug.Log("huh" + );


                             for (int b = 0; b < manager.StoryJigsaws[i].jigsawLevelActive.Count; b++)
                             {

                         //   Debug.Log("eat me");

                            if (info.name == manager.StoryJigsaws[i].jigsawLevelActive[b].name) 
                                 {
                                        manager.StoryJigsaws[i].jigsawLevelActive[b] = info;
                                   //     Debug.Log(info.name);
                                 }
              
                            }
                   
                        }


                    for (int i = 0; i < manager.WeridJigsaws.Count; i++)
                    {

                        for (int b = 0; b < manager.WeridJigsaws[i].jigsawLevelActive.Count; b++)
                        {

                            if (info.name == manager.WeridJigsaws[i].jigsawLevelActive[b].name)
                            {
                                manager.WeridJigsaws[i].jigsawLevelActive[b] = info;
                               // Debug.Log("wow 2");
                            }

                        }

                    }

                    for (int i = 0; i < manager.SandBoxJigsaws.Count; i++)
                    {

                        for (int b = 0; b < manager.SandBoxJigsaws[i].jigsawLevelActive.Count; b++)
                        {

                            if (info.name == manager.SandBoxJigsaws[i].jigsawLevelActive[b].name)
                            {
                                manager.SandBoxJigsaws[i].jigsawLevelActive[b] = info;
                              //  Debug.Log("wow 3");
                            }

                        }

                    }

                }

                }

            if (!manager.justOpened) 
            {
                ChangeLevelSetMode(manager.levelsetLastOpened);
                Changelevel(manager.playID, manager.altID, buts[manager.playID]);
                ChangeAlt(manager.altID);
                ChangeUIView(2);
            }

          //  PopulateScrollView(manager.StoryJigsaws);

          
        }

        public void PopulateScrollView(List<JigsawLevel> JigsawList ) 
        {
            buts = new List<GameObject>(); 

            foreach (Transform t in contentView.transform) 
            {
                Destroy(t.gameObject);
            }

            int total = 0;
            int majorlevels = 1;
            int altnums = 0;
            string[] letters = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

            for (int i = 0; i < JigsawList.Count; i++)
            {
                GameObject but;
                int ID = i;

                but = Instantiate(Button, contentView.transform);

                but.GetComponentInChildren<TextMeshProUGUI>().text = (majorlevels) + letters[altnums] + ". " + JigsawList[i].jigsawLevelDefaults.Name;
                but.GetComponent<Button>().onClick.AddListener(() => Changelevel(ID, 0, but));
                but.transform.SetAsLastSibling();
                buts.Add(but);
                total++;

            //    if (JigsawList[i].jigsawLevelActive[altLoadID].completed == true) { but.GetComponent<Image>().color = Color.green; }

                majorlevels++;
            }


            contentView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, total * 100);
            Changelevel(0, 0, buts[0]);
        }

        public void Changelevel(int jigsawID, int altID, GameObject obj)
        {
            choosenJigsaw = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID];

           

            ChangeCustomizeUI(customizeMode, choosenJigsaw.Customizable);

            thingselected = true;
            playbuttontext.text = "Click Here to Start";

            infoView.SetActive(true);

            loadID = jigsawID;

            if (customizeMode == false)
            {

                //populate the info panel data
                InfoText[0].text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].name.ToString();
                InfoText[1].text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].Xpieces.ToString();
                InfoText[2].text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].Ypieces.ToString();
                InfoText[3].text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].puzzleResolution.ToString().Substring(1);
                InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].numberOfpuzzles.ToString();
            }
            else
            {
                InfoText[0].text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].name.ToString();
                InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].numberOfpuzzles.ToString();

                infoCustom[0].GetComponent<TMP_InputField>().text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].Xpieces.ToString();
                infoCustom[1].GetComponent<TMP_InputField>().text = manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels[altID].Ypieces.ToString();
                infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();




            }

            //Remove alts 
            for (int i = 0; i < altButtons.Count; i++)
            {
                int numberofbeing = altButtons.Count - manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels.Count;

                if (i < manager.Jigsaws[jigsawID].jigsawLevelDefaults.JigLevels.Count)
                { altButtons[i].SetActive(true); }
                else
                {
                    altButtons[i].SetActive(false);
                }
            }

            //check if button is in progress or completed

            for (int i = 0; i < manager.Jigsaws.Count; i++)
            {
                if (manager.Jigsaws[i].jigsawLevelActive[altID].inProgress == true)
                {
                    buts[i].GetComponent<Image>().color = inProgressColor;
                }
                else 
                {
                    buts[i].GetComponent<Image>().color = Color.white;
                }


                //turns button green if all alts are completed

                bool allcompleted = true;

                
                    foreach (JigsawlevelSave j in manager.Jigsaws[i].jigsawLevelActive)
                    {
                        if(!j.completed)
                        {
                        allcompleted = false;
                        }

                    }

                if (allcompleted == true)
                {
                    buts[i].GetComponent<Image>().color = CompleteColor;
                }
            

            }

            //if all alt ids jigsaws are completed turn green
            ChangeAlt(0);


            //Change to selected color
            Color tempcolor = obj.GetComponent<Image>().color;
            obj.GetComponent<Image>().color = new Color(tempcolor.r - darkenSelectColorAmount, tempcolor.g - darkenSelectColorAmount, tempcolor.b - darkenSelectColorAmount);



            if (manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altLoadID].allowCustomFile && customizeMode == true)
            {
                filepathFrame.SetActive(true);
            }
            else
            {
                filepathFrame.SetActive(false);
            }

        }

        public void ChangeAlt(int altID)
        {
            altLoadID = altID;
            choosenJigsaw = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID];

            InfoText[0].text = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID].name.ToString();
            InfoText[1].text = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID].Xpieces.ToString();
            InfoText[2].text = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID].Ypieces.ToString();
            InfoText[3].text = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID].puzzleResolution.ToString().Substring(1);
            InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altID].numberOfpuzzles.ToString();

            if (customizeMode == true)
            {
                infoCustom[0].GetComponent<TMP_InputField>().text = choosenJigsaw.Xpieces.ToString();
                infoCustom[1].GetComponent<TMP_InputField>().text = choosenJigsaw.Ypieces.ToString();
                infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();
            }

            foreach (GameObject obj in altButtons)
            {
                obj.GetComponent<Image>().color = Color.white;
            }

            //if completed then turn green

            for (int i = 0; i < manager.Jigsaws[loadID].jigsawLevelActive.Count; i++)
            {



                if (manager.Jigsaws[loadID].jigsawLevelActive[i].completed)
                {
                    altButtons[i].GetComponent<Image>().color = CompleteColor;
                }
                else if(manager.Jigsaws[loadID].jigsawLevelActive[i].inProgress)
                {
                    Debug.Log("hifuh");
                    altButtons[i].GetComponent<Image>().color = inProgressColor;
                }

            
            }

            //if in progress, show start button and reset button. as well as blocker on info screen.

            if (manager.Jigsaws[loadID].jigsawLevelActive[altID].inProgress)
            {
                customizationBlocker.SetActive(true);
                resetbutton.SetActive(true);
            }
            else 
            {
                customizationBlocker.SetActive(false);
                resetbutton.SetActive(false);
            }

            //update custom size and tiles

            if (customizeMode == true && manager.Jigsaws[loadID].jigsawLevelActive[altID].customMode) 
            {
                manager.CustomX = manager.Jigsaws[loadID].jigsawLevelActive[altID].XCustom;
                manager.CustomY = manager.Jigsaws[loadID].jigsawLevelActive[altID].YCustom;

            }

            Color tempcolor = altButtons[altID].GetComponent<Image>().color;

            altButtons[altID].GetComponent<Image>().color =  new Color(tempcolor.r - darkenSelectColorAmount, tempcolor.g - darkenSelectColorAmount, tempcolor.b - darkenSelectColorAmount);

            if (manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altLoadID].allowCustomFile && customizeMode == true)
            {
                filepathFrame.SetActive(true);
                FindObjectOfType<VideoPathScript>().LoadFromSave(manager.Jigsaws[loadID].jigsawLevelActive[altID].pathURL) ;
            }
            else
            {
                filepathFrame.SetActive(false);
            }

            ChangeCustomizeUI(customizeMode, choosenJigsaw.Customizable);

        }

        public void Startlevel()
        {
            if (customizeMode == true)
            {
                manager.customMode = true;
                manager.Jigsaws[loadID].jigsawLevelActive[manager.altID].XCustom = manager.CustomX;
                manager.Jigsaws[loadID].jigsawLevelActive[manager.altID].YCustom = manager.CustomY;
                manager.Jigsaws[loadID].jigsawLevelActive[manager.altID].puzzleResolution = manager.Customrez;
             
            }

            if (loadID != -1 && thingselected == true)
            {
                manager.playID = loadID;
                manager.altID = altLoadID;
                manager.debug = false;

                SceneManager.LoadScene("JigsawMain", LoadSceneMode.Single);
            }
        }

        public void ChangeCustomMode()
        {

            if (customizeMode == true)
            {
                customizeMode = false;
            }
            else 
            {
                customizeMode = true;
            }

            ChangeCustomizeUI(customizeMode, choosenJigsaw.Customizable);

        }


        public void ChangeCustomX()
        {
            bool ifSuccess = int.TryParse(infoCustom[0].GetComponent<TMP_InputField>().text, out int result);

            if (ifSuccess)
            {
                manager.CustomX = int.Parse(infoCustom[0].GetComponent<TMP_InputField>().text);

                if (manager.CustomX <= 0)
                {
                    manager.CustomX = 1;
                    infoCustom[0].GetComponent<TMP_InputField>().text = manager.CustomX.ToString();
                }
                if (manager.CustomX > 20)
                {
                    manager.CustomX = 20;
                    infoCustom[0].GetComponent<TMP_InputField>().text = manager.CustomX.ToString();
                }
            }
            else
            {
                manager.CustomY = 1;
                infoCustom[0].GetComponent<TMP_InputField>().text = 1.ToString();

            }
        }

        public void ChangeCustomY()
        {
            bool ifSuccess = int.TryParse(infoCustom[1].GetComponent<TMP_InputField>().text, out int result);

            if (ifSuccess)
            {
                manager.CustomY = int.Parse(infoCustom[1].GetComponent<TMP_InputField>().text);
                if (manager.CustomY <= 0)
                {
                    manager.CustomY = 1;
                    infoCustom[1].GetComponent<TMP_InputField>().text = manager.CustomY.ToString();
                }
                if (manager.CustomY > 20)
                {
                    manager.CustomY = 20;
                    infoCustom[1].GetComponent<TMP_InputField>().text = manager.CustomY.ToString();
                }
            }
            else
            {
                manager.CustomY = 1;
                infoCustom[1].GetComponent<TMP_InputField>().text = 1.ToString();
            }
        }

        public void ChangeCustomRez()
        {
            manager.Customrez = (PuzzleResolution)infoCustom[2].GetComponent<TMP_Dropdown>().value;
        }



        public void ChangeLevelSetMode(int modeId)
        {

            manager.levelsetLastOpened = modeId;
            goBackButton.SetActive(true);

            switch (modeId) 
            {
                case 0:
                    manager.Jigsaws = manager.StoryJigsaws;                  
                    PopulateScrollView(manager.StoryJigsaws);
                    break;
                case 1:
                    manager.Jigsaws = manager.WeridJigsaws;
                    PopulateScrollView(manager.WeridJigsaws);
                    break;
                case 2:
                    manager.Jigsaws = manager.SandBoxJigsaws;
                    PopulateScrollView(manager.SandBoxJigsaws);
                    break;
            
            }

            ChangeUIView(2);

        }

        public void ChangeUIView(int i) 
        {

            foreach (GameObject g in UIViews) 
            {
                g.SetActive(false);

            }

            UIViews[i].SetActive(true);
            goBackButton.transform.SetParent(UIViews[i].transform);

        }

        public void GoBackButton() 
        {

            int currentview = 0;

            for (int i = 0; i < UIViews.Count; i++) 
            {
                if (UIViews[i].activeSelf) 
                {
                    currentview = i;                    
                }

                UIViews[i].SetActive(false);
            }

            Debug.Log(currentview);

            if (currentview != 0)
            {
                ChangeUIView(currentview - 1);           
                goBackButton.SetActive(true);
            }

            if(currentview == 1)
            {
                Debug.Log("turn off back button");
                goBackButton.SetActive(false);
            }
         
        }


        public void ChangeCustomizeUI(bool onOrNot, bool customizable)
        {
            if (onOrNot == true)
            {
                if (manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altLoadID].allowCustomFile)
                {
                    filepathFrame.SetActive(true);
                }

                InfoText[1].gameObject.SetActive(false);
                InfoText[2].gameObject.SetActive(false);
                InfoText[3].gameObject.SetActive(false);

                infoCustom[0].SetActive(true);
                infoCustom[1].SetActive(true);
                infoCustom[2].SetActive(true);

                infoCustom[0].GetComponent<TMP_InputField>().text = choosenJigsaw.Xpieces.ToString();
                infoCustom[1].GetComponent<TMP_InputField>().text = choosenJigsaw.Ypieces.ToString();
                infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();

                customizeMode = true;
                manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].customMode = true;

                if (manager.path != null)
                {
                    filePathInput.text = manager.path;
                }

            }

            if (onOrNot == false || customizable == false)
            {
                customizeMode = false;
               // manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].customMode = true;

                filepathFrame.SetActive(false);

                InfoText[1].gameObject.SetActive(true);
                InfoText[2].gameObject.SetActive(true);
                InfoText[3].gameObject.SetActive(true);

                infoCustom[0].SetActive(false);
                infoCustom[1].SetActive(false);
                infoCustom[2].SetActive(false);

                
            }

            

           
                customizeableCheckbox.SetActive(customizable);
          

        }

        public void ResetPuzzle() 
        {
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].inProgress = false;
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].completed = false;
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].customMode = false;
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].savePiece = new List<JigsawPieceSavePostion>();
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].solvedPuzzles = new List<int>();
            manager.Jigsaws[loadID].jigsawLevelActive[altLoadID].numberofpuzzles = manager.Jigsaws[loadID].jigsawLevelDefaults.JigLevels[altLoadID].numberOfpuzzles;
            ChangeAlt(altLoadID);

        }

        public void UpdateObjectColor(Image ImageToUpdate,bool Presseddown, bool inprogress, bool completed) 
        {

            int amount = -10;

            if (inprogress) 
            {
                ImageToUpdate.color = inProgressColor;
            }

            if (completed) 
            {
                ImageToUpdate.color = CompleteColor;
            }

            if (!inprogress && !completed) 
            {
                ImageToUpdate.color = Color.white;

            }

            if (Presseddown) 
            {
                ImageToUpdate.color = new Color(ImageToUpdate.color.r - amount, ImageToUpdate.color.g - amount, ImageToUpdate.color.b - amount);
            }

           
               // return new Color(currentcolor.r - amount, currentcolor.g - amount, currentcolor.b - amount);
            
          
       
        }

    }

   
   
}
