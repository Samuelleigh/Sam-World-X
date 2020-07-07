using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Cafe Character")]
[System.Serializable]

public class CafeCharacterScriptObject : ScriptableObject
{

    public string CharacterName;
    public string Inkshorthand;
    public Sprite ProfileImage;
    public Color ColourTheme;

}
