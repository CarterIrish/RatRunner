using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    /// <summary>
    /// The required key
    /// </summary>
    [SerializeField]
    private ItemsEnum requiredKey = ItemsEnum.key;

    /// <summary>
    /// Called when [enable].
    /// </summary>
    private void OnEnable()
    {
        // listen for item picked up event
        Inventory.OnItemAdded.AddListener(OnItemPickedUp);
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    private void OnDisable()
    {
        // Remove the listener once required item is collected
        Inventory.OnItemAdded.RemoveListener(OnItemPickedUp);
    }

    /// <summary>
    /// Called when [item picked up].
    /// </summary>
    /// <param name="item">The item.</param>
    private void OnItemPickedUp(ItemsEnum item)
    {
        // If its the required item
        if(item == requiredKey)
        {
            // Unlock the door
            UnlockDoor();
        }
    }

    /// <summary>
    /// Unlocks the door.
    /// </summary>
    private void UnlockDoor()
    {
        // Log the door is unlocked
        Debug.Log($"Door Unlocked {gameObject.name}");
        // Disable this object
        gameObject.SetActive(false);
    }
}
