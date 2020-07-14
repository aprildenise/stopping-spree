using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public CanvasGroup dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public CanvasGroup options;
    public bool displayingText = false;

    private string[] currentDialogue;
    private int currentDialogueIndex;
    private bool showOptions;
    private string currentName;

    public static DialogueManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (!displayingText) return;

        //TODO CHANCE INPUT ACCORDINGLY
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            DisplayText();
        }
    }

    private void DisplayText()
    {
        if (currentDialogueIndex > currentDialogue.Length - 1)
        {
            if (showOptions)
            {

            }
            else
            {
                HideDialogue();
            }
            return;
        }
        dialogueText.SetText(currentDialogue[currentDialogueIndex]);
    }

    public void DisplayOptions()
    {
        dialogueBox.alpha = 1;
        dialogueBox.interactable = true;
    }

    public void HideOptions()
    {
        dialogueBox.alpha = 0;
        dialogueBox.interactable = false;
    }

    public void DisplayDialogue(string[] dialogue, string name, bool showOptionsAtEnd)
    {
        dialogueBox.alpha = 1;
        dialogueBox.interactable = true;
        displayingText = true;
        currentDialogueIndex = -1;
        nameText.SetText(name);
    }

    public void HideDialogue()
    {
        HideOptions();
        dialogueBox.alpha = 0;
        dialogueBox.interactable = false;
        displayingText = false;
    }

}
