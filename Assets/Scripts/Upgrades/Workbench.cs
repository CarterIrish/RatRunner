
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Workbench : MonoBehaviour
{
    [Header("Available Recipes")]
    public List<CraftingRecipe> _availableRecipies;

    [Header("Interaction")]
    public float _interactRange = 3.0f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI eToCraft;
    [SerializeField] private Button _mobilityButton;
    [SerializeField] private Button _sightButton;
    [SerializeField] private Button _vigorButton;
    [SerializeField] private TextMeshProUGUI _mobilityLevelText;
    [SerializeField] private TextMeshProUGUI _sightLevelText;
    [SerializeField] private TextMeshProUGUI _vigorLevelText;

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

    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        InputManager.OnInteractPressed.RemoveListener(HandleInteract);
        GameManager.OnWorkbenchOpened.RemoveListener(OpenCraftingMenu);

    }


    private void HandleInteract()
    {
        // Only respond if player is in range
        if (_playerInRange)
        {
            GameManager.Instance.ChangeGameState(GameStates.CRAFTING);
        }
    }


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
    public void TryCraftRecipe(int index)
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
        eToCraft.text = "E to craft";
    }

    /// <summary>
    /// Hides the interact prompt.
    /// </summary>
    /// <param name="show">if set to <c>true</c> [show].</param>
    private void HideInteractPrompt(bool show)
    {
        eToCraft.text = "";
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

        // Update UI to reflect current upgrade levels and button states
        UpdateUpgradeUI();
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
            // Still update UI to ensure button states are correct
            UpdateUpgradeUI();
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

        // Apply all upgrades to player (re-applies all to account for new level)
        if (UpgradeManager.Instance != null)
        {
            UpgradeManager.Instance.ApplyAllUpgrades(_currentPlayer);
        }

        // Refresh menu to show updated inventory
        UpdateUpgradeUI();
    }

    /// <summary>
    /// Crafts the Mobility upgrade (called by UI button).
    /// </summary>
    public void CraftMobility()
    {
        TryCraftUpgradeByType(UpgradesEnum.Mobility);
    }

    /// <summary>
    /// Crafts the Vision/Sight upgrade (called by UI button).
    /// </summary>
    public void CraftSight()
    {
        TryCraftUpgradeByType(UpgradesEnum.Vision);
    }

    /// <summary>
    /// Crafts the Vigor upgrade (called by UI button).
    /// </summary>
    public void CraftVigor()
    {
        TryCraftUpgradeByType(UpgradesEnum.Vigor);
    }

    /// <summary>
    /// Try to craft an upgrade by its type (called by UI buttons).
    /// </summary>
    /// <param name="upgradeType">The type of upgrade to craft.</param>
    public void TryCraftUpgradeByType(UpgradesEnum upgradeType)
    {
        if (_currentPlayer == null)
        {
            Debug.LogError("No player reference!");
            return;
        }

        // Find the recipe that grants this upgrade
        CraftingRecipe targetRecipe = _availableRecipies.Find(r => r._upgradeGranted == upgradeType);

        if (targetRecipe == null)
        {
            Debug.LogWarning($"No recipe found for upgrade: {upgradeType}");
            return;
        }

        // Try to craft it
        CraftRecipe(targetRecipe);
    }

    /// <summary>
    /// Updates the upgrade UI to reflect current player upgrade levels and button interactability.
    /// </summary>
    private void UpdateUpgradeUI()
    {
        if (_currentPlayer == null) return;

        // Update Mobility
        UpdateUpgradeTile(UpgradesEnum.Mobility, _mobilityButton, _mobilityLevelText);

        // Update Sight/Vision
        UpdateUpgradeTile(UpgradesEnum.Vision, _sightButton, _sightLevelText);

        // Update Vigor
        UpdateUpgradeTile(UpgradesEnum.Vigor, _vigorButton, _vigorLevelText);
    }

    /// <summary>
    /// Updates a single upgrade tile's level text and button interactability.
    /// </summary>
    /// <param name="upgradeType">The upgrade type.</param>
    /// <param name="button">The button for this upgrade.</param>
    /// <param name="levelText">The TextMeshPro component displaying level.</param>
    private void UpdateUpgradeTile(UpgradesEnum upgradeType, Button button, TextMeshProUGUI levelText)
    {
        // Get current upgrade level
        int currentLevel = 0;
        if (_currentPlayer.Upgrades.ContainsKey(upgradeType))
        {
            currentLevel = _currentPlayer.Upgrades[upgradeType];
        }

        // Update level text
        if (levelText != null)
        {
            levelText.text = $"Lvl. {currentLevel}";
        }

        // Find the recipe for this upgrade
        CraftingRecipe recipe = _availableRecipies.Find(r => r._upgradeGranted == upgradeType);

        if (recipe != null && button != null)
        {
            // Check if player can craft (has required items)
            bool canCraft = recipe.CanCraft(_currentPlayer.Inventory);
            button.interactable = canCraft;
        }
    }

}
