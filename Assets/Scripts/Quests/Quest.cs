using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public QuestObjective[] objectives;
    public Reward reward;
    public QuestStatus status;
}

[System.Serializable]
public class QuestObjective
{
    public string description;
    public int currentAmount; // Current amount of collected items
    public int requiredAmount; // Required amount to complete the objective
    public bool isCompleted;
}

[System.Serializable]
public class Reward
{
    public int experiencePoints;
    public Item[] items; // Assuming you have an Item class.
}

public enum QuestStatus
{
    NotStarted,
    InProgress,
    Completed,
    Failed
}
