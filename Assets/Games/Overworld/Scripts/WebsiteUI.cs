using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WebsiteUI : MonoBehaviour
{
    public TextMeshProUGUI linkText;
    public GameObject crosshair;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLinkText(string linktext) 
    {
        linkText.gameObject.SetActive(true);
        linkText.text = linktext;

    }

    public void HideLinkText() 
    {

        linkText.gameObject.SetActive(false);
    
    }

    public void HideCrosshair() 
    {
        crosshair.SetActive(false);
    
    }

    public void ShowCrosshair()
    {
        crosshair.SetActive(true);

    }


}
