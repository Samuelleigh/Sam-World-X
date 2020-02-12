using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBase : MonoBehaviour
{

    public UIMaster LayerController;

    void Awake() 
    {

     LayerController = FindObjectOfType<UIMaster>();
    
    
    }

    void Start() 
    {
    
    

    }


    public void Nextlayer() 
    {
        Debug.Log("ww");
        LayerController.NextLayer();
    }

    public void Previouslayer()
    {
        LayerController.PreviousLayer();
    }

    public void SwitchToLayer(int SwitchTo) 
    {
        LayerController.SwitchLayer(SwitchTo);
    }


    void Update() 
    {
    
    
    
    }


}
