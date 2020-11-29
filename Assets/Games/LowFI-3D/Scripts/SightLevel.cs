using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndStateType {Collectable,TimeLimit}

[System.Serializable]
public class SightLevel
{
    public string levelName;
    public bool Completed = false;
    public EndStateType endStateType;
    public int timeTilSwitch;

    [Header("Level Parents")]
    public GameObject LevelSceneParent;
    public GameObject LevelUIParent;
    [Header("Level Players")]
    public GameObject Player1;
    public GameObject Player2;
}
