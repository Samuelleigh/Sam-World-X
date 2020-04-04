using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum Compare {EvenOdd,MoreLess,MultipleOfX,IsX,ContainsX,BetweenXAndY,Prime,BiggerSmallerThanLastAttempt,SameUnSameRow,SameUnSameColumn,Triedbefore,binary,hasXnumofCharacters,nothing }
public enum Polarity {noPolarity,FirstOption,SecondOption}
public enum GameFrames {submit,numberTracker,calculator,Xvar,Yvar}

[System.Serializable]
public class Data_Level 
{
    [Header("Save Stuff")]
    public bool unlocked;
    public bool completed;

    [Header("flow stuff")]
    public int unlockedAt;

    [Header("Set up")]

    public string levelName;
    public List<GameFrames> FramesUsed = new List<GameFrames> {GameFrames.submit };

    [Header("Rule")]
    public Compare rule;
    public Polarity polarity;

    [Header("Win / Fail Conditions")]

    public int healthInt;
    public int goalInt;

    [Header("Rule Varibles")]

    public bool hasRange = true;
    public float MinRange;
    public float MaxRange;
    public float xValue;
    public float yValue;



}
