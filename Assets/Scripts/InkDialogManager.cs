using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class InkDialogManager : MonoBehaviour
{

    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] TextAsset startingText;
    [SerializeField] TextAsset allItemsFoundText;

    // [SerializeField] ItemsFoundTracker itemsFoundTracker;
    [SerializeField] bool showStartingTextOnPlay = false;

    [SerializeField] List<GameObject> choices= new List<GameObject>();

    Story currentStory;
    bool dialogueIsPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // This will start a dialogue immediately when the game starts playing, if you say in the inspector you want it to do that. 
        if (showStartingTextOnPlay)
        {
            EnterDialogueMode (startingText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show the next part of the text when Space is pressed
        if (dialogueIsPlaying && Input.GetKeyDown(KeyCode.Space)){
            ContinueStory();
        }

    }

    void AddThingFound()
    {
        //numThingsFound++;

        if (transform.GetChild(0).GetComponent<ItemsFoundTracker>().keyItemFound){
            EnterDialogueMode(allItemsFoundText);
        }
    }

    /// Call THIS function when you what dialog to happen, and pass it the text asset!
    /// For example from a button click
    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        // Get a story from the text asset
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Get the first line from the text and display it
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        // Wait for a short amount of time
        yield return new WaitForSeconds(0.2f);

        // Do the things that need to happen when dialogue is over
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.SetText("");
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            // Get the next line that should appear in the text
            string nextLine = currentStory.Continue();
            // Handle case where the last line isn't valid: end the dialogue
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // Otherwise, handle the normal case for continuing the story, 
            // setting the text in the text box to the next Line
            else 
            {

                dialogueText.SetText(nextLine);
            }

            // DisplayChoices(); 
            
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }


    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
				// choices is a list of references to the button game objects
        if (currentChoices.Count > choices.Count)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].transform.parent.gameObject.SetActive(true);
            choices[index].GetComponent<TextMeshProUGUI>().SetText(choice.text);
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Count; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

    }

public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        
    }
}
