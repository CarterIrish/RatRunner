using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [SerializeField]
    private int currentDay = 3;

    [SerializeField]
    private int maxDays = 3;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private List<Vector3> enemyPositions;

    public GameObject player;

    public List<GameObject> enemies;

    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI UIText;

    private float timer = 0;

    [SerializeField]
    private CanvasGroup daysScreen;
    private float fadeDuration = 1.0f;

    [SerializeField]
    private AudioSource caughtAudio;

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
            startPos = gameObject.transform.position;
        }

        // make sure enemy positions are set
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            if (enemyPositions[i] == Vector3.zero)
            {
                Debug.Log("Not all enemy positions set.");
            }
        }

        // make sure _currentPlayer isn't null
        if (player == null)
        {
            Debug.Log("Missing reference to _currentPlayer");
        }

        // make sure enemy references aren't null
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                Debug.Log("Missing an enemy reference.");
            }
        }

        //load in the correct loadedData into the game
        GameData loadedData = SaveSystem.LoadGameData();

        //if there is a current save load the loadedData
        if (loadedData != null && loadedData.day >= 1)
        {
            Debug.Log("LoadingData");
            //list containing all items
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

            //delete every default item in the scene
            for (int i = items.Length - 1; i >= 0; i--)
            {
                Destroy(items[i]);
            }

            //load correct items based on save
            currentDay = loadedData.day;
            playerInventory.LoadData(loadedData.GetInventoryDictionary());
            LoadItems(loadedData);
        }
        else
        {
            Debug.Log("No save file found, starting fresh.");
        }

        ChangeDayText();
    }

    private void Start()
    {
        if (daysScreen != null)
            daysScreen.alpha = 0f;
    }

    private void Update()
    {
        // increment timer
        timer += Time.deltaTime;

        // change the position and size of the day text for the first 1.5 seconds 
        // after spawning in or changing days
        //if (timer < 2.0f)
        //{
        //    StartCoroutine(MoveToTarget(1.5f, new Vector3(0, 0, 0)));
        //    StartCoroutine(Scale(1.5f, new Vector3(2, 2, 2)));
        //}
        //else // return to original position and scale
        //{
        //    // get tranform of text object
        //    RectTransform textTransform = text.GetComponent<RectTransform>();
        //
        //    // change text position
        //    textTransform.localPosition = new Vector3(-843, 491, 0);
        //
        //    // change text size
        //    textTransform.localScale = new Vector3(1, 1, 1);
        //}
    }

    public void OnPlayerCaught()
    {
        if (currentDay <= 1) // final day
        {
            GameManager.Instance.SetPlayerEscaped(false);
            UIManager.Instance.LoadScene("GameOver");
            SaveSystem.DeleteGameData();
        }
        else
        {
            StartCoroutine(HandleDayTransition());
        }
    }

    private IEnumerator HandleDayTransition()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Play audio
        if (caughtAudio != null)
            caughtAudio.Play();

        // Update day count
        currentDay--;
        ChangeDayText();

        // Reset _currentPlayer position
        player.transform.position = startPos;
        player.transform.rotation = Quaternion.identity;

        // Reset enemy positions
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            enemies[i].transform.position = enemyPositions[i];
        }

        // Wait for audio to finish or a short delay
        yield return new WaitForSeconds(caughtAudio != null ? caughtAudio.clip.length : 1f);

        // Save progress if applicable
        if (currentDay <= maxDays && currentDay > 0)
            SaveSystem.SaveGameData(playerInventory, currentDay);

        // Fade back to gameplay
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        // Check for game over
        if (currentDay <= 0)
        {
            GameManager.Instance.SetPlayerEscaped(false);
            UIManager.Instance.LoadScene("GameOver");
            SaveSystem.DeleteGameData();
        }
    }

    /// <summary>
    /// handles fading theh screen in and out
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            daysScreen.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }
        daysScreen.alpha = endAlpha;
    }

    /// <summary>
    /// progresses to the next day if _currentPlayer has some left, ends game if _currentPlayer out of days
    /// </summary>
    public void NextDay()
    {
        timer = 0;
        currentDay--;

        if (currentDay == 0)
        {
            GameManager.Instance.SetPlayerEscaped(false);
            UIManager.Instance.LoadScene("GameOver");
            SaveSystem.DeleteGameData();
        }

        // bring _currentPlayer back to start
        player.transform.position = startPos;
        player.transform.rotation = Quaternion.identity;

        // bring enemies back to starting positions
        for (int i = 0; i < enemyPositions.Count; i++) 
        {
            enemies[i].transform.position = enemyPositions[i];
        }

        //saves the players playerInventory and the current day if the _currentPlayer is on a valid day
        if (currentDay <= maxDays)
        {
            SaveSystem.SaveGameData(playerInventory, currentDay);
        }
    }

    /// <summary>
    /// changes the day text to represent the current day
    /// </summary>
    private void ChangeDayText()
    {
        if (text == null)
        {
            Debug.Log("Text Object is Null.");
            return;
        }

        if (currentDay > 1)
        {
            text.text = currentDay + " Days Left.";
            UIText.text = currentDay + " Days Left.";
        }
        else
        {
            text.text = "Final Day.";
            UIText.text = "Final Day.";
        }
        
    }

    /// <summary>
    /// Lerps the day text to the middle of the screen for a visual effect
    /// </summary>
    /// <param name="duration">time effect should last</param>
    /// <param name="targetPos">position text should move to</param>
    /// <returns></returns>
    private IEnumerator MoveToTarget(float duration, Vector3 targetPos)
    {
        RectTransform textTransform = text.GetComponent<RectTransform>();
        Vector3 start = textTransform.localPosition;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            textTransform.localPosition = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }

        textTransform.localPosition = targetPos;
        ChangeDayText();
    }

    /// <summary>
    /// Lerps the day text size bigger to create a visual effect
    /// </summary>
    /// <param name="duration">how long effect should last</param>
    /// <param name="targetScale">how big text should get</param>
    /// <returns></returns>
    private IEnumerator Scale(float duration, Vector3 targetScale)
    {
        RectTransform textTransform = text.GetComponent<RectTransform>();
        Vector3 startScale = textTransform.localScale;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            textTransform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        textTransform.localScale = targetScale;
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
            //create containers to hold the saved loadedData
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
