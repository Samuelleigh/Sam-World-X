using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cafe_GameLogic : MonoBehaviour
{
    public UIMaster ui;
    public CafeManager cafeManager;
    public CafeConvoScriptObject currentConvo;
    public CafeDialogScript ink;
    public TextMeshProUGUI d;

    public List<GameObject> overWorldframes;


    delegate void MyDelegate(object VarIs, string name);
    MyDelegate myDelegate;

    public void Awake()
    {
        ui = FindObjectOfType<UIMaster>();
        cafeManager = FindObjectOfType<CafeManager>();
        ink = FindObjectOfType<CafeDialogScript>();
        myDelegate = UpdateConvoVaribles;
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void LoadConversation(int ID) 
    {
        ink.SetUpConversation();

        currentConvo = cafeManager.conversations[ID];

        ink.story.ResetState();

        foreach (CafeTrackedVaribles var in cafeManager.varibles) 
        {

            if (var.result == goalstate.win) { ink.story.EvaluateFunction("setwin" + var.name); }
            if (var.result == goalstate.loss) { ink.story.EvaluateFunction("setloss" + var.name); }
            
            ink.story.ObserveVariable(var.name, (string varName, object newValue) =>
            {
                myDelegate(newValue, varName);
            });


        }

        ink.story.ChoosePathString(currentConvo.InkLink);
        Debug.Log(currentConvo.InkLink);
        ink.RefreshView();   
        ui.NextLayer();
    
    
    }


    public void BackToMenu() 
    {

        ui.PreviousLayer();

        for (int i = 0; i < overWorldframes.Count; i++) 
        {

            goalstate tempgoal = cafeManager.varibles[i+2].result; 

            overWorldframes[i].GetComponent<CafeOverWorldFrame>().Lightup(tempgoal);
        
        
        }
     

    
    }

    public void UpdateConvoVaribles(object varIs, string name) 
    {

        Debug.Log(varIs.ToString());

        goalstate tempgoal = goalstate.win;

        if (varIs.ToString() == "win") {tempgoal = goalstate.win; }
        if (varIs.ToString() == "loss") {tempgoal = goalstate.loss; }
        if (varIs.ToString() == "N") {tempgoal = goalstate.nothing; }

        List<CafeTrackedVaribles> list = cafeManager.varibles;

        for (int i = 0; i < list.Count; i++) {
            
            if (name == list[i].name) { list[i].result = tempgoal; }
        }
    
    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
