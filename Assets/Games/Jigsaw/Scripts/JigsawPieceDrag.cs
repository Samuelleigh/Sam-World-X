using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MovingJigsaw
{
    public class JigsawPieceDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler
    {
        [SerializeField]
        public Color selectedcolor;

        public GameObject main;
        public JigsawPieceScript jig;
        public JigsawGameLogic gm;
        public Vector2 offset;
        public Vector2 multiSelectOffset;
        public DragSelection dragSelection;
        public RectTransform mytransform;
        public bool groupSelected = false;

        public Vector3[] dragArea = new Vector3[4];

        public RectTransform viewport;
        public RectTransform canvas;

        void Awake()
        {
            gm = FindObjectOfType<JigsawGameLogic>();
            dragSelection = FindObjectOfType<DragSelection>();
            mytransform = GetComponent<RectTransform>();

        }

        public void Start()
        {
            dragArea = new Vector3[4];
            mytransform.GetWorldCorners(dragArea);
            viewport = transform.parent.parent.parent.GetComponent<RectTransform>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("blah blah blah");
            dragSelection.dragingAPiece = true;

        }

        public void OnPointerClick(PointerEventData eventData) // 3
        {
            

            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("left Click");
                jig.InstantPutBack();

            }
            if (Input.GetMouseButtonUp(0))
            {
                jig.transform.SetParent(gm.ui.MasterLayers[1].transform);
                dragSelection.dragingAPiece = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                //jig.transform.SetParent(gm.ui.MasterLayers[1].transform);
                dragSelection.dragingAPiece = true;
            }
        }

  

        public void OnBeginDrag(PointerEventData eventData)
        {

            offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (new Vector2(main.transform.position.x, main.transform.position.y));
            main.transform.SetAsLastSibling();
            //gameObject.transform.SetParent(gm.box.transform);
            jig.transform.SetParent(gm.ui.MasterLayers[1].transform);

            dragSelection.dragingAPiece = true;

            SoundSystem.instance.PlaySound("click");

            if (!dragSelection.selectedPieces.Contains(this)) 
            {
                dragSelection.selectedPieces.Add(this);
            }

            //Make all 

            foreach (JigsawPieceDrag jig in dragSelection.selectedPieces) 
            {
                if (jig != this)
                {
                    jig.transform.SetParent(gameObject.transform.parent,true); //true or false
                    jig.multiSelectOffset = new Vector2(main.transform.position.x, main.transform.position.y) - (new Vector2(jig.main.transform.position.x, jig.main.transform.position.y));
                }
            }


        }

        public void OnDrag(PointerEventData eventData)
        {
            if (50 < eventData.position.x && eventData.position.x < (Screen.width - 50) && 50 < eventData.position.y && eventData.position.y < (Screen.height - 50))
            {
                main.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;

                foreach (JigsawPieceDrag jig in dragSelection.selectedPieces)
                {
                    if (jig != this)
                    {
                        jig.transform.position = new Vector2(main.transform.position.x, main.transform.position.y) - jig.multiSelectOffset;
                    }
                }

            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            SoundSystem.instance.PlaySound("click");
            //Debug.Log("end drag");
            dragSelection.dragingAPiece = false;


            foreach (JigsawPieceDrag jigdragscript in dragSelection.selectedPieces)
            {
                jigdragscript.SetJigpieceColorToNormal();

                //generate matrix
                jigdragscript.GenerateNewRectArea();
                int pointsovercanvas = 0;

                foreach(Vector3 point in jigdragscript.dragArea ) 
                {
                    if (CheckifOverRectTransform(point,viewport)) 
                    {
                        //if any point in over the overlay then set parenbt to content view
                        Debug.Log("Set Selection to this thing lol");
                        jigdragscript.transform.SetParent(gm.box.transform);
                    }

                    if (!CheckifOverRectTransform(point, dragSelection.canvas.GetComponent<RectTransform>())) 
                    {
                        pointsovercanvas++;                  
                    }

                }

                jigdragscript.CheckIfOverJigPiece();

                //  Debug.Log(pointsovercanvas);
                if (pointsovercanvas >= 3) 
                {
                    //Debug.Log("outside canvas");
                    jigdragscript.jig.InstantPutBack();
                }

            }

          //  CheckIfOverJigPiece();

            dragSelection.selectedPieces.Clear();



        //  PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        //  pointerEventData.position = Input.mousePosition;
        //
        //  List<RaycastResult> raycastResultList = new List<RaycastResult>();
        //  EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        //
        //  if (dragSelection.selectedPieces.Count == 0) {
        //      for (int i = 0; i < raycastResultList.Count; i++)
        //      {
        //          // Debug.Log(raycastResultList[i]);
        //          if (raycastResultList[i].gameObject.name == "SnapPiece(Clone)")
        //          {
        //              if (raycastResultList[i].gameObject.transform.childCount == 0)
        //              {
        //                  SnapToPiece(raycastResultList[i].gameObject);
        //
        //                  //if not on the last level
        //                  if (gm.Level.name != "Just A Dream (The End)")
        //                  {
        //                      gm.CheckWin(jig.puzzleID);
        //                  }
        //              }
        //              else
        //              {
        //                  gameObject.transform.SetParent(gm.ui.MasterLayers[1].transform);
        //
        //              }
        //          }
        //          if (raycastResultList[i].gameObject.name == "Viewport")
        //          {
        //              Debug.Log("snap back to content");
        //              gameObject.transform.SetParent(gm.box.transform);
        //
        //          }
        //          // Debug.Log(raycastResultList[i].gameObject.name);
        //      } }
        //
        //  jig.UpdatePostionInSave();
        //
            if (gm.Level.name == "Just A Dream (The End)")
            {
              //  Debug.Log("werid");
                gameObject.SetActive(false);
                gm.EndingStepForward();
            }
        }

        public void SnapToPiece(GameObject piece)
        {
            gameObject.transform.position = piece.transform.position;
            gameObject.transform.SetParent(piece.gameObject.transform);

           
        }

        public void GenerateNewRectArea()
        {

            mytransform.GetWorldCorners(dragArea);
        }

        public bool CheckIfInContentView(Vector3 point) 
        {

            if (transform.parent.parent.parent == viewport)
            {
              //  Debug.Log("piece is inside viewport");

                Vector3[] tempmatrix = new Vector3[4];
                viewport.GetWorldCorners(tempmatrix);


                Rect tempRect = Utils.GetScreenRect(tempmatrix[0], tempmatrix[2]);
               // Debug.Log("min viewport: " + tempmatrix[0] + "  max viewport: " + tempmatrix[2]);
               // Debug.Log("point: " + point);

                if (point.x >= tempmatrix[0].x
                         && point.x <= tempmatrix[2].x
                         && point.y >= tempmatrix[0].y
                         && point.y <= tempmatrix[2].y)
                {
                   // Debug.Log("return true on point");
                    return true; 
                }              
                else 
                {
                    return false;
                }                

            }
            else 
            {
                return true;
            }
        }

        public bool CheckifOverRectTransform(Vector3 point,RectTransform overtransform) 
        {
            Vector3[] tempmatrix = new Vector3[4];
            overtransform.GetWorldCorners(tempmatrix);


            Rect tempRect = Utils.GetScreenRect(tempmatrix[0], tempmatrix[2]);
            // Debug.Log("min viewport: " + tempmatrix[0] + "  max viewport: " + tempmatrix[2]);
            // Debug.Log("point: " + point);

            if (point.x >= tempmatrix[0].x
                     && point.x <= tempmatrix[2].x
                     && point.y >= tempmatrix[0].y
                     && point.y <= tempmatrix[2].y)
            {
                // Debug.Log("return true on point");
                return true;
            }
            else
            {
                return false;
            }

        }


        public void CheckIfOverJigPiece()
        {

            Debug.Log("CheckIfOverJigPiece");

            Vector3 center = ((dragArea[2] - dragArea[0]) / 2) + dragArea[0];

            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = center;

            List<RaycastResult> raycastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

            for (int i = 0; i < raycastResultList.Count; i++)
            {
                // Debug.Log(raycastResultList[i]);
                if (raycastResultList[i].gameObject.name == "SnapPiece(Clone)")
                {
                    if (raycastResultList[i].gameObject.transform.childCount == 0)
                    {
                        SnapToPiece(raycastResultList[i].gameObject);

                        //if not on the last level
                        if (gm.Level.name != "Just A Dream (The End)")
                        {
                            gm.CheckWin(jig.puzzleID);
                        }
                    }
                    else
                    {
                        gameObject.transform.SetParent(gm.ui.MasterLayers[1].transform);

                    }
                }


            }
        }


        

        public void SetJigpieceColorToSelected() 
        {
            jig.child.GetComponent<RawImage>().color = selectedcolor;

        }

        public void SetJigpieceColorToNormal()
        {
            jig.child.GetComponent<RawImage>().color = Color.white;

        }

    }
}
