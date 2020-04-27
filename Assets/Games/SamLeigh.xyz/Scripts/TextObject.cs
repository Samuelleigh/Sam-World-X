using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : MonoBehaviour
{
    public Canvas ObjectCanvas;
    public TextMeshProUGUI canvasText;
    public List<string> Dialog;

    private void Awake()
    {
        ObjectCanvas = gameObject.GetComponentInChildren<Canvas>();
        canvasText = ObjectCanvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

}
