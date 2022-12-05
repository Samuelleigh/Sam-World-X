using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

namespace MovingJigsaw
{
    public class VideoPathScript : MonoBehaviour
    {
        public JigLevelManager manager;
        public TMP_InputField field;
        public TMP_InputField SecondsField;
        public TMP_InputField minutesField;
        public TMP_InputField hourField;
        public Image tick;


        //detects the pasted file path then corrects it.

        private void Awake()
        {
            manager = FindObjectOfType<JigLevelManager>();
        }

        public void UpdatePath()
        {
            if (field.text.StartsWith("https"))
            {
                tick.color = Color.green;
                manager.customFile = true;

            }
            else
            {


                if (field.text.Length > 4)
                {
                    if (field.text[0] == '"' && field.text[field.text.Length - 1] == '"')
                    {

                        field.text = field.text.Substring(1, field.text.Length - 2);
                    }
                }
                if (field.text.EndsWith("PNG"))
                {
                    field.text = field.text.Substring(0, field.text.LastIndexOf('.'));
                    field.text += ".png";
                    UpdatePath();
                }


                if (field.text == "")
                {
                    manager.customFile = false;
                    tick.color = Color.white;
                }
                else if (File.Exists(field.text))
                {
                    //Path.GetExtension(field.text) == ".png"

                    if (Path.GetExtension(field.text) == ".mp4")
                    {

                        tick.color = Color.green;
                        manager.customFile = true;
                    }
                    else
                    {

                        manager.customFile = false;
                        tick.color = Color.red;
                    }
                }
                else
                {
                    manager.customFile = false;
                    tick.color = Color.red;
                }
            }

            // Debug.Log(field.text);
            manager.path = field.text;



            if (int.Parse(SecondsField.text) >= 60) 
            {
                SecondsField.text = 60.ToString() ;
            }


            if (int.Parse(minutesField.text) >= 60)
            {
                minutesField.text = 60.ToString();
            }



            string timecode = hourField.text + ":" + minutesField.text + ":" + SecondsField.text;
            Debug.Log(timecode);

            manager.startseconds = int.Parse(SecondsField.text);
            manager.startminutes = int.Parse(minutesField.text);
            manager.starthours = int.Parse(hourField.text);

            JigsawMenu s = FindObjectOfType<JigsawMenu>();

            manager.Jigsaws[s.loadID].jigsawLevelActive[s.altLoadID].pathURL = field.text;

            //"C:\Users\sleig\Desktop\New folder (2)\pepsi0000_still08.png"

        }

        public void UpdateTimeStart() 
        {

         
        }

        public void LoadFromSave(string pathname)
        {


            field.text = pathname;
            UpdatePath();


        }


    }
}

   