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
        if (data != null)
        {
            currentDay = data.day;
            inventory.inventory = new List<ItemsEnum>(data.inventoryData);
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
}
