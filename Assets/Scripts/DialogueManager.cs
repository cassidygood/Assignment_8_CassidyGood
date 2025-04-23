using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    private bool isDialogueActive = false;

    void Start()
    {
        HideDialogue();
    }

    public void StartDialogue()
    {
        if (isDialogueActive) return;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);
        ShowDialogue("Have you tamed any animals yet?");
    }

    void Update()
    {
        if (!isDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ShowDialogue("That's great! Have you named them?");
            EndDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ShowDialogue("Would you like to know how to?");
            EndDialogue();
        }
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        Invoke("HideDialogue", 2f); // Optional delay before hiding
    }

    void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
