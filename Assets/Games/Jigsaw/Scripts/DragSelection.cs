using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovingJigsaw;
using UnityEngine.EventSystems;

public class DragSelection : MonoBehaviour,IPointerDownHandler
{

    
    RaycastHit hit;

    public bool dragSelect;
    public bool dragingAPiece = false;

    public JigsawPieceDrag[] jigsawpieces;

    public List<JigsawPieceDrag> selectedPieces;

    public RectTransform selectionRecttransform;

    public Vector3 lastMousepostion;
    public bool mouseMoving;

    public Rect rectyglobal;

    public GameObject canvas;

    public EventSystem events;

    //Collider variables
    //=======================================================//

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    // Start is called before the first frame update
    void Start()
    {
      
        dragSelect = false;
        jigsawpieces = FindObjectsOfType<JigsawPieceDrag>();
        events = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastMousepostion != Input.mousePosition)
        {
            mouseMoving = true;

        }
        else 
        {
            mouseMoving = false;
        }

        lastMousepostion = Input.mousePosition;

        //1. when left mouse button clicked (but not released)
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;          

         
              selectedPieces.Clear();
          
              foreach (JigsawPieceDrag jig in jigsawpieces)
              {
                  jig.SetJigpieceColorToNormal();
              }
  
        }

        //2. while left mouse button held
        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40 && !dragingAPiece )
            {
                dragSelect = true;
            }
         
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0))
        {

            
            if (dragSelect == false) //single select
            {
                
            }
            else //marquee select
            {
                CheckInsideSelectionRect();
                Destroy(selectionBox, 0.02f);

            }//end marquee select

            dragSelect = false;

         

        }

    }

    public void CheckInsideSelectionRect()
    {

        //Debug.Log("Check inside selection rect");

       
            Rect recty = Utils.GetScreenRect(p1, Input.mousePosition);
            selectionRecttransform.pivot = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            selectionRecttransform.sizeDelta = new Vector2(recty.width, recty.height);

            // Vector3[] drag = new Vector3[4];
            // selectionRecttransform.GetWorldCorners(drag);

            foreach (JigsawPieceDrag piece in jigsawpieces)
            {
                piece.GenerateNewRectArea();
                // Debug.Log(piece.GetComponent<RectTransform>().localPosition);                  

                int cornerinsidecount = 0;

                bool[] visable = new bool[4];

                

                for (int i = 0; i < piece.dragArea.Length; i++)
                {


                    visable[i] = piece.CheckIfInContentView(piece.dragArea[i]);
                //  Debug.Log(piece.dragArea[i].x + "      " + selectionRecttransform.pivot.x);

                    if (CheckDragArea(piece.dragArea[i]) && visable[i] )
                    {
                        cornerinsidecount++;
                    }

             

                }

            for (int i = 0; i < visable.Length; i++)
            {
                Debug.Log(visable[i]);
            }

            if (cornerinsidecount > 0)
            {
                piece.groupSelected = true;
            }
            else
            {
                piece.groupSelected = false;
            }

            if (piece.groupSelected)
                {
                    Debug.Log("add piece");
                    selectedPieces.Add(piece);
                    piece.SetJigpieceColorToSelected();
                }
            }      
    }


    public bool CheckDragArea(Vector3 pointtocheck) 
    {

        bool left = false;
        bool up= false; 

        if (p1.x < Input.mousePosition.x) 
        {
            left = true;
        }

        if (p1.x >= Input.mousePosition.x)
        {
            left = false;
        }

        if (p1.y > Input.mousePosition.y)
        {
            up = false;
        }
        if (p1.y <= Input.mousePosition.y)
        {
            up = true;
        }


        if (left & up)
        {
           // Debug.Log("moving left up");

            if (pointtocheck.x <= Mathf.Abs(selectionRecttransform.pivot.x)
                            && pointtocheck.x >= Mathf.Abs(selectionRecttransform.pivot.x - rectyglobal.width)
                            && pointtocheck.y <= Mathf.Abs(selectionRecttransform.pivot.y)
                            && pointtocheck.y >= Mathf.Abs(selectionRecttransform.pivot.y - rectyglobal.height)) { return true; }
            else { return false; }
        }

        if (left & !up) 
        {
         //   Debug.Log("moving left down");
            if (pointtocheck.x <= Mathf.Abs(selectionRecttransform.pivot.x)
                            && pointtocheck.x >= Mathf.Abs(selectionRecttransform.pivot.x - rectyglobal.width)
                          && pointtocheck.y >= Mathf.Abs(selectionRecttransform.pivot.y)
                 && pointtocheck.y <= Mathf.Abs(selectionRecttransform.pivot.y + rectyglobal.height)) { return true; }
            else { return false; }
        }

        if (!left & up)
        {

            Debug.Log("Point to check" + pointtocheck + "   min x,y" + (selectionRecttransform.pivot.x) + "," + (selectionRecttransform.pivot.y) + "   max x,y" + (selectionRecttransform.pivot.x + rectyglobal.width) + " , " + (selectionRecttransform.pivot.y - rectyglobal.height));


            if (pointtocheck.x >= selectionRecttransform.pivot.x
                 && pointtocheck.x <= selectionRecttransform.pivot.x + rectyglobal.width
                 && pointtocheck.y <= selectionRecttransform.pivot.y
                          && pointtocheck.y >= selectionRecttransform.pivot.y - rectyglobal.height
                 ) { return true; }
            else { return false; }
        }

        if (!left & !up)
        {
            //  Debug.Log("moving right down");

            if (pointtocheck.x >= selectionRecttransform.pivot.x)
                Debug.Log("point x is less than pivot");


            if (pointtocheck.x >= selectionRecttransform.pivot.x
                 && pointtocheck.x <= selectionRecttransform.pivot.x + rectyglobal.width
                 && pointtocheck.y >= selectionRecttransform.pivot.y
                 && pointtocheck.y <= selectionRecttransform.pivot.y + rectyglobal.height)
            { return true; }
            else { return false; }
        }

        return false;

    }

    private void OnGUI()
    {

        if (dragSelect == true)
        {
            rectyglobal = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rectyglobal, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rectyglobal, 2, new Color(0.8f, 0.8f, 0.95f));        
        }
    }

       
        private void OnTriggerEnter(Collider other)
        {
            // selected_table.addSelected(other.gameObject);
        }

   // public void OnPointerDown(PointerEventData eventData)
   // {
   //
   //     Debug.Log(eventData.lastPress.name);
   //
   //     if (eventData.lastPress.name == "JigsawPiece(Clone)")
   //     {
   //
   //         Debug.Log("hehe");
   //     }
   //     else 
   //     {
   //         selectedPieces.Clear();
   //
   //         foreach (JigsawPieceDrag jig in jigsawpieces)
   //         {
   //             jig.SetJigpieceColorToNormal();
   //         }
   //
   //     };
   //     
   // }
}
