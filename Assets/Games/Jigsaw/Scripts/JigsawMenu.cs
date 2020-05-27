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
    public TextMeshProUGUI playbuttontext;

    public int loadID = -1;
    public int altLoadID = -1;
    public bool allowsave = false;
    public bool thingselected = false;

    public List<bool> aa = new List<bool>();

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

        infoView.SetActive(false);

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

        thingselected = true;
        playbuttontext.text = "Click Here to Start";

        infoView.SetActive(true);

      //  Debug.Log(jigsawID);
        loadID = jigsawID;

        //populate the info panel data
        InfoText[0].text = "Width: " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Xpieces.ToString();
        InfoText[1].text = "Height " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].Ypieces.ToString();
        InfoText[2].text = "Number of puzzles:  " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].numberOfpuzzles.ToString();
     //   InfoText[3].text = "Style: " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].style;
      //  InfoText[4].text = "Difficulty: " + manager.Jigsaws[jigsawID].jigsawLevelInfo.JigLevels[altID].difficulty.ToString();


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
        InfoText[0].text = manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].name;
        InfoText[1].text = "Width: " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].Xpieces.ToString() + " " + "Height: " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].Ypieces.ToString();
        InfoText[2].text = "Number of puzzles:  " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].numberOfpuzzles.ToString();
     //   InfoText[3].text = "Style: " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].style;
    //    InfoText[4].text = "Difficulty: " + manager.Jigsaws[loadID].jigsawLevelInfo.JigLevels[altID].difficulty.ToString();


        foreach (GameObject obj in altButtons) 
        {
            obj.GetComponent<Image>().color = Color.white;
        }

        altButtons[altID].GetComponent<Image>().color = Color.grey;


    }

    public void Startlevel()
    {


        if (loadID != -1 && thingselected == true)
        {

            manager.playID = loadID;
            manager.altID = altLoadID;
            manager.debug = false;

            SceneManager.LoadScene("JigsawMain", LoadSceneMode.Single);

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
