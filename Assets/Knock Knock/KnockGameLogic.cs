using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink;

public class KnockGameLogic : MonoBehaviour
{

    public Knocking KnockInputScript;
    public BasicInkExample ink;
    public CharacterManager manager;
    public GameObject DialogChoices;
    public UIMaster ui;

    public Character currentCharacter;
    public string characterName;

    public int[] CharacterQue = new int[10];
    public int PlaceInQue = 0;

    public int[] phaseLength = new int[4];
    public int[] knocks = new int[4];
    public int phase = 0;

    private void Awake()
    {
        KnockInputScript = FindObjectOfType<Knocking>();
        ink = FindObjectOfType<BasicInkExample>();
        manager = FindObjectOfType<CharacterManager>();
        ui = FindObjectOfType<UIMaster>();
    }


    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = manager.CharacterList[CharacterQue[PlaceInQue]];
        ApplyNewCharacter(currentCharacter);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNewCharacter() 
    {
        PlaceInQue++;
        currentCharacter = manager.CharacterList[CharacterQue[PlaceInQue]];
        Debug.Log(currentCharacter.name);
    }

    public void KnockInput() {

        //Discovery Phase
        if (phase == 0)
        {
            if (knocks[0] == 0) { StartCoroutine(KnockTime()); }
               
            knocks[0]++;
       
        }


        //Travel phase
        if (phase == 1)
        {
            knocks[1]++;
        }

        //Check Phase
        if (phase == 2)
        {
            knocks[2]++;
        }

        //Open Phase
        if (phase == 3)
        {
            knocks[3]++;
        }


    }

    public IEnumerator KnockTime()
    {
        Debug.Log("discovery Phase");
        //discovery Stage
        phase = 0;

        yield return new WaitForSeconds(phaseLength[0]);

        //travel phase
        Debug.Log("travel Phase");
        phase = 1;

        yield return new WaitForSeconds(phaseLength[1]);

        //Check phase
        Debug.Log("Check Phase");
        phase = 2;

        yield return new WaitForSeconds(phaseLength[2]);
        Debug.Log("open Phase");

        //openPhase
        phase = 3;
        DialogChoices.SetActive(true);
        OpenDoor();


        yield return null;
    }

    public void ClearKnockPhase()
    {
        phase = 0;

        for (int i = 0; i < knocks.Length; i++) 
        {

            knocks[i] = 0;
        }

    
        

    }

    public void OpenDoor() 
    {

        string knotname;
        string reaction;

        reaction = GetReaction();
        knotname = GetInkPath(reaction);

        Debug.Log(knotname);


        //End of setting an ink knot



        KnockInputScript.door.SetActive(false);
        KnockInputScript.opendoor.SetActive(true);

        DialogChoices.SetActive(true);

        ink.StartStory();
        ink.story.variablesState["reaction"] = reaction;
        ink.story.ChoosePathString(knotname);
        ink.RefreshView();

    }

    //calculates the reaction for 

    public string GetReaction() 
    {
        //if knocks during travel phase is less than 2, don't be annoyed
        if (knocks[1] <= 2)
        {

            Debug.Log(knocks[0]);
            //if equal or less than 3 knocks during discovery
            if (knocks[0] <= 3)
            {
                return "neutral";

            }
            else
            {

                return "expectingfriend";

            }

        }
        else { return "annoyed"; }

        
    
    
    }

    //==========VERY IMPORTANT========determines the ink  path to get, edit this to add more paths

    public string GetInkPath(string reaction) 
    {

        switch (currentCharacter.name) 
        {

            case "normal":
                switch (reaction) 
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }
            case "normal2":
                switch (reaction)
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }
            case "bloodynose":
                switch (reaction)
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }
            case "weridface":
                switch (reaction)
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }
            case "remembersyou":
                switch (reaction)
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }
            case "needsfriend":
                switch (reaction)
                {
                    case "neutral":
                        return currentCharacter.name + "." + reaction;
                    case "annoyed":
                        return currentCharacter.name + "." + reaction;
                    case "expectingfriend":
                        return currentCharacter.name + "." + reaction;
                    default:
                        Debug.Log("switch error");
                        return currentCharacter.name + "." + "neutral";

                }


        }


        return "";

    }

    public void ShutDoor() 
    {

        KnockInputScript.door.SetActive(true);
        KnockInputScript.opendoor.SetActive(false);
        phase = 0;

        if (currentCharacter.name == "needsfriend") { ui.SwitchLayer(2); Debug.Log("End Of Game"); }
        else {

            GetNewCharacter();
            ApplyNewCharacter(currentCharacter);
            DialogChoices.SetActive(false);
            ClearKnockPhase();
        }
        

    }


    public void ApplyNewCharacter(Character charcter)
    {

        phaseLength[0] = charcter.DiscoveryTime;
        phaseLength[1] = charcter.TravelTime;
        phaseLength[2] = charcter.CheckTime;
        characterName = charcter.name;
        ink.charDelay = charcter.timeBeforeSpeaking;

    }


}
