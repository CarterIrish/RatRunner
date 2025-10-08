using UnityEngine;

public class Item : MonoBehaviour
{
    //choose what item this is in the inspector
    public ItemsEnum item;

    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="collider">The collider.</param>
    void OnTriggerEnter(Collider collider)
    {
        // If collided with pivot
        if (collider.tag == "Player")
        {
            // Get the inventory of pivot who collided
            Inventory inventory = collider.gameObject.GetComponentInChildren<Inventory>();
            // If inventory is not null add item
            if (inventory != null)
            {
                inventory.AddItem(item);
            }

            // Destory the game object when finished
            Destroy(gameObject);
        }
    }
}
