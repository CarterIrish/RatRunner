using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] 
    private Camera playerCamera;

    [SerializeField] 
    private float interactRange = 5f;

    [SerializeField] 
    private Inventory playerInventory;

    void Update()
    {
        // Cast ray from camera forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //if the raycast hits an item within the interact range
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            Items item = hit.collider.GetComponent<Items>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                item.Pickup(playerInventory);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (playerCamera == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interactRange);
    }
}