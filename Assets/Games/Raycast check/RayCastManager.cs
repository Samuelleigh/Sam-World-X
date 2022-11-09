using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{

    public Camera s;

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray(s.transform.position, s.transform.forward);
        Debug.DrawRay(s.transform.position, s.transform.forward * 100);

       

            if (Physics.Raycast(ray, out hit))
            {
            Debug.DrawRay(ray.origin,ray.direction);
            Debug.Log(hit.transform.gameObject);
            
            }
        
        
        

    }
}
