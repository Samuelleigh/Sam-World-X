using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CafeTextBox : MonoBehaviour
{

    public Image profilepic;
    public GameObject characterName;
    public GameObject content;
    

    public void NewCharacterSetUp(CafeCharacterScriptObject character, string text, bool newcharacter) 
    {
        if (newcharacter)
        {
            profilepic.sprite = character.ProfileImage;
            characterName.GetComponent<TextMeshProUGUI>().text = character.name;
        }
        else { characterName.gameObject.SetActive(false); }

        content.GetComponent<TextMeshProUGUI>().text = text;
    
    }

}
