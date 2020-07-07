using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EmoteLogic : MonoBehaviour
{

    public bool AllowEmotes = true;
    public EmotionManager emotionManager;
    public Animator ani;

    public Image LeftEye;
    public Image RightEye;
    public Image Mouth;

    public Emotion currentEmotion;
    public EmotionType currentEmotionType;


    private void Awake()
    {
        emotionManager = FindObjectOfType<EmotionManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    public void ChangeEmote2(string emote) 
    {
        //Debug.Log("changeemote2");

        if (AllowEmotes == true)
        {

            if (emote == "") 
            {
                currentEmotion = emotionManager.emotion[0];
                
               
            }

            //get correct emotion from list
            foreach (Emotion e in emotionManager.emotion) 
            {
                //Debug.Log(e.EmotionType.ToString() + "   " + emote);

                if (e.EmotionType.ToString() == emote) 
                {
                    Debug.Log("match");
                    currentEmotion = e;                          
                }                      
            }


            //apply
            Mouth.sprite = currentEmotion.Mouth;
            LeftEye.sprite = currentEmotion.LeftEye;
            RightEye.sprite = currentEmotion.RightEye;
            currentEmotionType = currentEmotion.EmotionType;

            //Change animator emotion varible
            switch (currentEmotion.EmotionType) 
            {
                case EmotionType.neutral:          
                    ani.SetInteger("Emotion", 0);
                    break;
                case EmotionType.angry:
                    ani.SetInteger("Emotion", 1);
                        break;
                case EmotionType.annoyed:
                    ani.SetInteger("Emotion", 2);
                    break;
                case EmotionType.concerned:
                    ani.SetInteger("Emotion", 3);
                    break;
                case EmotionType.happy:
                    ani.SetInteger("Emotion", 4);
                    break;
                case EmotionType.sad:
                    ani.SetInteger("Emotion", 5);
                    break;


            }



            Resize();

        }
    }

    void Resize() 
    {
        Mouth.SetNativeSize();
        LeftEye.SetNativeSize();
        RightEye.SetNativeSize();
    }


    public void Talk() 
    {
        if (AllowEmotes == true)
        {
            // Debug.Log("www");
            ani.SetBool("Talking", true);
        }
    }


    public void StopTalking() 
    {
        if (AllowEmotes == true)
        {
            ani.SetBool("Talking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}


