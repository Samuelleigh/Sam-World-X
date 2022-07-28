using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovingJigsaw;

namespace MovingJigsaw
{
    public class JigsawPieceScript : MonoBehaviour
    {
        // Start is called before the first frame update

        public int ID;
        public int puzzleID;
        public Vector2Int targetSpace;
        public GameObject child;
        public JigsawGameLogic gm;
        public Rect st;
        public Transform parent;

        public float x;
        public float y;

        private void Awake()
        {
            gm = FindObjectOfType<JigsawGameLogic>();
        }

        public void SetMask(int targetX, int targetY, float cellsizeX, float cellsizeY, int puzzleid, Vector2 rawsize)
        {
            puzzleID = puzzleid;
            targetSpace.y = (targetY);
            targetSpace.x = (targetX);

            child.GetComponent<RectTransform>().sizeDelta = rawsize;

            float xOffset = (rawsize.x / 2 - (cellsizeX * targetSpace.x));
            float yOffset = (-rawsize.y / 2 + (cellsizeY * targetSpace.y));

            float x = xOffset;
            float y = yOffset;

            Vector3 v = new Vector3(x, y, 0);

            child.GetComponent<RectTransform>().anchoredPosition = v;

        }

        public void InstantPutBack()
        {
            gameObject.transform.SetParent(parent);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);


        }

        public void SaveStartTransform()
        {
            parent = gameObject.transform.parent;

            x = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            y = gameObject.GetComponent<RectTransform>().anchoredPosition.y;

        }

        public void UpdatePostionInSave() 
        {
            
        
        }

    }
}