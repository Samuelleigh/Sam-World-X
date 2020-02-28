using UnityEngine;
using UnityEngine.EventSystems;

public class Knocking : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{

    public bool open = false;
    public GameObject door;
    public GameObject opendoor;
    public Animator animatior;
    public KnockGameLogic gamelogic;

    public UIMaster master;
    public SoundSystem sound;





    void Awake() 
    {

        master = FindObjectOfType<UIMaster>();
        sound = FindObjectOfType<SoundSystem>();
        gamelogic = FindObjectOfType<KnockGameLogic>();
    
    
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void OnPointerClick(PointerEventData eventdata) 
    {
        if (master.CurrentLayer == 0)
        {
            master.SwitchLayer(1);
            sound.PlaySound("Knock");
            gamelogic.KnockInput();
        }

        else
        {
            sound.PlaySound("Knock");
            animatior.SetTrigger("Shake");
            gamelogic.KnockInput();
        }

 
       // Debug.Log("knockl");
    
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
