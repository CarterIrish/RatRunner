using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [SerializeField]
    private int currentDay = 1;

    [SerializeField]
    private int maxDays = 3;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private List<Vector3> enemyPositions;

    public GameObject player;

    public List<GameObject> enemies;

    [SerializeField]
    private Inventory inventory;

    private void Awake()
    {
        // make sure there is only one DayManager at a time
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // make sure starting position is set
        if (startPos == Vector3.zero)
        {
            Debug.Log("Insert Starting Position");
        }

        // make sure enemy positions are set
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            if (enemyPositions[i] == Vector3.zero)
            {
                Debug.Log("Not all enemy positions set.");
            }
        }

        // make sure player isn't null
        if (player == null)
        {
            Debug.Log("Missing reference to player");
        }

        // make sure enemy references aren't null
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                Debug.Log("Missing an enemy reference.");
            }
        }

        //load in the correct data into the game
        GameData data = SaveSystem.LoadGameData();

        //if there is a current save load the data
        if (data != null && data.day >= 1)
        {
            //list containing all items
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

            //delete every default item in the scene
            for (int i = items.Length - 1; i >= 0; i--)
            {
                Destroy(items[i]);
            }

            //load correct items based on save
            currentDay = data.day;
            inventory.inventory = new List<ItemsEnum>(data.inventoryData);
            LoadItems(data);
        }
        else
        {
            Debug.Log("No save file found, starting fresh.");
        }

    }

    /// <summary>
    /// progresses to the next day if player has some left, ends game if player out of days
    /// </summary>
    public void NextDay()
    {
        currentDay++;

        if (currentDay > maxDays)
        {
            UIManager.Instance.LoadScene("GameOver");
            SaveSystem.DeleteGameData();
        }

        // bring player back to start
        player.transform.position = startPos;
        player.transform.rotation = Quaternion.identity;

        // bring enemies back to starting positions
        for (int i = 0; i < enemyPositions.Count; i++) 
        {
            enemies[i].transform.position = enemyPositions[i];
        }

        //saves the players inventory and the current day if the player is on a valid day
        if (currentDay <= maxDays)
        {
            SaveSystem.SaveGameData(inventory, currentDay);
        }
    }


    /// <summary>
    /// loads in all of the items that got saved
    /// </summary>
    /// <param name="data"></param>
    private void LoadItems(GameData data)
    {
        //if there is no items to load return nothing
        if (data.itemDictionary == null || data.itemDictionary.Count == 0)
        {
            Debug.Log("No saved items to load.");
            return;
        }

        //loop through every key value pair in the saved dictionary
        foreach (KeyValuePair<string, List<float[]>> entry in data.itemDictionary)
        {
            //create containers to hold the saved data
            string itemName = entry.Key;
            List<float[]> transformations = entry.Value;

            //loads a prefab with the same name in the dictionary key into a game object
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{itemName}");

            //if a prefab does not exist throw an error message and skip it
            if (prefab == null)
            {
                Debug.LogWarning($"No prefab found for item '{itemName}' in Resources/Prefabs/. Skipping...");
                continue;
            }

            //loop through each array in the list of transformations and instantiate the item based on the transformations and prefab
            foreach (float[] transArray in transformations)
            {
                Vector3 position = new Vector3(transArray[0], transArray[1], transArray[2]);
                Quaternion rotation = new Quaternion(transArray[3], transArray[4], transArray[5], transArray[6]);
                Vector3 scale = new Vector3(transArray[7], transArray[8], transArray[9]);
                Instantiate(prefab, position, rotation);
                Instance.transform.localScale = scale;
            }
        }
    }
}
