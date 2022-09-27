using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JigWorldText : MonoBehaviour
{

    public TextMeshProUGUI textObject;
    public string charactersToLoop;
    private int number;
    public float time;


    private void Awake()
    {
        textObject = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CycleCharacters());
        
    }

    public IEnumerator CycleCharacters() 
    {
        while (true) 
        {
            if (number == charactersToLoop.Length) { number = 0; }

            textObject.text = charactersToLoop[number].ToString();
            number++;
            yield return new WaitForSeconds(time);

        }  
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
