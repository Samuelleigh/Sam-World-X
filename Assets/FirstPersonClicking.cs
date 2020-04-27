using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;

public class FirstPersonClicking : MonoBehaviour
{
    public Camera cam;
    public Transform playerCamera;
    public WebsiteUI websiteUI;

    //link Object
    private LinkObject linkObject;
    private bool clickToLink;
    private bool safty = true;
    private string linkstring;
    public LockMouse lockMouse;

    //Text Objects
    public TextObject currentTextObject;
    public TextObject tempTextObject;
    public TextMeshProUGUI canvasText;
    public int textCount;
    public string currentText;
    public bool clickToText;


    //Grab Objects


    //Interact Objects

    private void Awake()
    {
        playerCamera = cam.transform;
        websiteUI = FindObjectOfType<WebsiteUI>();
        lockMouse = FindObjectOfType<LockMouse>();
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
            CheckLinkObject(hit);
            CheckTextObject(hit);

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

        //On Click
        if (Input.GetMouseButtonDown(0))
        {
            //Click to link
            if (clickToLink == true && safty == true)
            {
            OpenLinkJSPlugin();
            safty = false;
            lockMouse.LockMouseLook();
            }

            //click on object with Text
            if (tempTextObject != null) 
            {
                AdvanceText();         
            }
        }
           

    }


    public void CheckLinkObject(RaycastHit hit) 
    {

        if (hit.collider.gameObject.GetComponent<LinkObject>() != null)
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


    public void CheckTextObject(RaycastHit hit) 
    {
        if (hit.collider.gameObject.GetComponentInParent<TextObject>() != null)
        {
            tempTextObject = hit.collider.gameObject.GetComponentInParent<TextObject>();
        }
        else 
        {
            tempTextObject = null;
        }

    }


    public void AdvanceText() 
    {
        //If talking to new person
        if (tempTextObject != currentTextObject)
        {
            textCount = 0;
            if (currentTextObject) 
            {
                currentTextObject.canvasText.text = "";
            }
        }
        else 
        {
            textCount++;    
        }

        currentTextObject = tempTextObject;

        if (textCount < currentTextObject.Dialog.Count)
        {
            currentTextObject.canvasText.text = currentTextObject.Dialog[textCount];
        }
        else 
        {

            currentTextObject.canvasText.text = "";
        }

    
    }

    public void OpenLinkJSPlugin()
    {
#if! UNITY_EDITOR
		openWindow(linkstring);
#else 
        Application.OpenURL(linkstring);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);
}
