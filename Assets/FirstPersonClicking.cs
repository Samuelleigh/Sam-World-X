using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonClicking : MonoBehaviour
{
    public Camera cam;
    public Transform playerCamera;
    public WebsiteUI websiteUI;

    private LinkObject linkObject;
    private bool clickToLink;
    private bool safty = true;
    private string linkstring;

    private void Awake()
    {
        playerCamera = cam.transform;
        websiteUI = FindObjectOfType<WebsiteUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)) 
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //Did Hit

            if (hit.collider.gameObject.GetComponent<LinkObject>() != null  )
            {             
                linkObject = hit.collider.gameObject.GetComponent<LinkObject>();
                clickToLink = true;
                linkstring = linkObject.linkString;
                websiteUI.ShowLinkText(linkObject.linkMessage);
            }
            else 
            {
                linkObject = null;
                clickToLink = false;
                websiteUI.HideLinkText();
                safty = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.blue);
           // Debug.Log("Did not Hit");
            linkstring = "";
            websiteUI.HideLinkText();
            clickToLink = false;
            safty = true;
        }


        if (Input.GetMouseButtonDown(0) && clickToLink == true && safty == true) 
        {
            Application.OpenURL(linkstring);
            safty = false;
            
        }
           

    }
}
