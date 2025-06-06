using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    private QuestUIManager questUIManager;
    private FadeManager fadeManager; // Reference to the FadeManager
    private AudioManager audioManager; // Reference to the AudioManager


    void Start()
    {
        questUIManager = FindObjectOfType<QuestUIManager>();
        fadeManager = FindObjectOfType<FadeManager>(); // Find the FadeManager in the scene
        audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene

    }

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
            questUIManager.ShowQuest(quest); // Show the quest in the UI

            if (quest is DragonQuest)
            {
                audioManager.PlayDragonQuestSoundtrack(); // Play the DragonQuest soundtrack
            }
        }
    }

    public void UpdateFlowerCount(string questID, int count)
    {
        Quest quest = GetQuestByID(questID);
        if (quest != null && quest.status == QuestStatus.InProgress)
        {
            var objective = quest.objectives[0];
            objective.currentAmount = count;
            questUIManager.UpdateQuestObjective(quest);

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
            questUIManager.UpdateQuestObjective(quest);

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
            questUIManager.UpdateQuestStatus(quest); // Update the quest status in the UI

            GiveReward(quest.reward);
            questUIManager.HideQuest(); // Optionally hide the quest UI when completed

            audioManager.PlayQuestCompletionSound(); // Play quest completion sound


            // Check if the completed quest is the DragonQuest
            if (quest is DragonQuest)
            {
                fadeManager.FadeToBlack("PlayerWinScene"); // Trigger the fade to black effect and scene transition
            }
        }
    }

    private void GiveReward(Reward reward)
    {
        // Implement reward logic here
        Debug.Log($"Reward given: {reward.experiencePoints} XP");
    }
}
