using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EmoteLogic : MonoBehaviour
{

    public bool AllowEmotes = true;

    public Image Eyes;
    public Image Mouth;

    public Sprite angryEyes;
    public Sprite neutralEyes;
    public Sprite happyMouth;
    public Sprite neutralMouth;
    public Sprite sadMouth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeEmote2(string emote) 
    {

        if (AllowEmotes == true)
        {

            // Debug.Log("ww");
            Debug.Log(emote);

            if (emote == "")
            {
                Mouth.sprite = neutralMouth;
                Eyes.sprite = neutralEyes;


            }

            if (emote == "sad")
            {

                Mouth.sprite = sadMouth;
                Eyes.sprite = neutralEyes;

            }

            if (emote == "annoyed")
            {
                Mouth.sprite = neutralMouth;
                Eyes.sprite = angryEyes;
            }

            if (emote == "angry")
            {
                Mouth.sprite = sadMouth;
                Eyes.sprite = angryEyes;
            }

            if (emote == "happy")
            {
                Mouth.sprite = happyMouth;
                Eyes.sprite = neutralEyes;
            }

            if (emote == "evil")
            {
                Mouth.sprite = happyMouth;
                Eyes.sprite = angryEyes;
            }


            Resize();

        }
    }

    void Resize() 
    {
        Mouth.SetNativeSize();
        Eyes.SetNativeSize();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
