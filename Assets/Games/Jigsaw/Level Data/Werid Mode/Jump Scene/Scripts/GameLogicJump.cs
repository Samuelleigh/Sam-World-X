using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicJump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame() 
    {
        Invoke("ResetGameDelay", 3f);
    
    }

    public void ResetGameDelay() 
    {
        SceneManager.LoadScene("Jump Scene");

    }
}
