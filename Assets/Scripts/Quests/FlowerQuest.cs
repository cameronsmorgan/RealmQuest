using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerQuest : MonoBehaviour
{
    public QuestManager questManager;
    public string questID;
    public  int flowersCollected = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower"))
        {
            CollectFlower();
            Destroy(other.gameObject); // Destroy the flower object upon collection
        }
    }

    void CollectFlower()
    {
        flowersCollected++;
        questManager.UpdateFlowerCount(questID, flowersCollected);
    }
}
