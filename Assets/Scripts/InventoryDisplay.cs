using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory; // Assign in Inspector
    [SerializeField] private TMP_Text inventoryText;    // Or Text if using legacy UI

    private void OnEnable()
    {
        // Subscribe to event so UI updates when an item is added
        Inventory.OnItemAdded.AddListener(UpdateDisplay);
        Workbench.OnUpgradeCrafted.AddListener(UpdateFullInventoryText);
        UpdateFullInventoryText();
    }

    private void OnDisable()
    {
        Inventory.OnItemAdded.RemoveListener(UpdateDisplay);
        Workbench.OnUpgradeCrafted.RemoveListener(UpdateFullInventoryText);
    }

    private void UpdateDisplay(ItemsEnum item)
    {
        UpdateFullInventoryText();
    }

    private void UpdateFullInventoryText()
    {
        if (playerInventory == null || inventoryText == null)
            return;

        string display = "Items Holding: ";
        foreach (var kvp in playerInventory.inventoryData)
        {
            display += $"{kvp.Key}: {kvp.Value}\n";
        }

        inventoryText.text = display;
    }
}

