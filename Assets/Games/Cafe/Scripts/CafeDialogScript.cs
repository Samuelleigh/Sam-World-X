using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink;
using Ink.Runtime;
using System;
using TMPro;
using UnityEngine.UI;

public class CafeDialogScript : MonoBehaviour
{

    public GameObject choicePanel;
    public GameObject chatBox;

    [SerializeField]
    private TextAsset inkJSONAsset;
    public Story story;

    [SerializeField]
    private Canvas canvas;

    // UI Prefabs
    [SerializeField]
    private GameObject textPrefab;
    [SerializeField]
    private GameObject narratorTextPrefab;
    [SerializeField]
    private Button buttonPrefab;

    [SerializeField]
    private Button buttonPrefab2;

    public ScrollRect scrollbar;
    public Cafe_GameLogic gamelogic;
    public GameObject backToMenu;

    public bool newCharacter = false;
    public bool reachedEnd = false;
    public CafeCharacterScriptObject currentcharacter;

    public List<GameObject> profilePics;
    public GameObject continuePrompt;

    public string[] RandomSoundNames;

    private GameObject lastCreatedText;
    public CafeCharacterScriptObject blank;



    public static event Action<Story> OnCreateStory;

    void Awake()
    {
        // Remove the default message
        StartStory();
        //chatBox.text = "";
        RemoveChoiceChildren();
    }


    public void StartStory()
    {

        backToMenu.SetActive(false);
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        choicePanel.SetActive(true);
        RefreshView();
    }

    public void RefreshView()
    {
        if (reachedEnd == false)
        {
            string text = "";


            RemoveChoiceChildren();


            //add text to chat view
            if (story.canContinue)
            {

                text = story.Continue();

                if (GetCharacter(text) != currentcharacter)
                {

                    newCharacter = true;
                    currentcharacter = GetCharacter(text);
                }
                else
                {
                    newCharacter = false;
                }

                currentcharacter = GetCharacter(text);
                CreateContentView(text, currentcharacter, newCharacter);

            }

            CheckTags(story.currentTags);

            //add choices if any
            if (story.currentChoices.Count > 0)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            }
            else
            {
                // RefreshView();
            }

        }
    }

    public CafeCharacterScriptObject GetCharacter(string text)
    {
        if (text[0].ToString() == ":")
        {

            foreach (CafeCharacterScriptObject character in gamelogic.currentConvo.Characters) 
            {
                if (text[1].ToString() == character.Inkshorthand) { return character; }            
            }

        }
        else
        {
            newCharacter = false;
            return currentcharacter;
        }

        return currentcharacter;

    }

    public void RemoveChoiceChildren()
    {

        int childCount = choicePanel.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choicePanel.transform.GetChild(i).gameObject);
        }

    }

    public void SetUpConversation()
    {

        reachedEnd = false;
        backToMenu.SetActive(false);
        choicePanel.SetActive(true);

        string temp = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

        foreach (Transform child in chatBox.transform)
        {
            Destroy(child.gameObject);
        }

        CreateContentView(temp,blank, true);
      
    }

     

    Button CreateChoiceView(string text)
    {
        continuePrompt.SetActive(false);

        CafeCharacterScriptObject buttoncharacter;

        if (text[0] == ':')
        {
           buttoncharacter = GetCharacter(text);
           text = text.Remove(0, 3);
        }
        else 
        {
            buttoncharacter = currentcharacter;
        }

        if (buttoncharacter.CharacterName != "narrator")
        {

            text = "<b>" + buttoncharacter.CharacterName + ":</b>\n" + text;

            // Creates the button from a prefab
            Button choice = Instantiate(buttonPrefab) as Button;
            choice.transform.SetParent(choicePanel.transform, false);



            // Gets the text from the button prefab
            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = text;

            //set up profile pic
            choiceText.GetComponentInChildren<Image>().sprite = buttoncharacter.ProfileImage;

            // Make the button expand to fit the text
            HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;

            return choice;
        }
        else 
        {

            Button choice = Instantiate(buttonPrefab2) as Button;
            choice.transform.SetParent(choicePanel.transform, false);

            // Gets the text from the button prefab
            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = text;

            // Make the button expand to fit the text
            HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;

            return choice;

        }
    }


    void CreateContentView(string text, CafeCharacterScriptObject currentCharacter, bool newCharacter)
    {
        //adds text to the chat box component
        if (newCharacter)
        {

            //adds a space at the end of the line for the sake of being more readable
            text = text.Remove(0, 3);

            //If the character is a character
            if (currentcharacter.CharacterName != "narrator")
            {
                if (currentcharacter.CharacterName != "")
                {
                    text = "<b>" + currentcharacter.CharacterName + ":</b>\n" + text;
                }

                lastCreatedText = Instantiate(textPrefab, chatBox.GetComponent<RectTransform>());
                lastCreatedText.GetComponent<TextMeshProUGUI>().text = text;
                lastCreatedText.GetComponentInChildren<Image>().sprite = currentCharacter.ProfileImage;

            }
            else 
            {
                lastCreatedText = Instantiate(narratorTextPrefab, chatBox.GetComponent<RectTransform>());
                lastCreatedText.GetComponent<TextMeshProUGUI>().text = "\n" + "<b>" + text + " \n";
            }
              
        }
        else 
        {

            if (text[0] == ':') 
            {
                text = text.Remove(0, 3);
            }          

            if (lastCreatedText)
            {
                if (currentcharacter.CharacterName != "narrator")
                {
                    lastCreatedText.GetComponent<TextMeshProUGUI>().text += text;
                }
                else
                {

                    lastCreatedText.GetComponent<TextMeshProUGUI>().text += text + " \n";

                }
            }
        }

        scrollbar.verticalNormalizedPosition = 0f;

        StartCoroutine(UpdateScroll());

        // profilePics[0].GetComponent<RectTransform>().
        continuePrompt.SetActive(true);

     } 
    
    public IEnumerator UpdateScroll ()
    {
        yield return null;
        chatBox.GetComponent<VerticalLayoutGroup>().spacing = 5f;
        yield return null;
        scrollbar.verticalNormalizedPosition = 0f;
    }

    void OnClickChoiceButton(Choice choice)
    {

        FindObjectOfType<SoundSystem>().PlayRandomSound(RandomSoundNames);
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
        chatBox.GetComponent<VerticalLayoutGroup>().spacing = 5.1f;
    }

    public void CheckTags(List<string> tags)
    {


        if (tags.Count == 0) { }
        //	else { Debug.Log(tags[0]); }

        foreach (string str in tags)
        {
            if (str == "overworld")
            {
                Debug.Log("end of the thing");
                reachedEnd = true;
                backToMenu.SetActive(true);
                choicePanel.SetActive(false);

            }

            if (!str.StartsWith("-"))
            {

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space") && gamelogic.ui.CurrentLayer == 2) 
        {

            RefreshView();

            chatBox.GetComponent<VerticalLayoutGroup>().spacing = 5.1f;
        }

    }
}


