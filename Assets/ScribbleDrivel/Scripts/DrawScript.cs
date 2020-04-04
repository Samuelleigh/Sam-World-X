using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



    public class DrawScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public static DrawScript drawScript;
        
        public GameObject brushPrefab;
        private Image brush;

        public Color brushColor = Color.black;
        [Range(10, 50)]
        public int brushSize = 10;
        [Range(0.1f, 1.0f)]
        public float spacing = 0.25f;

        //used to draw a line from our previous mouse pos to our current
        public Vector3 previousMousePosition = Vector3.zero;
        private bool drawingInProgress = false;
        public bool canDraw = false;

        //Used to position our brush tool
        private GameObject brushHolder;
        //Used to store our drawing history
        private GameObject historyHolder;
        //Used to store our current drawing
        private GameObject dotField;
        //store our draw history
        public List<GameObject> drawHistory = new List<GameObject>();

        public void Awake()
        {
            drawScript = this;
        }
        public void GetDefaultSettings()
        {
            if (!GetComponent<Mask>())
                gameObject.AddComponent<Mask>();
            if (!brushPrefab)
                brushPrefab = Resources.Load("Brush") as GameObject;
            if (!brushPrefab)
                Debug.LogError("Cannot locate Brush prefab. Please assign a brush to the DrawScript, in the Inspector. This may be a simple gameObject containing an Image Component.");
            brushColor = Color.black;
            brushSize = 10;
            spacing = 0.25f;
            previousMousePosition = Vector3.zero;
            canDraw = false;
        }
        public void Start()
        {
            if (!historyHolder)
                historyHolder = NewUIObject("DrawHistory");
            if (!brushHolder)
                brushHolder = NewUIObject("BrushHolder");
            if (!brushPrefab)
                brushPrefab = Resources.Load("Brush") as GameObject;
            if (brushPrefab)
            {
                brush = Instantiate(brushPrefab.gameObject, Input.mousePosition, Quaternion.identity).GetComponent<Image>();
                brush.gameObject.transform.SetParent(brushHolder.transform, false);

                SetBrushSize(brushSize);
                SetBrushColor(brushColor);
            }
            else
                Debug.LogError("Brush is missing. Please assign a brush to the DrawScript, in the Inspector. This may be a simple gameObject containing an Image Component.");
        }
        public GameObject NewUIObject(string name)
        {
            GameObject newObject = new GameObject(name);
            newObject.AddComponent<CanvasRenderer>();
            newObject.transform.SetParent(this.gameObject.transform, false);
            newObject.hideFlags = HideFlags.HideInHierarchy;
            return newObject;
        }
        public void SetBrushSize(int bSize)
        {
            brushSize = bSize;
            Vector2 newBrushSize = new Vector2(brushSize, brushSize);
            brush.rectTransform.sizeDelta = newBrushSize;
        }
        public void SetBrushColor(Color bColor)
        {
            brushColor = bColor;
            brush.color = brushColor;
        }
        public void SetBrushShape(Sprite bSprite)
        {
            brush.sprite = bSprite;
        }
        public void Undo()
        {
            if (drawHistory.Count > 0)
            {
                Destroy(drawHistory[drawHistory.Count - 1].gameObject);
                drawHistory.RemoveAt(drawHistory.Count - 1);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            canDraw = true;
            brush.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            canDraw = false;
            brush.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (canDraw)
            {
                if (brush)
                    brush.transform.position = Input.mousePosition;
                else
                    Debug.LogError("Brush is missing. Please assign a brush to the DrawScript, in the Inspector. This may be a simple gameObject containing an Image Component.");
                if (Input.GetMouseButtonUp(0))
                {
                    brush.gameObject.SetActive(true);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    SetBrushSize(brushSize);

                    //Create history
                    dotField = NewUIObject("drawHist" + drawHistory.Count);
                    dotField.transform.SetParent(historyHolder.transform, false);
                    drawHistory.Add(dotField);

                    previousMousePosition = brush.transform.position;
                    //Draw a dot
                    Draw(brush.transform.position);
                }
                else if (Input.GetMouseButton(0))
                {
                    if(!dotField)
                    {
                        //Create history
                        dotField = NewUIObject("drawHist " + drawHistory.Count);
                        dotField.transform.SetParent(historyHolder.transform, false);
                        drawHistory.Add(dotField);
                    }
                    if (previousMousePosition != brush.transform.position)
                    {
                        if (drawingInProgress == false)
                            DrawDistance(previousMousePosition, brush.transform.position);
                    }
                }
            }
            else
            {
                //Check to see if our cursor if over our drawing board (in case the PointerHandlers miss it)
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
                if (hit)
                {
                    if (hit.transform.gameObject == this.gameObject)
                        canDraw = true;
                }
            }
        }
        private void DrawDistance(Vector3 oldPos, Vector3 newPos)
        {
            drawingInProgress = true;
            float spaceDist = Vector3.Distance(oldPos, newPos);
            float actualSpacing = spacing * brush.rectTransform.rect.height;
            float newSpace = actualSpacing / spaceDist;
            float lerpStep = 0;
            if (spaceDist >= actualSpacing)
            {
                while (lerpStep <= 1)
                {
                    Vector3 newDotPos = Vector3.Lerp(oldPos, newPos, lerpStep);
                    Draw(newDotPos);

                    lerpStep += newSpace;
                }
                previousMousePosition = newPos;
            }
            drawingInProgress = false;
        }
        private void Draw(Vector3 pos)
        {
            GameObject newDot = Instantiate(brush.gameObject) as GameObject;
            newDot.transform.position = pos;
            newDot.SetActive(true);
            newDot.transform.SetParent(dotField.transform, true);
        }
    }

