using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveFrame : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public GameObject main;
    public Vector2 offset;


    public void Awake()
    {
    
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("move");
        //   Debug.Log(eventData.pointerDrag);
        offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (new Vector2(main.transform.position.x, main.transform.position.y));
        //  Debug.Log(offset);
        main.transform.SetAsLastSibling();
        //   gamelogic.MoveToFront(main);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (50 < eventData.position.x && eventData.position.x < (Screen.width - 50) && 50 < eventData.position.y && eventData.position.y < (Screen.height - 50))
        {
          //  Debug.Log("drag");
            //Debug.Log(eventData.position.y);
            main.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {



    }



    // Update is called once per frame
    void Update()
    {

    }


}