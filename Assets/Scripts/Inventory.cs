
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//enum to hold our items 
public enum ItemsEnum { key, suspiciousPowder, thread, cloth, spring, cheese };

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// The on item added event
    /// </summary>
    public static UnityEvent<ItemsEnum> OnItemAdded = new UnityEvent<ItemsEnum>();

    //players inventory
    public List<ItemsEnum> inventory;

    /// <summary>
    /// Adds the item to inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(ItemsEnum item)
    {
        inventory.Add(item);
        OnItemAdded.Invoke(item);
    }
}
