using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogueManager != null)
        {
            dialogueManager.StartDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && dialogueManager != null)
        {
            dialogueManager.EndDialogue(); // Close the dialogue when player leaves
        }
    }
}
