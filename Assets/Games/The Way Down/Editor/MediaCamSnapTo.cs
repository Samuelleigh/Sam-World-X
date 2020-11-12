using UnityEngine;
using UnityEditor;
using TheWayDown;

[CustomEditor(typeof(CamTest))]
public class MediaCamSnapTo : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CamTest snap = (CamTest)target;
        GUILayout.Space(20);

        if (GUILayout.Button("Set Media Camera To this transform")) 
        {
            snap.Camtest();
                   
        }

        GUILayout.Space(20);

        if (GUILayout.Button("New camera"))
        {
            snap.NewCam();

        }

    }
}
