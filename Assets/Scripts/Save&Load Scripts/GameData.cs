
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SerializableInventory
{
    public List<ItemsEnum> keys = new List<ItemsEnum>();
    public List<int> values = new List<int>();

    public void FromDictionary(Dictionary<ItemsEnum, int> dict)
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<ItemsEnum, int> kvp in dict)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public Dictionary<ItemsEnum, int> ToDictionary()
    {
        Dictionary<ItemsEnum, int> dict = new Dictionary<ItemsEnum, int>();
        for(int i = 0; i<keys.Count;i++)
        {
            dict[keys[i]] = values[i];
        }
        return dict;
    }

}




[System.Serializable]
public class GameData
{
    //data we want to save
    public int day;
    public SerializableInventory inventoryData = new SerializableInventory();
    public Dictionary<string, List<float[]>> itemDictionary;

    /// <summary>
    /// constructor that sets all the data we want to save to the actual data in game
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="day"></param>
    public GameData(Inventory inventory, int day)
    {
        itemDictionary = new Dictionary<string, List<float[]>>();

        //fill the dictionary with all the games current items
        CollectAllItems();

        this.day = day;

        
        if (inventory != null)
        {
            inventoryData.FromDictionary(inventory.inventoryData);
        }
    }

    public Dictionary<ItemsEnum, int> GetInventoryDictionary()
    {
        return inventoryData.ToDictionary();
    }


    /// <summary>
    /// finds all items in the game and saves their names and positions
    /// </summary>
    public void CollectAllItems()
    {
        itemDictionary.Clear();

        //list containing all items
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        //loop through the items and add its transformations into the array and save it into the dictionary
        foreach (GameObject item in items)
        {
            float[] transformArray = new float[10]
            {
                item.transform.position.x, item.transform.position.y, item.transform.position.z,
                item.transform.rotation.x, item.transform.rotation.y, item.transform.rotation.z, item.transform.rotation.w,
                item.transform.localScale.x, item.transform.localScale.y, item.transform.localScale.z
            };

            // If this item name hasn't been added yet, initialize its list
            if (!itemDictionary.ContainsKey(item.name))
            {
                itemDictionary[item.name] = new List<float[]>();
            }

            // Add this item's transformations to the list
            itemDictionary[item.name].Add(transformArray);
        }
    }
}
