using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //enum to hold our items 
    public enum items { key, suspiciousPowder, thread, cloth, spring };

    //players inventory
    public List<items> inventory = new List<items>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
