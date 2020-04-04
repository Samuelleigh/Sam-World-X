using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour
{

    public GameLogic gameLogic;
    public Data_Level currentLevel;

    public enum RuleResponse { Correct, Incorrect, NotApplicable };

    //set ups

    public bool gameIsOn;
    public GameObject parentFrame;

    public GameObject submitFrame;
    public GameObject trackerFrame;
    public GameObject trackerFrameContent;
    public GameObject inputframe;
    public GameObject endScreen;
    public GameObject XvarFrame;
    public GameObject YvarFrame;


    public TrackerFrame tFScript;


    //live varibles

    public int currentGoal;
    public int currenthealth;
    public int CorrectAnswers;
    public int attemptsMade;
    private RuleResponse ruleResponse;
    public List<string> PreviousAttempts;

    public Color GreenMe;
    public Color GreyMe;
    public Color RedMe;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel(Data_Level level)
    {

        currenthealth = 0;
        CorrectAnswers = 0;
        attemptsMade = 0;
        PreviousAttempts = new List<string>();

        currentLevel = level;
        gameIsOn = true;
        endScreen.SetActive(false);
        currentGoal = level.goalInt;

        //turns off all frames

        for (int i = 0; i < parentFrame.transform.childCount; i++)
        {
            parentFrame.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < trackerFrameContent.transform.childCount; i++)
        {
            trackerFrameContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        //if a frame is present first turn it on, then set it up.

        if (currentLevel.FramesUsed.Contains(GameFrames.submit))
        {
            submitFrame.SetActive(true);

        }

        if (currentLevel.FramesUsed.Contains(GameFrames.numberTracker))
        {

            trackerFrame.SetActive(true);

            tFScript = trackerFrame.GetComponent<TrackerFrame>();

            currenthealth = level.healthInt;

            if (currentLevel.hasRange == true)
            {
                tFScript.rangeText.text = level.MinRange + " - " + level.MaxRange;
            }
            else { tFScript.rangeText.text = "no range :)"; Debug.Log("wwowo"); }

            tFScript.goalText.text = currentGoal + " / " + level.goalInt;
            tFScript.healthText.text = currenthealth.ToString();


        }

        if (currentLevel.FramesUsed.Contains(GameFrames.calculator))
        {
            inputframe.SetActive(true);

        }

        if (currentLevel.FramesUsed.Contains(GameFrames.Xvar))
        {
            XvarFrame.SetActive(true);
            XvarFrame.GetComponent<VarWindowScript>().number.text = currentLevel.xValue.ToString();
        }

        if (currentLevel.FramesUsed.Contains(GameFrames.Yvar))
        {
            YvarFrame.SetActive(true);
            YvarFrame.GetComponent<VarWindowScript>().number.text = currentLevel.yValue.ToString();
        }


        RefreshCounters();

    }

    public void RefreshCounters()
    {
        if (currentLevel.hasRange == true)
        { 
        tFScript.rangeText.text = currentLevel.MinRange + " - " + currentLevel.MaxRange;
        }
        tFScript.goalText.text = CorrectAnswers + " / " + currentLevel.goalInt;
        tFScript.healthText.text = currenthealth.ToString();
       

    }



    public void Submit()
    {
        //get varible
        tFScript.scrollView.GetComponent<ScrollRect>().verticalScrollbar.value = 0;

        string submitString = inputframe.GetComponent<CalculatorInput>().completeString;
        inputframe.GetComponent<CalculatorInput>().completeString = "";
        inputframe.GetComponent<CalculatorInput>().Clear();


        
        

        //calculate if right or wrong
        

        ruleResponse = RuleCalculation(submitString, currentLevel.rule, currentLevel.polarity); ;


        //add bar to 


        GameObject newbar = trackerFrameContent.transform.GetChild(attemptsMade).gameObject;
        newbar.SetActive(true);
        newbar.GetComponent<TrackerBar>().TrackerText.text = submitString;

        //Reactions
        if (ruleResponse == RuleResponse.Correct)
        {
            CorrectAnswers++;
            newbar.GetComponent<TrackerBar>().barImage.color = GreenMe;
            PreviousAttempts.Add(submitString);
        }
        if (ruleResponse == RuleResponse.Incorrect)
        {
            currenthealth--;
            newbar.GetComponent<TrackerBar>().barImage.color = RedMe;
            PreviousAttempts.Add(submitString);          
        }
        if (ruleResponse == RuleResponse.NotApplicable)
        {          
            newbar.GetComponent<TrackerBar>().barImage.color = GreyMe;
            PreviousAttempts.Add(submitString);
        }


        attemptsMade++;
        RefreshCounters();
        //  tFScript.verticleScrollBar.GetComponent<Scrollbar>().value = 0;

        //change UI

        ScrollRect rect = tFScript.scrollView.GetComponent<ScrollRect>();


        Canvas.ForceUpdateCanvases();
  
        
        rect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        rect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        rect.verticalNormalizedPosition = 0.001f;


        //Check if won or lost

        if (currenthealth == 0) { EndLevel(false); }

        if (CorrectAnswers == currentGoal) { EndLevel(true); }


       // Debug.Log(submitString);
    }

    public void EndLevel(bool win)
    {
        endScreen.SetActive(true);
        gameLogic.MoveToFront(endScreen);
        gameIsOn = false;

        if (win ==  true)
        {
            endScreen.GetComponent<EndFrame>().MessageText.text = "YOU WIN!!!";
            currentLevel.completed = true;
            gameLogic.currentWorld.numOfCompletedLevels++;

            //go through each level decideing wether it should be unlocked

            for (int i = 0; i < gameLogic.currentWorld.levels.Count; i++)
            {

                if (gameLogic.currentWorld.numOfCompletedLevels >= gameLogic.currentWorld.levels[i].unlockedAt)
                {
                    gameLogic.currentWorld.levels[i].unlocked = true;
                }
        
            }
       
            //Save everything
            gameLogic.dataManager.SaveGame(gameLogic.dataManager.AllWorlds);
            gameLogic.dataManager.LoadGame();


        }

        if (win == false)
        {
            endScreen.GetComponent<EndFrame>().MessageText.text = "YOU LOST";
            
        }

    }



    public RuleResponse RuleCalculation(string submitted, Compare rule, Polarity polarity)
    {

        //Rules that bipass checks go here
        if (rule == Compare.Triedbefore) { return TriedBefore(submitted, polarity); }


        //Universal Checks go below =======================================


        Debug.Log(submitted);
            //converts to the correct format
            if (submitted == "") { if (rule != Compare.nothing) { return RuleResponse.NotApplicable; } else { return RuleResponse.Correct; } }


            float submittedfloat = float.Parse(submitted);
            //  Debug.Log(submittedfloat);


            //Checks said number is in range
            if ((currentLevel.hasRange == true) && (currentLevel.MinRange <= submittedfloat && submittedfloat <= currentLevel.MaxRange) == false)
            {
                return RuleResponse.NotApplicable;
            }

            if (PreviousAttempts.Contains(submitted)) { return RuleResponse.Incorrect; }
            if (gameIsOn == false) { return RuleResponse.Incorrect; }



        //Universal Checks go above =======================================

        //returns the approate logic for World 1


        if (rule == Compare.EvenOdd) { return EvenOdd(submittedfloat, polarity); }
        else if (rule == Compare.MoreLess) { return MoreLess(submittedfloat, polarity, currentLevel.xValue); }
        else if (rule == Compare.nothing) { return RuleResponse.Incorrect; }
        else if (rule == Compare.MultipleOfX) { return MultipleOfX(submittedfloat, polarity, currentLevel.xValue); }
        else if (rule == Compare.IsX) { return IsX(submittedfloat, polarity, currentLevel.xValue); }
        else if (rule == Compare.ContainsX) { return ContainsX(submitted, polarity, currentLevel.xValue); }
        else if (rule == Compare.BetweenXAndY) { return BetweenXAndY(submittedfloat, polarity, currentLevel.xValue, currentLevel.yValue); }
        else if (rule == Compare.Prime) { return Prime(submittedfloat, polarity); }
        else if (rule == Compare.BiggerSmallerThanLastAttempt)
        {

            if (PreviousAttempts.Count == 0) { return RuleResponse.NotApplicable; }
            else
            {
                if (PreviousAttempts[PreviousAttempts.Count - 1] =="") { return RuleResponse.NotApplicable; }
                Debug.Log("the previous attempt was " + float.Parse(PreviousAttempts[PreviousAttempts.Count-1]));
                return MoreLess(submittedfloat, polarity, float.Parse(PreviousAttempts[PreviousAttempts.Count - 1]));
            }
          
        }
        else if (rule == Compare.SameUnSameColumn) { return SameUnsameColumn(submitted, polarity); }
        else if (rule == Compare.SameUnSameRow) { return SameUnsameRow(submitted, polarity); }
        else if (rule == Compare.binary) { return Binary(submitted, polarity); }
        else if (rule == Compare.hasXnumofCharacters) { return HasXNumberOfCharacter(submitted, polarity, currentLevel.xValue); }   
        else { return RuleResponse.Incorrect; }


        //returns the appropriate logic for World 2

    }



    ///NOW ENETERING THE CALCULATION ZONE
    ///

    public RuleResponse EvenOdd(float submittedFloat, Polarity polarity)
    {


        if (polarity == Polarity.FirstOption)
        {

            int evenOdd = 0;

            evenOdd = Convert.ToInt32(polarity);
            Debug.Log(evenOdd);
            Debug.Log(submittedFloat % 2);

            if (submittedFloat % 2 == 0)
            {
                return RuleResponse.Correct;
            }

            else
            {
                return RuleResponse.Incorrect;
            }
        }

        if (polarity == Polarity.SecondOption)
        {

            if (submittedFloat % 2 != 0)
            {
                return RuleResponse.Correct;
            }

            else
            {
                return RuleResponse.Incorrect;
            }


        }



        return RuleResponse.Incorrect;


    }
        

    


    public RuleResponse MoreLess(float submittedFloat, Polarity polarity, float X)
    {

        Debug.Log(X);

        if (submittedFloat == X)
        {
            Debug.Log("hmm");
            return RuleResponse.NotApplicable;

        }

        if (polarity == Polarity.FirstOption)
        {
            if (submittedFloat < X)
            {

                return RuleResponse.Incorrect;

            }
            else if (submittedFloat > X)
            {

                return RuleResponse.Correct;
            }
            }

            if (polarity == Polarity.SecondOption)
        {
            if (submittedFloat < X)
            {

                return RuleResponse.Correct;

            }
            else if (submittedFloat > X)
            {

                return RuleResponse.Incorrect;
            }

        }

        Debug.Log("hmm");
        return RuleResponse.NotApplicable;
    

    }


    public RuleResponse MultipleOfX(float submittedFloat, Polarity polarity, float X)
    {

     


        if (polarity == Polarity.FirstOption)
        {
            if (submittedFloat == 0f) { return RuleResponse.Correct; }
            if (submittedFloat % X == 0) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }
        }


        //is not a multiple of x, 
        if (polarity == Polarity.SecondOption)
        {
            if (submittedFloat == 0f) { return RuleResponse.Incorrect; }
            if (submittedFloat % X == 0) { return RuleResponse.Incorrect; }
            else { return RuleResponse.Correct; }
        }

        return RuleResponse.NotApplicable;

    }

    public RuleResponse ContainsX(string submitted, Polarity polarity, float X)
    {

        string xString = X.ToString();
        char[] XChar = xString.ToCharArray();
        char xcharacter = XChar[0];

        if (X == 0) { xcharacter = '0' ; }

        Debug.Log(xcharacter);
        Debug.Log(submitted);

        if (polarity == Polarity.FirstOption)
        {

            foreach (char character in submitted)
            {
                Debug.Log(character);
                if (character == xcharacter)
                {
                    return RuleResponse.Correct;
                }
                               
            }

            return RuleResponse.Incorrect;

        }

        if (polarity == Polarity.SecondOption)
        {
            foreach (char character in submitted)
            {
                if (character == XChar[0])
                {
                    return RuleResponse.Incorrect;
                }              
            }

            return RuleResponse.Correct;

        }

        return RuleResponse.Incorrect;
    }


    public RuleResponse BetweenXAndY(float submittedFloat, Polarity polarity, float X,float y)
    {

        if (X == submittedFloat || y == submittedFloat) { return RuleResponse.NotApplicable; }

        if (polarity == Polarity.FirstOption)
        {
            if ((X < submittedFloat) && (submittedFloat < y)) { return RuleResponse.Correct; }
            return RuleResponse.Incorrect;
        }


        if (polarity == Polarity.SecondOption)
        {
            if ((X > submittedFloat) || (submittedFloat > y)) { return RuleResponse.Correct; }
            return RuleResponse.Incorrect;
        }

        return RuleResponse.NotApplicable;


    }

    public RuleResponse Prime(float submittedFloat, Polarity polarity)
    {
        int submitInt = (int)submittedFloat;

        List<int> primenumbers = new List<int>{2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79,83,89,97};

        if (polarity == Polarity.FirstOption)
        {
            if (primenumbers.Contains(submitInt)){ return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }
        }

        if (polarity == Polarity.SecondOption)
        {
            if (primenumbers.Contains(submitInt)) { return RuleResponse.Incorrect; }
            else { return RuleResponse.Correct; }

        }

        return RuleResponse.Incorrect;
    }

    public RuleResponse TriedBefore(string submitted, Polarity polarity)
    {
        string s = submitted;

        Debug.Log("gets to tried before");

        if (PreviousAttempts.Count == 0) { return RuleResponse.NotApplicable; }


        if (polarity == Polarity.FirstOption)
        {
            if (PreviousAttempts.Contains(s)) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }

        }

        if (polarity == Polarity.SecondOption)
        {
            if (PreviousAttempts.Contains(s)) { return RuleResponse.Incorrect; }
            else { return RuleResponse.Correct; }
        }

        return RuleResponse.Incorrect;
        


    }

    public RuleResponse IsX(float submittedFloat, Polarity polarity, float x) 
    {

        if (polarity == Polarity.FirstOption)
        {
            if ( x == submittedFloat) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }

        }

        if (polarity == Polarity.SecondOption)
        {
            if (x == submittedFloat) { return RuleResponse.Incorrect; }
            else { return RuleResponse.Correct; }
        }

        return RuleResponse.Incorrect;
    }

    public RuleResponse HasXNumberOfCharacter(string submittedFloat,Polarity polarity,float x)
    {
        if (polarity == Polarity.FirstOption)
        {
            if (submittedFloat.Length == x) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }

        }

        if (polarity == Polarity.SecondOption)
        {
            if (submittedFloat.Length == x) { return RuleResponse.Incorrect; }
            else { return RuleResponse.Correct; }
        }

        return RuleResponse.Incorrect;

    }


    public RuleResponse Binary(string submitted, Polarity polarity)
    {

        char[] Wow = submitted.ToCharArray();

        Debug.Log(Wow[0]);
        Debug.Log(Wow.Length);

        if (polarity == Polarity.FirstOption)
        {

            if (Wow[0] == '0' && Wow.Length > 1) { return RuleResponse.Incorrect; }

            foreach (char character in Wow)
            {
                if (character == '0' || character == '1') { }
                else { return RuleResponse.Incorrect; }

            }
            return RuleResponse.Correct;

        }

        if (polarity == Polarity.SecondOption)
        {

            if (Wow[0] == '0' && Wow.Length > 1) { return RuleResponse.Correct; }

            foreach (char character in Wow)
            {
                if (character == '0' || character == '1') { return RuleResponse.Incorrect; }
            }

            return RuleResponse.Incorrect;
        }
        return RuleResponse.Correct;
    }

    public RuleResponse SameUnsameRow(string submitted, Polarity polarity)
    {
        if (submitted.Length <= 1)
        {
            return RuleResponse.NotApplicable;
        }

        char[] submitarray = submitted.ToCharArray();

            List<int> IntRow = new List<int>() {0,0,0,0 };
             int rowsUsed = 0;

            List<char> row1 = new List<char>() { '1','2', '3' };
        List<char> row2 = new List<char>() { '4', '5', '6' };
        List<char> row3 = new List<char>() { '7', '8', '9' };
        List<char> row4 = new List<char>() { '0' };

        foreach (char sub in submitarray)
        {

            if (row1.Contains(sub)) { IntRow[0]++; Debug.Log("in row 1"); }
            if (row2.Contains(sub)) { IntRow[1]++; }
            if (row3.Contains(sub)) { IntRow[2]++; }
            if (row4.Contains(sub)) { IntRow[3]++; }
        }

        if (polarity == Polarity.FirstOption)
        {
            foreach (int count in IntRow)
            {
                Debug.Log(count);
                if (count >= 1) { rowsUsed++; }
            }

            Debug.Log("rows used" + rowsUsed);

            if (rowsUsed == 1) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }

        }

        if (polarity == Polarity.SecondOption)
        {

            foreach (int count in IntRow)
            {
                if (count > 1) { return RuleResponse.Incorrect; }
            }

            return RuleResponse.Correct; 
          
        }
        return RuleResponse.NotApplicable;
    }


    public RuleResponse SameUnsameColumn(string submitted, Polarity polarity)
    {
        if (submitted.Length <= 1)
        {
            return RuleResponse.NotApplicable;
        }

        char[] submitarray = submitted.ToCharArray();

        List<int> IntColumn = new List<int>() { 0, 0, 0,  };
        int ColumnsUsed = 0;

        List<char> Column1 = new List<char>() { '1', '4', '7' };
        List<char> Column2 = new List<char>() { '2', '5', '8','0' };
        List<char> Column3 = new List<char>() { '3', '6', '9' };

        foreach (char sub in submitarray)
        {

            if (Column1.Contains(sub)) { IntColumn[0]++; }
            if (Column2.Contains(sub)) { IntColumn[1]++; }
            if (Column3.Contains(sub)) { IntColumn[2]++; }

        }

        if (polarity == Polarity.FirstOption)
        {
            foreach (int count in IntColumn)
            {
                if (count >= 1) { ColumnsUsed++; }
            }

            if (ColumnsUsed == 1) { return RuleResponse.Correct; }
            else { return RuleResponse.Incorrect; }

        }

        if (polarity == Polarity.SecondOption)
        {

            foreach (int count in IntColumn)
            {
                if (count > 1) { return RuleResponse.Incorrect; }
            }

            return RuleResponse.Correct;

        }

        return RuleResponse.NotApplicable;



    }


}



