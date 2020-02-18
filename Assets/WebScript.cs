using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public void OpenWeb(string website)
    {

        Application.OpenURL(website);

    }

}
