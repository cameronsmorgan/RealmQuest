using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;

    public void GiveQuest()
    {
        questManager.StartQuest(questID);
    }

    public void OnDialogueOptionSelected()
    {
        GiveQuest();
    }
}
