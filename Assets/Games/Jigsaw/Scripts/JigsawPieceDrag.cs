using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MovingJigsaw
{
    public class JigsawPieceDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {

        public GameObject main;
        public JigsawPieceScript jig;
        public JigsawGameLogic gm;
        public Vector2 offset;

        void Awake()
        {
            gm = FindObjectOfType<JigsawGameLogic>();
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
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

            offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (new Vector2(main.transform.position.x, main.transform.position.y));
            main.transform.SetAsLastSibling();
            //gameObject.transform.SetParent(gm.box.transform);
            jig.transform.SetParent(gm.ui.MasterLayers[1].transform);

            SoundSystem.instance.PlaySound("click");

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (50 < eventData.position.x && eventData.position.x < (Screen.width - 50) && 50 < eventData.position.y && eventData.position.y < (Screen.height - 50))
            {
                main.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SoundSystem.instance.PlaySound("click");
            Debug.Log("end drag");


            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

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
                        gm.CheckWin(jig.puzzleID);
                    }
                    else
                    {
                        gameObject.transform.SetParent(gm.ui.MasterLayers[1].transform);

                    }
                }
                if (raycastResultList[i].gameObject.name == "Viewport")
                {
                    Debug.Log("snap back to content");
                    gameObject.transform.SetParent(gm.box.transform);

                }
                Debug.Log(raycastResultList[i].gameObject.name);
            }
        }

        public void SnapToPiece(GameObject piece)
        {
            gameObject.transform.position = piece.transform.position;
            gameObject.transform.SetParent(piece.gameObject.transform);
        }

    }
}
