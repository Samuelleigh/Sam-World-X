using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBase : MonoBehaviour
{

    public string FrameName;
    public bool CanExit;
    public GameLogic gameLogic;
    public State belongsToState;

    public GameObject exitButton;


    private void Awake()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (CanExit == false) { exitButton.SetActive(false); }
    }

    public void ChangeState(string changeto) { gameLogic.ChangeState(changeto); }

    public void PlayDrum()
    {
        gameLogic.PlayDrum();

    }



    public void ExitFrame()
    {

        if (belongsToState == State.WorldSelect) { gameLogic.ChangeState("MainMenu"); }

        if (belongsToState == State.LevelSelect) { gameLogic.ChangeState("MainMenu"); }

        if (belongsToState == State.Gameplay) { gameLogic.ChangeState("LevelSelect"); }

        if (belongsToState == State.MainMenu && gameLogic.currentState == State.MainMenu) { Debug.Log("quit"); }

        if (belongsToState == State.MainMenu) { gameObject.SetActive(false); }

        if (belongsToState == State.Extra ) { gameObject.SetActive(false); }
    }


    public void HowToPlay()
    {
        Debug.Log("comeOn");
        gameLogic.Howtoplay.SetActive(true);

    }

    public void DeleteSave()
    {
        SaveSystem.SavePlayer(gameLogic.dataManager.backup, true);
        SaveSystem.LoadPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
