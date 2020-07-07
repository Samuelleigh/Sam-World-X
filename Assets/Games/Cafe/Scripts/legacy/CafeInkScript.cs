using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CafeInkScript : MonoBehaviour
{
	public static event Action<Story> OnCreateStory;

	void Awake()
	{
		// Remove the default message
		RemoveChildren();
		StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory()
	{
		showNextLine = true;
		story = new Story(inkJSONAsset.text);
		if (OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	public void RefreshView()
	{
		// Remove all the UI on screen
		RemoveChoiceChildren();

		string text = "";
		bool newCharacter = true;
		
		text = story.Continue();
		CheckTags(story.currentTags);
	
		text = text.Trim();

		//get character

		if (text[0].ToString() == ":")
		{
			switch (text[1])
			{
				case '0':
					currentCharacter = gamelogic.cafeManager.characters[0];
					break;
				default:
					break;
			}
		}
		else
		{

			newCharacter = false;
		}

		Debug.Log("f");

		CreateContentView(currentCharacter,text,newCharacter);


		// Display all the choices, if there are any!
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
			RefreshView();
		}

	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton(Choice choice)
	{

		lastpressedbuttonText = choice.text;
		FindObjectOfType<SoundSystem>().PlayRandomSound(RandomSoundNames);
		story.ChooseChoiceIndex(choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView(CafeCharacterScriptObject character,string text, bool newcharacter)
	{
		TextMeshProUGUI storyText= Instantiate(textPrefab) as TextMeshProUGUI;
		storyText.transform.SetParent(textPanel.transform, false);
		storyText.text = text;
		
		choicePanel.SetActive(true);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(buttonPrefab) as Button;
		choice.transform.SetParent(choicePanel.transform, false);

		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;


		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	public void RemoveChildren()
	{
		int childCount = choicePanel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i)
		{
			GameObject.Destroy(choicePanel.transform.GetChild(i).gameObject);
		}

		int childCount2 = textPanel.transform.childCount;
		for (int i = childCount2 - 1; i >= 0; --i)
		{
			GameObject.Destroy(textPanel.transform.GetChild(i).gameObject);
		}

	}

	void RemoveChoiceChildren() 
	{

		int childCount = choicePanel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i)
		{
			GameObject.Destroy(choicePanel.transform.GetChild(i).gameObject);
		}

	}


	public void CheckTags(List<string> tags)
	{


		if (tags.Count == 0) {  }
		//	else { Debug.Log(tags[0]); }

		foreach (string str in tags)
		{
			if (str == "overworld") 
			{
				Debug.Log("end of the thing");
				gamelogic.BackToMenu();
			
			}

			if (!str.StartsWith("-"))
			{
			
			}
		}
	}

	public void ChangeHangTime(List<string> tags)
	{
		//Debug.Log(tags[0]);

		if (tags.Count > 0)
		{

			if (tags[0].StartsWith("-"))
			{
				//Debug.Log();
				string temp = tags[0];
				temp = temp.Remove(0, 1);

				float hangtime = float.Parse(temp);

				hangTimeEnd = hangtime;

			}
		}
		else { hangTimeEnd = 1; }



	}

	public GameObject choicePanel;
	public GameObject textPanel;

	[SerializeField]
	private TextAsset inkJSONAsset;
	public Story story;

	[SerializeField]
	private Canvas canvas;

	// UI Prefabs
	[SerializeField]
	private TextMeshProUGUI textPrefab;
	[SerializeField]
	private Button buttonPrefab;

	private Queue<char> dialogQue = new Queue<char>();

	public float charDelay = 0.2f;
	public float fullstopDelay = 0.4f;

	public string lastpressedbuttonText;
	public bool gameover = false;

	public string[] RandomSoundNames;

	public bool AutoEnd = true;
	public bool endAfterShownText;
	public float hangTimeEnd = 1.0f;
	public EndGameLogic endGameLogic;

	public bool alwaysShowPlayerChoice = true;
	private bool showNextLine = false;


	//game Logic
	public Cafe_GameLogic gamelogic;
	public CafeCharacterScriptObject currentCharacter;
}
