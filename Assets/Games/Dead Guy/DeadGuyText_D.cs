using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadGuyText_D : MonoBehaviour
{

    public TextMeshProUGUI deadText;

    void Awake()
    {

        deadText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
