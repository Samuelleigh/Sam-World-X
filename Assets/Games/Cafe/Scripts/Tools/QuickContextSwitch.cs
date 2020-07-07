
using UnityEngine;
using UnityEditor;

public class QuickContextSwitch : EditorWindow
{


    public UIMaster ui;

    public void Awake()
    {
        ui = FindObjectOfType<UIMaster>();
    }

    [MenuItem("Window/context Switch")]
    public static void ShowWindow() 
    {
        GetWindow<QuickContextSwitch>("Quick Switch");
    }

    private void OnGUI()
    {

        GUILayout.Label("Quick Context Switch");

        if (GUILayout.Button("Main Menu",GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            ui = FindObjectOfType<UIMaster>();
            ui.SwitchLayer(0);

        }

        if (GUILayout.Button("Overworld",GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            ui = FindObjectOfType<UIMaster>();
            ui.SwitchLayer(1);

            

        }

        if (GUILayout.Button("Dialog", GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            ui = FindObjectOfType<UIMaster>();
            ui.SwitchLayer(2);

        }

        if (GUILayout.Button("End", GUILayout.Height(30)))
        {
            Debug.Log("Button Was Pressed");

            ui = FindObjectOfType<UIMaster>();
            ui.SwitchLayer(3);

        }

    }
}

