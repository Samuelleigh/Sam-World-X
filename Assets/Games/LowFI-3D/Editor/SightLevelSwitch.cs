using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class SightLevelSwitch : EditorWindow
{
    public UIMaster ui;
    public SightGameLogic sightGameLogic;

    public bool StartFromStart;
    public int SpecifiedStart;

    public string sigh;

    public void Awake()
    {
        ui = FindObjectOfType<UIMaster>();
        sightGameLogic = FindObjectOfType<SightGameLogic>();
     
    }


    [MenuItem("Window/SightLevelSwitch")]
    public static void ShowWindow()
    {
        GetWindow<SightLevelSwitch>("Sight Level Switch");
    }

    private void OnGUI()
    {
        sightGameLogic = FindObjectOfType<SightGameLogic>();
        GUILayout.Label("Sight Level Switch");

        GUILayout.Label(sightGameLogic.manager.currentlevelID.ToString());

        if (GUILayout.Button("Next Level",GUILayout.Height(30)))
        {
            Debug.Log("");

            sightGameLogic = FindObjectOfType<SightGameLogic>();
            sightGameLogic.LoadNextLevel();

        }

        if (GUILayout.Button("Previous Level",GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            sightGameLogic = FindObjectOfType<SightGameLogic>();
            sightGameLogic.LoadPreviousLevel();

        }

        if (GUILayout.Button("First Level", GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            sightGameLogic = FindObjectOfType<SightGameLogic>();

            sightGameLogic.manager.currentlevelID = 0;
            sightGameLogic.LoadLevel(sightGameLogic.manager.levels[sightGameLogic.manager.currentlevelID]);

        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Change Play Mode", GUILayout.Height(30)))
        {

            sightGameLogic = FindObjectOfType<SightGameLogic>();

            if (sightGameLogic.playFromStart == true)
            {
                sigh = "Start from here";
                sightGameLogic.playFromStart = false;
                return;
            }
            else 
            {
                sigh = "Start from beggining";
                sightGameLogic.playFromStart = true;
                return;
            }

        }

        GUILayout.Label(sigh);

    }

#endif

   
}
