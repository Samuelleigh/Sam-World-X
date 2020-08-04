using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WDLevel 
{

    public string EventName;
    public GameObject EventScenes;
    public List<Transform> cameraTransform;
    public List<Animator> Animators;
    public List<Sound> Music;

}
