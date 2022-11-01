using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "select";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    [SerializeField]private Transform _selection;

    [SerializeField] private Transform BoxStart;

    // Update is called once per frame
    void Update()
    {

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;

        }


        Ray ray = new Ray(BoxStart.position,Vector3.right);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin,ray.direction * 20,Color.red);


        if (Physics.Raycast(ray, out hit)) 
        {
            
            Transform selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
        
                Renderer selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {

                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }

    }
}
