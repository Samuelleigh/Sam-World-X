using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    

    public int CurrentLayer = 0;
    public List<GameObject> MasterLayers = new List<GameObject>();
    public List<GameObject> IndivdualFrames = new List<GameObject>();

    

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject current in MasterLayers) 
        {
            current.SetActive(false);    
        }
     
        SwitchLayer(0);             
    }

    public void PreviousLayer() 
    {
        SwitchLayer(CurrentLayer - 1);
    }


    public void NextLayer() 
    {
        SwitchLayer(CurrentLayer + 1);     
    }


    public void SwitchLayer(int newlayer) 
    {
        MasterLayers[CurrentLayer].SetActive(false);
        MasterLayers[newlayer].SetActive(true);

        CurrentLayer = newlayer;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
