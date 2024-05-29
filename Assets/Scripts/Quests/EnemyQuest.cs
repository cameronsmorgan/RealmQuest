using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuest : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public int objectiveIndex;

    private void Awake()
    {
        // Ensure the enemy starts inactive
        gameObject.SetActive(false);
    }

    public void ActivateEnemy()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // Check if the enemy is defeated
        questManager.CompleteObjective(questID, objectiveIndex);
    }
}
