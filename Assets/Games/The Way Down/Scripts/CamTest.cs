using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mediacamera;
    public GameObject prefab;

    void Start()
    {
        
    }

    public void Camtest() 
    {
        mediacamera.position = gameObject.transform.position;
        mediacamera.rotation = gameObject.transform.rotation;
        mediacamera.parent = gameObject.transform;
    
    }
    public void NewCam()
    {
        Instantiate(prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
