using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LowFiMenu : MonoBehaviour
{

    public LowFiManager manager;
    public GameObject Button;
    public List<GameObject> buts = new List<GameObject>();
    public GameObject contentView;
    public bool thingselected = false;
    


    // Start is called before the first frame update
    void Start()
    {

        int total = 0;

        for (int i = 0; i < manager.levels.Count; i++)
        {
            GameObject but;
            int ID = i;
            but = Instantiate(Button, contentView.transform);

            but.GetComponentInChildren<TextMeshProUGUI>().text = manager.levels[i].levelName;
            but.GetComponent<Button>().onClick.AddListener(() => Changelevel(ID,but));
          //  but.transform.SetAsLastSibling();
         //   buts.Add(but);
          //  total++;

           // if (manager.levels[i].Completed == true) { but.GetComponent<Image>().color = Color.green; }
            
        }

      //  contentView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, total * 100);
    }


    public void Changelevel(int jigsawID, GameObject obj)
    {
        Debug.Log("s");

        thingselected = true;

        obj.GetComponent<Image>().color = Color.grey;


    }

    public void LoadLevel() 
    {
    
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
