using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // what type of item this is
    public Inventory.items itemType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when player picks up the item
    public void Pickup(Inventory inventory)
    {
        inventory.inventory.Add(itemType);
        Destroy(gameObject);
    }
}
