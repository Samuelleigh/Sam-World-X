using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculatorInput : MonoBehaviour
{

    //active
    public string completeString;

    //gameobjects
    public TextMeshProUGUI inputfield;

    public GameLogic gamelogic;



    // Start is called before the first frame update
    void Start()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToString(string input)
    {
        if(gamelogic.currentLevel.MaxRange.ToString().Length > completeString.Length)

        completeString += input;
        inputfield.text = completeString;
    }

    public void Clear()
    {
        inputfield.text = "";

    }

}
