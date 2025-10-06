using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //data we want to save
    public int day;
    public List<ItemsEnum> inventoryData;
    //public List<GameObject> itemsInGame;

    /// <summary>
    /// constructor that sets all the data we want to save to the actual data in game
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="day"></param>
    public GameData(Inventory inventory, int day)
    {
        //itemsInGame = GameObject.FindGameObjectsWithTag("Item");

        day = this.day;

        for (int i = 0; i < inventory.inventory.Count;i++)
        {
            inventoryData.Add(inventory.inventory[i]);
        }
    }
}
