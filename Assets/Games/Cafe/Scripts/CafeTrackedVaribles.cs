using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum goalstate { nothing,win,loss }


[System.Serializable]
public class CafeTrackedVaribles
{
    public string name;
    public goalstate result;

}
