using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuest : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public int objectiveIndex;

    private void OnDestroy()
    {
        // Check if the enemy is defeated
        questManager.CompleteObjective(questID, objectiveIndex);
    }
}
