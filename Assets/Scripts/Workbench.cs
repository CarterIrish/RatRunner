
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Workbench : MonoBehaviour
{
    [Header("Available Recipes")]
    public List<CraftingRecipe> _availableRecipies;

    [Header("Interaction")]
    public float _interactRange = 3.0f;


    [Header("Debug")]
    [SerializeField] private Player _currentPlayer;
    [SerializeField] private bool _playerInRange;


    private void OnEnable()
    {
        // Subscribe to the interact event from InputManager
        InputManager.OnInteractPressed += HandleInteract;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        InputManager.OnInteractPressed -= HandleInteract;
    }

    private void HandleInteract()
    {
        // Only respond if player is in range
        if (_playerInRange)
        {
            OpenCraftingMenu();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
        
        if(other.CompareTag("Player"))
        {
            _currentPlayer = other.gameObject.GetComponent<Player>();
            _playerInRange = true;
            ShowInteractPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other) return;

        if(other.CompareTag("Player"))
        {
            _currentPlayer = null;
            _playerInRange = false;

            ShowInteractPrompt(false);
        }
    }

    private void ShowInteractPrompt(bool show)
    {
        Debug.Log(show ? "Press [E] to Craft" : "");
    }

    private void OpenCraftingMenu()
    {
        if(_currentPlayer == null)
        {
            return;
        }

        Debug.Log("=== WORKBENCH CRAFTING MENU ===");
        foreach(CraftingRecipe recipe in _availableRecipies)
        {
            bool canCraft = recipe.CanCraft(_currentPlayer.Inventory);
            string status = canCraft ? "[AVAILABLE]" : "[LOCKED]";

            Debug.Log($"{status} {recipe.name}");

            foreach(CraftingRecipe.ItemRequirement req in recipe._requiredItems)
            {
                int playerOwns = _currentPlayer.Inventory.GetItemCount(req._itemType);
                Debug.Log($"    - {req._itemType}: {playerOwns}/{req._quantity}");

            }
        }
    }


}
