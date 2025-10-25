
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

    // Track which number corresponds to which recipe
    private List<CraftingRecipe> _displayedRecipes = new List<CraftingRecipe>();


    /// <summary>
    /// Called when [enable].
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to input and game state events
        InputManager.OnInteractPressed.AddListener(HandleInteract);
        GameManager.OnWorkbenchOpened.AddListener(OpenCraftingMenu);
        GameManager.OnWorkbenchClosed.AddListener(CloseCraftingMenu);
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        InputManager.OnInteractPressed.RemoveListener(HandleInteract);
        GameManager.OnWorkbenchOpened.RemoveListener(OpenCraftingMenu);
        GameManager.OnWorkbenchClosed.RemoveListener(CloseCraftingMenu);
    }

    /// <summary>
    /// Handles the interact - changes game state to crafting.
    /// </summary>
    private void HandleInteract()
    {
        // Only respond if player is in range
        if (_playerInRange)
        {
            GameManager.Instance.ChangeGameState(GameStates.CRAFTING);
        }
    }

    /// <summary>
    /// Update - Listen for number key presses while in crafting menu
    /// </summary>
    private void Update()
    {
        // Only listen for number keys when crafting menu is open
        if (GameManager.Instance?.GameState != GameStates.CRAFTING) return;

        // Check number keys 1-9
        if (Keyboard.current.digit1Key.wasPressedThisFrame) TryCraftRecipe(0);
        else if (Keyboard.current.digit2Key.wasPressedThisFrame) TryCraftRecipe(1);
        else if (Keyboard.current.digit3Key.wasPressedThisFrame) TryCraftRecipe(2);

    }

    /// <summary>
    /// Try to craft recipe at given index
    /// </summary>
    private void TryCraftRecipe(int index)
    {
        if (index >= _displayedRecipes.Count)
        {
            Debug.LogWarning($"No recipe at index {index + 1}");
            return;
        }

        CraftRecipe(_displayedRecipes[index]);
    }



    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="other">The other.</param>
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

    /// <summary>
    /// Called when [trigger exit].
    /// </summary>
    /// <param name="other">The other.</param>
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

    /// <summary>
    /// Shows the interact prompt.
    /// </summary>
    /// <param name="show">if set to <c>true</c> [show].</param>
    private void ShowInteractPrompt(bool show)
    {
        Debug.Log(show ? "Press [E] to Craft" : "");
    }

    /// <summary>
    /// Opens the crafting menu.
    /// </summary>
    private void OpenCraftingMenu()
    {
        if(_currentPlayer == null)
        {
            return;
        }

        // Clear and repopulate recipe list for this menu
        _displayedRecipes.Clear();

        Debug.Log("=== WORKBENCH CRAFTING MENU ===");

        int index = 1;
        foreach(CraftingRecipe recipe in _availableRecipies)
        {
            _displayedRecipes.Add(recipe);

            bool canCraft = recipe.CanCraft(_currentPlayer.Inventory);
            string status = canCraft ? "[AVAILABLE]" : "[LOCKED]";

            Debug.Log($"[{index}] {status} {recipe.name}");

            foreach(CraftingRecipe.ItemRequirement req in recipe._requiredItems)
            {
                int playerOwns = _currentPlayer.Inventory.GetItemCount(req._itemType);
                Debug.Log($"    - {req._itemType}: {playerOwns}/{req._quantity}");
            }

            index++;
        }

        Debug.Log("\nPress 1-3 to craft, ESC to close");
    }

    /// <summary>
    /// Closes the crafting menu.
    /// </summary>
    private void CloseCraftingMenu()
    {
        Debug.Log("Crafting menu closed");
        // Future: Hide UI canvas here
    }

    /// <summary>
    /// Crafts the recipe.
    /// </summary>
    /// <param name="recipe">The recipe.</param>
    private void CraftRecipe(CraftingRecipe recipe)
    {
        if (_currentPlayer == null)
        {
            Debug.LogError("No player reference!");
            return;
        }

        if (recipe.CanCraft(_currentPlayer.Inventory) == false)
        {
            Debug.LogWarning($"Cannot craft {recipe.name} - missing required items!");
            return;
        }

        // Remove required items from inventory
        foreach(CraftingRecipe.ItemRequirement req in recipe._requiredItems)
        {
            _currentPlayer.Inventory.RemoveItem(req._itemType, req._quantity);
        }

        // Add upgrade to player's upgrades dictionary
        if (_currentPlayer.Upgrades.ContainsKey(recipe._upgradeGranted))
        {
            // Increment existing upgrade level
            _currentPlayer.Upgrades[recipe._upgradeGranted]++;
            Debug.Log($"Crafted {recipe.name}! Upgrade level: {_currentPlayer.Upgrades[recipe._upgradeGranted]}");
        }
        else
        {
            // Add new upgrade at level 1
            _currentPlayer.Upgrades.Add(recipe._upgradeGranted, 1);
            Debug.Log($"Crafted {recipe.name}! New upgrade unlocked at level 1");
        }

        // Refresh menu to show updated inventory
        OpenCraftingMenu();
    }

}
