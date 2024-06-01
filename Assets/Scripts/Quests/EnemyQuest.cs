using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuest : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public int objectiveIndex;
    public List<GameObject> enemies; // List of enemies

    private int enemiesDefeatedCount = 0;

    private void Awake()
    {
        // Ensure the enemy starts inactive
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void ActivateEnemies()
    {
        // Activate all enemies when the quest is given
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.questID = questID;
                enemyScript.questManager = questManager;
                enemyScript.objectiveIndex = objectiveIndex;
                enemyScript.OnEnemyDefeated += HandleEnemyDefeated;

            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from enemy events
        foreach (var enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.OnEnemyDefeated -= HandleEnemyDefeated;
            }
        }
    }

    private void HandleEnemyDefeated()
    {
        enemiesDefeatedCount++;
        Debug.Log($"Enemy defeated: {enemiesDefeatedCount}/{enemies.Count}");

        Quest quest = questManager.GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            var objective = quest.objectives[objectiveIndex];
            if (enemiesDefeatedCount >= objective.requiredAmount)
            {
                CompleteQuest();
            }
        }
    }

    private void CompleteQuest()
    {
        // Mark the objective as completed
        questManager.CompleteObjective(questID, objectiveIndex);
    }
}
