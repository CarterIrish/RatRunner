using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //reference to inventory
    [SerializeField]
    private Inventory inventory;

    //variable in the inspector to choose what type of item it is
    public items item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(gameObject);
            inventory.inventory.Add(item);
        }
    }
}
