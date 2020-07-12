using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDMovement : MonoBehaviour
{

    public int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey("left"))
        {
            print("left key was pressed");
            gameObject.transform.Rotate(Vector3.forward* Time.deltaTime * 100);
        }

        if (Input.GetKey("right"))
        {
            print("right key was pressed");
            gameObject.transform.Rotate(Vector3.back* Time.deltaTime * 100);
        }

        if (Input.GetKey("down"))
        {
            print("down key was pressed");
            gameObject.transform.position += -transform.up * moveSpeed * Time.deltaTime;
          
        
        }

        if (Input.GetKey("up"))
        {
            print("up key was pressed");
            gameObject.transform.position += transform.up * moveSpeed * Time.deltaTime;
        }


    }
}
