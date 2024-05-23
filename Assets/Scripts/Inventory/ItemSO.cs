using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player GameObject not found!");
                return false;
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on Player GameObject!");
                return  false;
            }

            if (playerHealth.health >= playerHealth.maxHealth)
            {
                Debug.Log("Player health is already at maximum. Item not used.");
                return false;
            }

            Debug.Log("Using item: " + itemName);
            playerHealth.ChangeHealth(amountToChangeStat);
        }
        return false;
    }



    public enum StatToChange
    {
        none,
        health,
        mana,
        staminia

    };

    public enum AttributeToChange
    {
        none,
        strength,
        defense,
        intelligence,
        agility

    };
}
