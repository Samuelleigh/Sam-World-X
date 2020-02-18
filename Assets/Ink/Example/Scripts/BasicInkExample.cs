using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	
    void Awake () {
		// Remove the default message
		RemoveChildren();
		//StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	public void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();

		string text = "";
		

		// Read all the content until we can't continue any more
		while (story.canContinue) {
			
			// add all lines into one piece of text
			text += story.Continue();
		}

		if (story.currentTags.Count > 0)
		{
			if (story.currentTags[0].StartsWith("-")) { ChangeHangTime(story.currentTags); }

			Debug.Log(story.currentTags[0]);
			ChangeEmote(story.currentTags); 
			
		
		}

		//trim white space
		text = text.Trim();
		// Display the text on screen!
		CreateContentView(text);


		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
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
		else {

			

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
				ChangeHangTime(story.currentTags);

			}
		
		
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		lastpressedbuttonText = choice.text;
		FindObjectOfType<SoundSystem>().PlayRandomSound(RandomSoundNames);
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
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
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (choicePanel.transform, false);

		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;
		

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = choicePanel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (choicePanel.transform.GetChild (i).gameObject);
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

		//the loop for each character update.
		while (dialogQue.Count != 0)
		{
		
			charDelay = 0.02f;

			string next = dialogQue.Dequeue().ToString();
		
			// Debug.Log(next.GetHashCode());
			// if the next character is an emoji then tell the bird emotes script about it. else add it to the text.
			if (next.GetHashCode() < 1000)
			{


				dialogQue.Dequeue();


				//things happen when emojis are placed in text
			}
			else
			{
				stext.text += next;

				//if the next character is a fullstop. increase character delay
				if (stext.text.EndsWith("."))
				{
					charDelay = fullstopDelay;
				}

				if (stext.text.EndsWith(","))
				{
					charDelay = 0.4f;
				}

				//decieds if the mouth should talk or not
				if (charDelay <= 0.7f)
				{
					//mouth should continue moving, or start again
				}
				else
				{
					//mouth should stop moving.
				}

				//
				yield return new WaitForSeconds(charDelay);

				//reset char delay
				charDelay = 0.01f;


			}
		}

		if (endAfterShownText == false) { choicePanel.SetActive(true); }
		else 
		{
			yield return new WaitForSeconds(hangTimeEnd);
			knockGameLogic.ShutDoor();
			endAfterShownText = false;
		}

		
	}

	public void ChangeEmote(List<string> tags)
	{

		if (tags.Count == 0) { emotes.ChangeEmote2(""); }

		foreach (string str in tags)
		{
			if (!str.StartsWith("-"))
			{
				//Debug.Log(str);
				emotes.ChangeEmote2(str);
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

	public EmoteLogic emotes;
	public bool AutoEnd = true;
	public KnockGameLogic knockGameLogic;
	public bool endAfterShownText;
	public float hangTimeEnd = 1.0f;
}
