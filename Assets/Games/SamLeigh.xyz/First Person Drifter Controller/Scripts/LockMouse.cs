// by @torahhorse

using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{

    public MouseLook m1;
    public MouseLook m2;
    public GameObject test;

	void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
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
        if  ( Input.GetKeyDown(KeyCode.Escape) )
        {

            Cursor.lockState = CursorLockMode.None;
            m1.enabled = false;
            m2.enabled = false;
        }
    }

}