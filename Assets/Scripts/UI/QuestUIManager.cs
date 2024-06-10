using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestUIManager : MonoBehaviour
{

    public GameObject questPanel; // The panel containing quest information
    public Text questTitleText; // Text element for the quest title
    public Text questDescriptionText; // Text element for the quest description
    public Text questObjectiveText; // Text element for the quest objective
    public Text questStatusText; // Text element for the quest status

    private QuestManager questManager;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        questPanel.SetActive(false); // Hide the quest panel initially
    }

    public void ShowQuest(Quest quest)
    {
        questPanel.SetActive(true);
        questTitleText.text = quest.questName;
        questDescriptionText.text = quest.description;
        UpdateQuestObjective(quest);
        questStatusText.text = "Status: " + quest.status.ToString();
    }

    public void HideQuest()
    {
        questPanel.SetActive(false);
    }

    public void UpdateQuestObjective(Quest quest)
    {
        QuestObjective objective = quest.objectives[0];
        questObjectiveText.text = $"{objective.description}: {objective.currentAmount}/{objective.requiredAmount}";
    }

    public void UpdateQuestStatus(Quest quest)
    {
        questStatusText.text = "Status: " + quest.status.ToString();
    }

    public void ToggleQuestPanel()
    {
        if (questPanel != null)
        {
            questPanel.SetActive(!questPanel.activeSelf); // Toggle the active state of the quest panel
        }
    }
}
