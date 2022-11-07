using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orienation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
     //  Cursor.lockState = CursorLockMode.Locked;
     // Cursor.visible = false;

    }

    // Update is called once per frame
    private void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * sensY;
        Debug.Log(mouseX);

        yRotation += mouseX;
        xRotation -= mouseY;

       // xRotation = Mathf.Clamp(xRotation,-90f,90);

        Debug.Log(mouseX);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
       // orienation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
