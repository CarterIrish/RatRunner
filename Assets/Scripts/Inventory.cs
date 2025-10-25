
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//enum to hold our items 
[System.Serializable]
public enum ItemsEnum { key, suspiciousPowder, thread, cloth, spring, cheese, needle };

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// The on item added event
    /// </summary>
    public static UnityEvent<ItemsEnum> OnItemAdded = new UnityEvent<ItemsEnum>();

    //players playerInventory
    //public List<ItemsEnum> playerInventory;

    public Dictionary<ItemsEnum, int> inventoryData { get => inventory; }
    private Dictionary<ItemsEnum, int> inventory = new Dictionary<ItemsEnum, int>();


    /// <summary>
    /// Adds the item to playerInventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(ItemsEnum item, int quantity)
    {
        //playerInventory.Add(item);

        if (inventory.ContainsKey(item))
        {
            // Add the item to the dictionary by incrementing the quantity tied to the key
            inventory[item] += quantity;
        }
        else
        {
            inventory.Add(item, 1);
        }

        // Debug stuff
        Dictionary<ItemsEnum, int>.KeyCollection keys = inventory.Keys;
        string debugString = "";
        foreach (ItemsEnum key in keys)
        {
            debugString += ($"{key} : {inventory[key]} \n");
        }
        Debug.Log(debugString);
        OnItemAdded.Invoke(item);
    }

    public bool HasItem(ItemsEnum item, int quantity)
    {
        return inventory.ContainsKey(item) && inventory[item] >= quantity;  
    }


    // Load data into the playerInventory
    public void LoadData(Dictionary<ItemsEnum, int> data)
    {
        inventory.Clear();
        if (data != null)
        {
            foreach (KeyValuePair<ItemsEnum, int> kvp in data)
            {
                inventory[kvp.Key] = kvp.Value;
            }
        }
    }

    public int GetItemCount(ItemsEnum type)
    {
        int count = 0;
        inventory.TryGetValue(type, out count);

        return count;
    }
}
