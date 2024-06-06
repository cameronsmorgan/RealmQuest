using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public List<EnemyQuest> enemyQuests; // List of enemy quest scripts
    public GameObject replacementNPC; // Reference to the replacement NPC GameObject
    public float destroyDelay = 5f; // Time delay before activation of the replacement NPC

    void Start()
    {
        // Ensure the replacement NPC starts inactive
        if (replacementNPC != null)
        {
            replacementNPC.SetActive(false);
            Debug.Log("Replacement NPC is set to inactive.");
        }
        else
        {
            Debug.LogError("Replacement NPC is not assigned in the Inspector.");
        }
    }

    public void GiveQuest()
    {
        Debug.Log("Quest given.");
        questManager.StartQuest(questID);
        foreach (var enemyQuest in enemyQuests)
        {
            enemyQuest.ActivateEnemies(); // Activate each enemy when the quest is given
        }

        CouroutineRunner.Instance.StartDelayedAction(ReplaceNPC());
    }

    public void OnDialogueOptionSelected()
    {
        GiveQuest();
    }

    private IEnumerator ReplaceNPC()
    {
        Debug.Log("ReplaceNPC coroutine started.");

        // Wait for the specified delay
        Debug.Log("Waiting for " + destroyDelay + " seconds.");
        yield return new WaitForSeconds(destroyDelay);

        Debug.Log("Delay finished.");

        // Activate the replacement NPC
        if (replacementNPC != null)
        {
            replacementNPC.SetActive(true);
            Debug.Log("Replacement NPC activated.");
        }
        else
        {
            Debug.LogError("Replacement NPC is not assigned.");
        }

        // Destroy the current NPC
        Destroy(gameObject);
        Debug.Log("Original NPC destroyed.");
    }
}
