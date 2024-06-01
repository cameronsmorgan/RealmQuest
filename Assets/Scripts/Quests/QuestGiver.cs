using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public List<EnemyQuest> enemyQuests; // List of enemy quest scripts


    public void GiveQuest()
    {
        questManager.StartQuest(questID);
        foreach (var enemyQuest in enemyQuests)
        {
            enemyQuest.ActivateEnemies(); // Activate each enemy when the quest is given
        }

    }

    public void OnDialogueOptionSelected()
    {
        GiveQuest();
    }
}
