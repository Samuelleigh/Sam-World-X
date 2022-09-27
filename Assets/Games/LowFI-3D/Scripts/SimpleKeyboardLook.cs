using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKeyboardLook : MonoBehaviour
{

    public float speed = 5;
    public Camera camera;
    public Vector3 point;

    private void Awake()
    {
        point = transform.position;
        camera = FindObjectOfType<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.position, Vector3.left, 100 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.RotateAround(transform.position, -Vector3.left, 100 * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.RotateAround(transform.position, -Vector3.up, 100 * Time.deltaTime);

    }
}

