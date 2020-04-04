using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data_World
{
    public int numOfunlockedLevels;
    public int numOfCompletedLevels;
    public string worldName;
    public int unlockedAt;
    public bool unlocked;
    public bool completed;

    public List<Data_Level> levels;

}
