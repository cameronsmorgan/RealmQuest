using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    public void StartQuest(string questID)
    {
        Quest quest = quests.Find(q => q.questID == questID);
        if (quest != null && quest.status == QuestStatus.NotStarted)
        {
            quest.status = QuestStatus.InProgress;
            Debug.Log($"Started quest: {quest.questName}");
        }
    }

    public void CompleteObjective(string questID, int objectiveIndex)
    {
        Quest quest = quests.Find(q => q.questID == questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            quest.objectives[objectiveIndex].isCompleted = true;
            Debug.Log($"Completed objective: {quest.objectives[objectiveIndex].description}");
            CheckQuestCompletion(quest);
        }
    }

    private void CheckQuestCompletion(Quest quest)
    {
        foreach (var objective in quest.objectives)
        {
            if (!objective.isCompleted)
                return;
        }
        quest.status = QuestStatus.Completed;
        Debug.Log($"Quest completed: {quest.questName}");
        GiveReward(quest.reward);
    }

    private void GiveReward(Reward reward)
    {
        // Implement reward logic here.
        Debug.Log($"Reward given: {reward.experiencePoints} XP");
    }
}
