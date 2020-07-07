using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CafeOverWorldFrame : MonoBehaviour
{

    public string varname;
    public TextMeshProUGUI text;

    public Image Red;
    public Image Green;

    public Color redOff;
    public Color redOn;
    public Color greenOff;
    public Color greenOn;

    public void Lightup(goalstate g) 
    {

        text.text = varname;

        Debug.Log(gameObject.name);
        Debug.Log(g);

        if (g == goalstate.nothing)
        {
            Red.color = redOff;
            Green.color = greenOff;

        }


        if (g == goalstate.win)
        {
            Red.color = redOff;
            Green.color = greenOn;
        }


        if (g == goalstate.loss)
        {
            Red.color = redOn;
            Green.color = greenOff;
        }




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
