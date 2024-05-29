using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
        public EnemyQuest enemyQuest; // Reference to the enemy quest script


    public void GiveQuest()
    {
        questManager.StartQuest(questID);
        enemyQuest.ActivateEnemy(); // Activate the enemy when the quest is given

    }

    public void OnDialogueOptionSelected()
    {
        GiveQuest();
    }
}
