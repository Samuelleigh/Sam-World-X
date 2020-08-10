using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class JigsawMenu : MonoBehaviour
{
    public JigLevelManager manager;
    public GameObject Button;
    public List<GameObject> buts = new List<GameObject>();
    public GameObject contentView;
    public GameObject infoView;
    public List<GameObject> altButtons;

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

    public List<bool> aa = new List<bool>();

    public TMP_InputField filePathInput;

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

        infoView.SetActive(false);
        filepathFrame.SetActive(false);
        customizeMode = false;

        if (allowsave)
        {
            //saving stuff
            List<bool> temp = JigSave.LoadPlayer();

            aa = temp;

            for (int i = 0; i < manager.Jigsaws.Count; i++) 
            {
                manager.Jigsaws[i].completed = temp[i];
            }

            if (temp != null) 
            {

                for (int i = 0; i < manager.Jigsaws.Count; i++)
                {
                    manager.Jigsaws[i].completed = false;
                }

            }
        }


        int total = 0;
        int majorlevels = 1;
        int altnums = 0;
        string[] letters = new string[] {"","a","b","c","d", "e", "f", "g", "h", "i", "j" };

        for (int i = 0; i < manager.Jigsaws.Count; i++)
        {
            GameObject but;
            int ID = i;
            

            but = Instantiate(Button, contentView.transform);

            but.GetComponentInChildren<TextMeshProUGUI>().text = (majorlevels) + letters[altnums] + ". " + manager.Jigsaws[i].jigsawLevelInfo.Name;
            but.GetComponent<Button>().onClick.AddListener(() => Changelevel(ID,0,but));
            but.transform.SetAsLastSibling();
            buts.Add(but);
            total++;

            if (manager.Jigsaws[i].completed == true) { but.GetComponent<Image>().color = Color.green; }
            majorlevels++;
        }

        contentView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, total * 100);

       // delegate { Changelevel(i - 1); }
    }

    public void Changelevel(int jigsawID, int altID, GameObject obj)
    {

        choosenJigsaw = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID];

        socialFrame.SetActive(false);

        thingselected = true;
        playbuttontext.text = "Click Here to Start";

        infoView.SetActive(true);

      //  Debug.Log(jigsawID);
        loadID = jigsawID;

        if (customizeMode == false)
        {

            //populate the info panel data
            InfoText[0].text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].name.ToString();
            InfoText[1].text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Xpieces.ToString();
            InfoText[2].text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Ypieces.ToString();
            InfoText[3].text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].puzzleResolution.ToString().Substring(1);
            InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].numberOfpuzzles.ToString();
        }
        else 
        {
            InfoText[0].text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].name.ToString();
            InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].numberOfpuzzles.ToString();

            infoCustom[0].GetComponent<TMP_InputField>().text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Xpieces.ToString();
            infoCustom[1].GetComponent<TMP_InputField>().text = manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Ypieces.ToString();       
            infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();

            


        }

        //Remove alts 
        for (int i = 0; i < altButtons.Count; i++) 
        {

            int numberofbeing = altButtons.Count - manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels.Count;

            if (i < manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels.Count)
            { altButtons[i].SetActive(true);}
            else 
            {
                altButtons[i].SetActive(false);
            }
           

        }


        for (int i = 0; i < manager.Jigsaws.Count; i++) 
        {

            if (manager.Jigsaws[i].completed == true) 
            {
                buts[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                buts[i].GetComponent<Image>().color = Color.white;
            }
        }

        ChangeAlt(0);

        obj.GetComponent<Image>().color = Color.grey;


    }

    public void ChangeAlt(int altID) 
    {



        altLoadID = altID;
        choosenJigsaw = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID];

        InfoText[0].text = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].name.ToString();
        InfoText[1].text = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].Xpieces.ToString();
        InfoText[2].text = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].Ypieces.ToString();
        InfoText[3].text = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].puzzleResolution.ToString().Substring(1);
        InfoText[4].text = "Number of puzzles:  " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].numberOfpuzzles.ToString();

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

        altButtons[altID].GetComponent<Image>().color = Color.grey;


    }

    public void Startlevel()
    {
        if (customizeMode == true) 
        {
            manager.customMode = true;
           // infoCustom[0].GetComponent<TMP_InputField>().text = choosenJigsaw.Xpieces.ToString();
           // infoCustom[1].GetComponent<TMP_InputField>().text = choosenJigsaw.Ypieces.ToString();
           // infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();


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

        customizeMode = !customizeMode;



        if (customizeMode == true)
        {
            filepathFrame.SetActive(true);

            InfoText[1].gameObject.SetActive(false);
            InfoText[2].gameObject.SetActive(false);
            InfoText[3].gameObject.SetActive(false);

            infoCustom[0].SetActive(true);
            infoCustom[1].SetActive(true);
            infoCustom[2].SetActive(true);

            infoCustom[0].GetComponent<TMP_InputField>().text = choosenJigsaw.Xpieces.ToString();
            infoCustom[1].GetComponent<TMP_InputField>().text = choosenJigsaw.Ypieces.ToString();
            infoCustom[2].GetComponent<TMP_Dropdown>().value = choosenJigsaw.puzzleResolution.GetHashCode();

           

            if (manager.path != null) { 
            filePathInput.text = manager.path;
        }

        }

        if (customizeMode == false) 
        {
            filepathFrame.SetActive(false);

            InfoText[1].gameObject.SetActive(true);
            InfoText[2].gameObject.SetActive(true);
            InfoText[3].gameObject.SetActive(true);

            infoCustom[0].SetActive(false);
            infoCustom[1].SetActive(false);
            infoCustom[2].SetActive(false);

        }

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
            if(manager.CustomX > 20) 
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

    public void CheckIfFileExists() 
    {

       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
