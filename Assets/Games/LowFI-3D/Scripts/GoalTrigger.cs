using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    public SightGameLogic gamelogic;
    public GameObject player;
    public int destoryCountDown = 1;

    private void Awake()
    {
        gamelogic = FindObjectOfType<SightGameLogic>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("hit");

        Destroy(gameObject, destoryCountDown);

        if (collision.gameObject.name == "FirstPerson-AIO" && gamelogic.currentLevel.endStateType == EndStateType.Collectable)
        {
            Debug.Log("hit2");
            
            gamelogic.LoadNextLevel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
