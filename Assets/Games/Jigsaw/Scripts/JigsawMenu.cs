using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class JigsawMenu : MonoBehaviour
{
    public JigLevelManager manager;
    public GameObject Button;
    public List<GameObject> buts = new List<GameObject>();
    public GameObject contentView;
    public TextMeshProUGUI playbuttontext;

    public int loadID = -1;
    public bool allowsave = false;
    public bool thingselected = false;

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

        if (allowsave)
        {
            //saving stuff
            List<JigsawLevel> tempsave = JigSave.LoadPlayer();
            if (tempsave != null) { manager.Jigsaws = tempsave; }
        }


        int total = 0;
        int majorlevels = 0;
        int altnums = 0;
        string[] letters = new string[] {"","a","b","c","d", "e", "f", "g", "h", "i", "j" };

        for (int i = 0; i < manager.Jigsaws.Count; i++)
        {
            GameObject but;
            int ID = i;

            but = Instantiate(Button, contentView.transform);

            if (manager.Jigsaws[i].alt == false)
            {
                altnums = 0;
                majorlevels++;

                if (manager.Jigsaws[i + 1].alt == true) { altnums++; }


            }
            else 
            {                
                altnums++;
            }

            but.GetComponentInChildren<TextMeshProUGUI>().text = (majorlevels) + letters[altnums] + ". " + manager.Jigsaws[i].Name;
            but.GetComponent<Button>().onClick.AddListener(() => Changelevel(ID, but));
            but.transform.SetAsLastSibling();
            buts.Add(but);
            total++;

            if (manager.Jigsaws[i].completed == true) { but.GetComponent<Image>().color = Color.green; }

        }

        contentView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, total * 100);

       // delegate { Changelevel(i - 1); }
    }

    public void Changelevel(int jigsawID, GameObject obj)
    {

        thingselected = true;
        playbuttontext.text = "Click Here to Start";

      //  Debug.Log(jigsawID);
        loadID = jigsawID;

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

        

        obj.GetComponent<Image>().color = Color.grey;


    }

    public void Startlevel()
    {


        if (loadID != -1 && thingselected == true)
        {

            manager.playID = loadID;
            manager.debug = false;

            SceneManager.LoadScene("JigsawMain", LoadSceneMode.Single);

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
