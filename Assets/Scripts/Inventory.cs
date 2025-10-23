
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.Examples;
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

    private Dictionary<ItemsEnum, int> _inventory = new Dictionary<ItemsEnum, int>();


    /// <summary>
    /// Adds the item to inventory.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(ItemsEnum item, int quantity)
    {
        inventory.Add(item);

        if (_inventory.ContainsKey(item))
        {
            // Add the item to the dictionary by incrementing the quantity tied to the key
            _inventory[item] += quantity;
        }
        else
        {
            _inventory.Add(item, 1);
        }

        // Debug stuff
        //Dictionary<ItemsEnum, int>.KeyCollection keys = _inventory.Keys;
        //string debugString = "";
        //foreach (ItemsEnum key in keys)
        //{
        //    debugString += ($"{key} : {_inventory[key]} \n");
        //}
        //Debug.Log(debugString);
        OnItemAdded.Invoke(item);
    }

    public bool HasItem(ItemsEnum item, int quantity)
    {
        return _inventory.ContainsKey(item) && _inventory[item] >= quantity;  
    }
}
