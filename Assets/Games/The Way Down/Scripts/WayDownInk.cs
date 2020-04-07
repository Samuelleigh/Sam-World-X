using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WayDownInk : MonoBehaviour
{
	public static event Action<Story> OnCreateStory;

	void Awake()
	{
		// Remove the default message
		RemoveChildren();
		//StartStory();
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
		RemoveChildren();

		string text = "";
		string trash = "";

		if (alwaysShowPlayerChoice) { showNextLine = true; }

		// Read all the content until we can't continue any more
		while (story.canContinue)
		{

			if (showNextLine == true)
			{

				text += story.Continue();
				
				//Debug.Log(text);
				//foreach (string str in story.currentTags) { Debug.Log(str); }


			}
			else
			{

				trash = story.Continue();
				showNextLine = true;
				// Debug.Log(trash);
				//	foreach (string str in story.currentTags) { Debug.Log(str); }

			}


		}


		if (story.currentTags.Count > 0)
		{

			//Debug.Log(story.currentTags[0]);

			//This changes the hangtime before shutting the door
			if (story.currentTags[0].StartsWith("-")) { ChangeVaribles(story.currentTags); }

			//if the current ink Knot is overworld switch to this, corountine controls the hang time.
			if (story.currentTags.Contains("overworld"))
			{				
				Debug.Log("move back to overworld");
				//wayDownLogic.MoveToOverworld();
				goBackWhenTextFinish = true;
				
			}
			if (story.currentTags.Contains("end"))
			{
				Debug.Log("end");
				wayDownLogic.GameOver();
			}
			if (story.currentTags.Contains("m")) 
			{
				int i = story.currentTags.IndexOf("m");
				Debug.Log("new media");
				wayDownLogic.ShowMedia(Convert.ToInt32(story.currentTags[i+1]));		
			
			}
			if (story.currentTags[0] == "h")
			{
				Debug.Log("Hide media");
				wayDownLogic.HideMedia();

			}
			if (story.currentTags.Contains("p")) 
			{
				int i = story.currentTags.IndexOf("p");
				wayDownLogic.CreatePotionButton((Convert.ToInt32(story.currentTags[i+1])));
				
			}

			//in ink, have the corrisponding animator number in the tag after a, and the name of the animation in in the next tag.

			if (story.currentTags.Contains("a")) 
			{
				int tag = story.currentTags.IndexOf("a");

				int animatorID = Convert.ToInt32(story.currentTags[tag + 1]);
				string triggername = story.currentTags[tag + 2];

				wayDownLogic.TriggerAnimation(animatorID, triggername, false);
			
			}



		}


		//trim white space
		text = text.Trim();
		// Display the text on screen!
		CreateContentView(text);


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

				//sam added this, after the buttons have been made
				choicePanel.SetActive(false);

			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else
		{



			if (AutoEnd == false)
			{
				//sam added this, after the buttons have been made
				choicePanel.SetActive(false);
				Button choice = CreateChoiceView("The End");
				gameover = true;
				choice.onClick.AddListener(delegate
				{
					StartStory();
				});
			}
			else
			{

				endAfterShownText = true;
				ChangeEmote(story.currentTags);
				ChangeVaribles(story.currentTags);

			}	

		}



		//showNextLine = false;
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton(Choice choice)
	{
		//Here so that when a potion is uded, it will reload the ink to make sure the correct choices are implemented.
		SaveMomentBeforeState();
		lastchoice = choice;


		showNextLine = false;
		lastpressedbuttonText = choice.text;
		FindObjectOfType<SoundSystem>().PlayRandomSound(RandomSoundNames);
		story.ChooseChoiceIndex(choice.index);
		RefreshView();
	}



	// Creates a textbox showing the the line of text
	void CreateContentView(string text)
	{
		TextMeshProUGUI storyText = Instantiate(textPrefab) as TextMeshProUGUI;
		storyText.text = "";
		storyText.transform.SetParent(textPanel.transform, false);

		dialogQue.Clear();

		foreach (char character in text)
		{
			dialogQue.Enqueue(character);
		}

		StopAllCoroutines();
		StartCoroutine(CharDelay(storyText));

		//storyText.text = text;

	}

	// Creates a button showing the choice text
	Button CreateChoiceView(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(buttonPrefab) as Button;
		choice.transform.SetParent(choicePanel.transform, false);

		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();

		//If a choice text ends in X, make it unresponsive
		if (text.EndsWith("X"))
		{
			choice.interactable = false;
			choice.image.color = Color.gray;
			choiceText.text = text.Substring(0, text.Length - 1);
		}
		else 
		{
			choiceText.text = text;
		}

		//if text says IGNORE then turn off
		if (text == "IGNORE") { choice.gameObject.SetActive(false); }

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;


		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren()
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

	public IEnumerator CharDelay(TextMeshProUGUI stext)
	{
		

		//edit charDelay in inspector to change OPENING pause.

		yield return new WaitForSeconds(charDelay);

		//talking animation trigger
		//emotes.Talk();


		//the loop for each character update.
		while (dialogQue.Count != 0)
		{

			charDelay = 0.02f;

			string next = dialogQue.Dequeue().ToString();

			
				stext.text += next;

			if (instantTextScroll == false)
			{

				//if the next character is a fullstop. increase character delay
				if (stext.text.EndsWith("."))
				{
					//	Debug.Log("Stop Talking");
					charDelay = fullstopDelay;
					//emotes.StopTalking();
				}

				if (stext.text.EndsWith(","))
				{
					charDelay = 0.4f;
				}

				//decieds if the mouth should talk or not
				if (charDelay <= 0.5f)
				{
					//Debug.Log("talking");
					//mouth should continue moving, or start again
					//emotes.Talk();
				}
				else
				{
					//Debug.Log("stoptalking");
					//mouth should stop moving.
					//emotes.StopTalking();

				}


				yield return new WaitForSeconds(charDelay);

				//reset char delay
				charDelay = 0.01f;
			}
			
		
		}

		instantTextScroll = false;

		if (goBackWhenTextFinish == true) 
		{
			Invoke("MoveToOverworldDelay", 2);
			goBackWhenTextFinish = false;

		}

		//This is perhaps a bit messy, but endAftershown text should only be true when the game is about to end
		if (endAfterShownText == false)
		{
		//	Debug.Log("buttons turn on");
			choicePanel.SetActive(true);
			//emotes.StopTalking(); 
		}
		else
		{
			//emotes.StopTalking();
			yield return new WaitForSeconds(hangTimeEnd);

			//what to do when end ink
			if (wayDownLogic) { wayDownLogic.GameOver(); }

			endAfterShownText = false;
		}


	}

	public void BackToOverworldSetUp()
	{
	

	}

	public void ChangeEmote(List<string> tags)
	{


		if (tags.Count == 0) 
		{ //emotes.ChangeEmote2("");
		}
		//	else { Debug.Log(tags[0]); }

		foreach (string str in tags)
		{
			if (!str.StartsWith("-"))
			{
				Debug.Log(str);
				//emotes.ChangeEmote2(str);
			}
		}
	}

	public void ChangeVaribles(List<string> tags)
	{
		Debug.Log(tags[0]);

		if (tags.Count > 0)
		{

			if (tags[0].StartsWith("-"))
			{
				
				string temp = tags[0];
				temp = temp.Remove(0, 1);

				float hangtime = float.Parse(temp);

				hangTimeEnd = hangtime;
				Debug.Log("changed hang time to " + hangTimeEnd);
			}
		}
		else { hangTimeEnd = 1; }



	}

	public void MoveToOverworldDelay() 
	{
		wayDownLogic.MoveToOverworld();
	}

	public void SaveMomentBeforeState() 
	{

		lastState = story.state.ToJson();

	}

	public void ReevaultateInk() 
	{
		Debug.Log("reloaded? " + lastchoice.index);
		story.state.LoadJson(lastState);
		
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

	//public EmoteLogic emotes;
	public bool AutoEnd = true;
	public bool endAfterShownText;
	public float hangTimeEnd = 1.0f;
	public bool goBackWhenTextFinish = false;
	public EndGameLogic endGameLogic;

	public bool alwaysShowPlayerChoice;
	public bool showNextLine = false;
	private bool MoveToOverworld = false;

	public string lastState;
	public Choice lastchoice;

	public bool instantTextScroll = false;


	//game Logic
	//public KnockGameLogic knockGameLogic;
	//public RockConversation rockConversation;

	public WayDownGame wayDownLogic;
	
}
