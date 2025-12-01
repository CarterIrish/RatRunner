
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DevConsole : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject consoleUI;
    [SerializeField] private TMP_InputField commandInputField;
    [SerializeField] private TextMeshProUGUI placeholderText;
    [SerializeField] private Player player;

    private bool isOpen = false;

    // Command registry - maps command names to their execution functions
    private Dictionary<string, Action<string[]>> commands = new Dictionary<string, Action<string[]>>();

    private void Start()
    {
        // Register all available commands
        RegisterCommands();
    }

    private void OnEnable()
    {
        GameManager.OnConsoleClosed.AddListener(HideConsole);
        GameManager.OnConsoleOpened.AddListener(ShowConsole);
        consoleUI.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnConsoleClosed.RemoveListener(HideConsole);
        GameManager.OnConsoleOpened.RemoveListener(ShowConsole);
    }

    public void OnOpenConsole(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            ToggleConsole();
        }
    }

    private void ToggleConsole()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            GameManager.Instance.ChangeGameState(GameStates.CONSOLE);
        }
        else
        {
            GameManager.Instance.ChangeGameState(GameStates.PLAYING);
        }
    }

    // Called when user presses Enter in the input field
    public void OnSubmitCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        // Parse the command and arguments
        string[] parts = input.Trim().Split(' ');
        string commandName = parts[0].ToLower();

        // Get arguments (everything after the command name)
        string[] args = new string[parts.Length - 1];
        Array.Copy(parts, 1, args, 0, args.Length);

        // Execute the command if it exists
        if (commands.ContainsKey(commandName))
        {
            try
            {
                commands[commandName].Invoke(args);
                Debug.Log($"Executed command: {commandName}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error executing command '{commandName}': {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"Unknown command: '{commandName}'");
        }

        // Clear the input field
        commandInputField.text = "";
        commandInputField.ActivateInputField();
    }

    // Register a new command
    private void RegisterCommand(string commandName, Action<string[]> callback)
    {
        commands[commandName.ToLower()] = callback;
    }

    private void RegisterCommands()
    {
        // Register debug commands here
        RegisterCommand("noclip", ToggleCollisions);
        RegisterCommand("nogravity", ToggleGravity);
        RegisterCommand("endgame", EndGame);
        RegisterCommand("giveupgrade", GiveUpgrade);
        RegisterCommand("mouselock", MouseLock);
        RegisterCommand("godmode", ToggleGodMode);
        RegisterCommand("giveitem", GiveItem);

    }

    private void MouseLock(string[] args)
    {
        if(args.Length == 0)
        {
            Debug.LogWarning("Usage: mouselock <true|false>");
            return;
        }

        string input = args[0].ToLower();
        switch(input)
        {
            case "true":
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameManager.DebugModeActive = false; // Disable debug mode (normal gameplay)
                Debug.Log("Mouse locked. Debug mode disabled.");
                return;
            case "false":
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameManager.DebugModeActive = true; // Enable debug mode (prevent state overrides)
                Debug.Log("Mouse unlocked. Debug mode enabled.");
                return;
            default:
                Debug.LogWarning("No valid args. Usage: mouselock <true|false>");
                return;
        }
    }

    private void GiveUpgrade(string[] args)
    {
        if (args.Length == 0)
        {
            Debug.LogWarning("Usage: mobility | vigor | vision");
            return;
        }


        string upgrade = args[0].ToLower();
        try
        {
            switch (upgrade)
            {
                case "mobility":
                    player.Upgrades[UpgradesEnum.Mobility] = player.Upgrades.ContainsKey(UpgradesEnum.Mobility) ? player.Upgrades[UpgradesEnum.Mobility] + 1 : 1;
                    return;
                case "vigor":
                    player.Upgrades[UpgradesEnum.Vigor] = player.Upgrades.ContainsKey(UpgradesEnum.Vigor) ? player.Upgrades[UpgradesEnum.Vigor] + 1 : 1;
                    return;
                case "vision":
                    player.Upgrades[UpgradesEnum.Vision] = player.Upgrades.ContainsKey(UpgradesEnum.Vision) ? player.Upgrades[UpgradesEnum.Vision] + 1 : 1;
                    return;
                default:
                    Debug.LogWarning("No valid args. Usage: mobility | vigor | vision");
                    return;

            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error giving upgrade '{upgrade}': {e.Message}");
        }
        finally
        {
            UpgradeManager.Instance.ApplyAllUpgrades(player);
        }
    }


    /// <summary>
    /// Toggles the gravity.
    /// </summary>
    /// <param name="args">The object.</param>
    private void ToggleGravity(string[] args)
    {
        if(player == null) { Debug.LogWarning("Player not found"); return; }
        player.Movement.EnableGravity = !player.Movement.EnableGravity;
        if(!player.Movement.EnableGravity)
        {
            player.Rigidbody.velocity = Vector3.zero;
        }
        Debug.Log($"Gravity: {player.Movement.EnableGravity}");
    }

    /// <summary>
    /// Disables the collisons.
    /// </summary>
    /// <param name="obj">The object.</param>
    private void ToggleCollisions(string[] args)
    {
        if (player == null) { Debug.LogWarning("Player not found"); return; }

        player.BoxCollider.enabled = !player.BoxCollider.enabled;
        Debug.Log($"Collisions: {player.BoxCollider.enabled}");
    }

    private void EndGame(string[] args)
    {
        // Validate that an argument was provided
        if (args.Length == 0)
        {
            Debug.LogWarning("Usage: endgame <win|loss>");
            return;
        }

        // Parse the win/loss argument (case-insensitive)
        string outcome = args[0].ToLower();

        switch (outcome)
        {
            case "win":
                GameManager.Instance.EndGame(true);
                Debug.Log("Ending game as WIN");
                return;

            case "loss":
                GameManager.Instance.EndGame(false);
                Debug.Log("Ending game as LOSS");
                return;

            default:
                Debug.LogWarning($"Invalid argument '{args[0]}'. Usage: endgame <win|loss>");
                return;
        }
    }

    private void ShowConsole()
    {
        consoleUI.SetActive(true);
        // Focus input field when console opens
        if (commandInputField != null)
        {
            commandInputField.ActivateInputField();
            commandInputField.Select();
        }
    }

    private void HideConsole()
    {
        consoleUI.SetActive(false);
    }

    /// <summary>
    /// Toggles godmode with configurable invincibility, no gravity, and no clip features.
    /// </summary>
    /// <param name="args">The arguments.</param>
    private void ToggleGodMode(string[] args)
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            return;
        }

        // Toggle godmode state
        player.GodModeActive = !player.GodModeActive;

        if (player.GodModeActive)
        {
            Debug.Log("Godmode ENABLED");

            // Apply configured godmode features
            if (player.GodModeInvincibility)
            {
                Debug.Log("  - Invincibility: ON");
            }

            if (player.GodModeNoGravity)
            {
                player.Movement.EnableGravity = false;
                player.Rigidbody.velocity = Vector3.zero;
                Debug.Log("  - No Gravity: ON");
            }

            if (player.GodModeNoClip)
            {
                player.BoxCollider.enabled = false;
                Debug.Log("  - No Clip: ON");
            }
        }
        else
        {
            Debug.Log("Godmode DISABLED");

            // Restore normal state
            if (player.GodModeInvincibility)
            {
                Debug.Log("  - Invincibility: OFF");
            }

            if (player.GodModeNoGravity)
            {
                player.Movement.EnableGravity = true;
                Debug.Log("  - No Gravity: OFF");
            }

            if (player.GodModeNoClip)
            {
                player.BoxCollider.enabled = true;
                Debug.Log("  - No Clip: OFF");
            }
        }
    }

    private void GiveItem(string[] args)
    {
        Debug.Log("GiveItem enter");
        if(args.Length == 0)
        {
            placeholderText.text = "Usage: giveitem <spring|needle|key|thread>";
            return;
        }

        string outcome = args[0].ToLower();
        switch (outcome)
        {
            case "spring":
                player.Inventory.AddItem(ItemsEnum.spring, 1);
                return;
            case "key":
                player.Inventory.AddItem(ItemsEnum.key, 1);
                return;
            case "needle":
                player.Inventory.AddItem(ItemsEnum.needle, 1);
                return;
            case "thread":
                player.Inventory.AddItem(ItemsEnum.thread, 1);
                return;
            default:
                placeholderText.text = $"Invalid argument '{args[0]}'. Usage: giveitem <spring|needle|key|thread>";
                return;
        }
    }

}
