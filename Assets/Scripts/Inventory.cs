using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum to hold our items 
public enum ItemsEnum { key, suspiciousPowder, thread, cloth, spring };

public class Inventory : MonoBehaviour
{
    //players inventory
    public List<ItemsEnum> inventory;

    //reference to the door
    [SerializeField]
    private GameObject door;

    public EnemyNavigation enemyScript;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ItemsEnum i in inventory)
        {
            if (i == ItemsEnum.key)
            {
                door.SetActive(false);

                enemyScript.target = playerTransform;
            }
        }
    }
}
