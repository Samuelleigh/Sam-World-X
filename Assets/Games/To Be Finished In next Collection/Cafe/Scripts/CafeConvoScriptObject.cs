using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Cafe Conversation")]
[System.Serializable]
public class CafeConvoScriptObject : ScriptableObject
{
    public string Name;
    public string InkLink;
    public List<CafeCharacterScriptObject> Characters;
    public string objective;
}
