using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VideoPathScript : MonoBehaviour
{
    public JigLevelManager manager;
    public TMP_InputField field;

    private void Awake()
    {
        manager = FindObjectOfType<JigLevelManager>();
    }

    public void UpdatePath()
    {
        Debug.Log(field.text);
        manager.path = field.text;
    }
}
