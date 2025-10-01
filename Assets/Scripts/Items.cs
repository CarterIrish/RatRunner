using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //reference to inventory
    [SerializeField]
    private Inventory inventory;

    //choose what item this is in the inspector
    public ItemsEnum item;

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
