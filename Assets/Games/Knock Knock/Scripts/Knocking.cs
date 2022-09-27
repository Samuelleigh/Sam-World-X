using UnityEngine;
using UnityEngine.EventSystems;

namespace KnockKnock {
public class Knocking : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{

    public bool open = false;
    public GameObject door;
    public GameObject opendoor;
    public Animator animatior;
    public KnockGameLogic gamelogic;
    public KnockKnockGameLogicMobile gamelogicMobile;

    public UIMaster master;
    public SoundSystem sound;


    void Awake() 
    {

        master = FindObjectOfType<UIMaster>();
        sound = FindObjectOfType<SoundSystem>();
        gamelogic = FindObjectOfType<KnockGameLogic>();
    
    
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
}
