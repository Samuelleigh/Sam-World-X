using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGameMenu : MonoBehaviour
{

    public static BackToGameMenu instance;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

            if (Input.GetKey("s"))
            {

                if (Input.GetKey("a"))
                {
                    if (Input.GetKey("m"))
                    {


                        Debug.Log("wow");
                        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);



                    }




                }



            

        }
    }
}
