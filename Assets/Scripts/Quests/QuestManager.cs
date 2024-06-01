using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    public Quest GetQuestByID(string questID)
    {
        return quests.Find(q => q.questID == questID);
    }

    public bool IsQuestCompleted(string questID)
    {
        Quest quest = GetQuestByID(questID);
        return quest != null && quest.status == QuestStatus.Completed;
    }

    public void StartQuest(string questID)
    {
        Quest quest = GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.NotStarted)
        {
            quest.status = QuestStatus.InProgress;
            Debug.Log($"Started quest: {quest.questName}");
        }
    }

    public void UpdateFlowerCount(string questID, int count)
    {
        Quest quest = GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            var objective = quest.objectives[0];
            objective.currentAmount = count;

            if (objective.currentAmount >= objective.requiredAmount)
            {
                CompleteObjective(questID, 0);
            }
        }
    }

    public void UpdateEnemyCount(string questID, int objectiveIndex)
    {
        Quest quest = GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            if (objectiveIndex < 0 || objectiveIndex >= quest.objectives.Length)
            {
                Debug.LogError($"Invalid objectiveIndex: {objectiveIndex} for questID: {questID}");
                return;
            }

            var objective = quest.objectives[objectiveIndex];
            objective.currentAmount++;
            Debug.Log($"Enemy defeated: {objective.currentAmount}/{objective.requiredAmount}");

            if (objective.currentAmount >= objective.requiredAmount)
            {
                CompleteObjective(questID, objectiveIndex);
            }
        }
    }

    public void CompleteObjective(string questID, int objectiveIndex)
    {
        Quest quest = GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            if (objectiveIndex < 0 || objectiveIndex >= quest.objectives.Length)
            {
                Debug.LogError($"Invalid objectiveIndex: {objectiveIndex} for questID: {questID}");
                return;
            }

            var objective = quest.objectives[objectiveIndex];
            objective.isCompleted = true;
            CheckQuestCompletion(quest);
        }
    }

    private void CheckQuestCompletion(Quest quest)
    {
        bool allCompleted = true;
        foreach (var objective in quest.objectives)
        {
            if (!objective.isCompleted)
            {
                allCompleted = false;
                break;
            }
        }

        if (allCompleted)
        {
            quest.status = QuestStatus.Completed;
            Debug.Log($"Quest completed: {quest.questName}");
            GiveReward(quest.reward);
        }
    }

    private void GiveReward(Reward reward)
    {
        // Implement reward logic here
        Debug.Log($"Reward given: {reward.experiencePoints} XP");
    }
}
