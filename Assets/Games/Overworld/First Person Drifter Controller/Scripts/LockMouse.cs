// by @torahhorse

using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{

    public MouseLook m1;
    public MouseLook m2;
    public GameObject test;
    public WebsiteUI ui;

	void Start()
	{
        ui = FindObjectOfType<WebsiteUI>();
        Cursor.lockState = CursorLockMode.Locked;
        LockMouseLook();
    }

    void Update()
    {
    	// lock when mouse is clicked
    	if( Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f )
    	{
    		Cursor.lockState = CursorLockMode.Locked;     
            m1.enabled = true;
            m2.enabled = true;
        }
    
    	// unlock when escape is hit
        if  ( Input.GetKeyDown(KeyCode.LeftShift) )
        {

            LockMouseLook();
        }
    }

    public void LockMouseLook() 
    {
        Cursor.lockState = CursorLockMode.None;
       // ui.HideCrosshair();
        //m1.enabled = false;
       // m2.enabled = false;


    }

}