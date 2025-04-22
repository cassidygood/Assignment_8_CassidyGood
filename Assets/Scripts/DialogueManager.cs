using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Button yesButton;
    public Button noButton;

    private bool isDialogueActive = false;

    void Start()
    {
        HideDialogue(); // Ensure everything is hidden at start

        // Assign button click listeners
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
    }

    public void StartDialogue()
    {
        if (isDialogueActive) return; // Prevent dialogue from restarting
        isDialogueActive = true;

        dialoguePanel.SetActive(true);
        ShowButtons();
        ShowDialogue("Have you tamed any animals yet?");
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
    }

    void OnYesClicked()
    {
        Debug.Log("Yes clicked!");
        ShowDialogue("That's great! Have you named them?");
        EndDialogue();
    }

    void OnNoClicked()
    {
        Debug.Log("No clicked!");
        ShowDialogue("Would you like to know how to?");
        EndDialogue();
    }

    void ShowButtons()
    {
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    void HideButtons()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        HideButtons();
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        HideButtons();
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
