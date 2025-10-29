using UnityEngine;

public class Item : MonoBehaviour
{
    //choose what item this is in the inspector
    public ItemsEnum item;

    private void Awake()
    {

    }


    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="collider">The collider.</param>
    void OnTriggerEnter(Collider collider)
    {
        // If collided with _currentPlayer
        if (collider.tag == "Player")
        {
            // Get the inventory of _currentPlayer who collided
            Inventory inventory = collider.gameObject.GetComponentInChildren<Inventory>();
            if(inventory == null)
            {
                Debug.LogWarning("Inventory null at Item trigger enter");
            }
            // If playerInventory is not null add item
            if (inventory != null)
            {
                inventory.AddItem(item, 1);
            }

            // Destory the game object when finished
            Destroy(gameObject);
        }
    }
}
