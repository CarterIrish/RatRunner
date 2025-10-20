using UnityEngine;


public class TempGameOver : MonoBehaviour
{
    // Game over condition
    private bool gameOverCond = false;

    [SerializeField]
    private ItemsEnum requiredItem = ItemsEnum.key;

    /// <summary>
    /// Called when [enable].
    /// </summary>
    private void OnEnable()
    {
        // Add listener to item pickup event
        Inventory.OnItemAdded.AddListener(OnItemPickedUp);
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    private void OnDisable()
    {
        // Remove listener
        Inventory.OnItemAdded.RemoveListener(OnItemPickedUp);
    }

    /// <summary>
    /// Called when [item picked up].
    /// </summary>
    /// <param name="item">The item.</param>
    private void OnItemPickedUp(ItemsEnum item)
    {
        // If item is our required 
        if(item == requiredItem)
        {
            gameOverCond = true;
        }
    }

    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="other">The other object.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(gameOverCond)
        {        
            Debug.Log($"Game Over hit {other.gameObject.name}");
            GameManager.Instance.SetPlayerEscaped(true);
            UIManager.Instance.LoadScene("GameOver");
        }
    }
}
