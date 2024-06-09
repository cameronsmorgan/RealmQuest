using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    public Text interactionText; // Assign the Text UI element in the Inspector


    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.enabled = false; // Hide the interaction message at the start
        }
        else
        {
            Debug.LogError("Interaction Text is not assigned in the Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideMessage(); // Hide the interaction message when the player exits the trigger
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                HideMessage(); // Hide the interaction message when the conversation starts

                ConversationManager.Instance.StartConversation(myConversation);
            }
        }
    }

    public void ShowMessage()
    {
        if (interactionText != null)
        {
            interactionText.enabled = true;
        }
    }

    public void HideMessage()
    {
        if (interactionText != null)
        {
            interactionText.enabled = false;
        }
    }

}
