using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{

    public int ageInYears = 25;
    private float moneyInPocket = 10.20f;
    public char firstIntial = 'S';
    public string fullName = "Samuel Leigh";
    public bool isAwake = true;

    public GameObject testGameObject;
    public Transform testTransform;
    public DeleteSave deleteSave;
    public Image playerImage;


    // Start is called before the first frame update
    void Start()
    {


        ageInYears = 24;
        ageInYears += 5;
        ageInYears -= 25;
        ageInYears *= 4;
        ageInYears++;
        

        fullName = "sam leigh" + " woah";

        isAwake = true;
        isAwake = !isAwake;


        if (ageInYears == 100)
        {
            Debug.Log("happy 100");
        }
        else 
        {
            Debug.Log("cool but your not 100, so who cares");
        }


        if (ageInYears >= 35) 
        {
            Debug.Log("wow");
        }

        if (ageInYears > 35)
        {
            Debug.Log("wow wow");
        }

        if (testGameObject.transform.position.x > 0) 
        {
            Debug.Log("Test");       
        }

        for (int i = 0; i <= 40; i++) 
        {
            Debug.Log(i);
            ageInYears++;       
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
