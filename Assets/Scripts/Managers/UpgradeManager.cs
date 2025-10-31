using System.Collections.Generic;
using UnityEngine;

public enum UpgradesEnum
{
    Vision,
    Mobility,
    Vigor
}

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance { get; private set; }

    [Header("Upgrade Multipliers Per Level")]
    [SerializeField] private float mobilitySpeedBonus = 2f; // +2 speed per level
    [SerializeField] private float mobilityTurningBonus = 15f; // +15 turning speed per level
    // Vision and Vigor not implemented yet - add when those systems exist

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Applies all upgrades from the player's upgrade dictionary to the player.
    /// </summary>
    /// <param name="player">The player to apply upgrades to.</param>
    public void ApplyAllUpgrades(Player player)
    {
        if (player == null)
        {
            Debug.LogError("Cannot apply upgrades - player is null!");
            return;
        }

        // Apply each upgrade the player has
        foreach (KeyValuePair<UpgradesEnum, int> upgrade in player.Upgrades)
        {
            ApplyUpgrade(upgrade.Key, upgrade.Value, player);
        }
    }

    /// <summary>
    /// Applies a specific upgrade at a given level to the player.
    /// </summary>
    /// <param name="upgrade">The upgrade type.</param>
    /// <param name="level">The upgrade level.</param>
    /// <param name="player">The player to apply to.</param>
    private void ApplyUpgrade(UpgradesEnum upgrade, int level, Player player)
    {
        switch(upgrade)
        {
            case UpgradesEnum.Mobility:
                ApplyMobilityUpgrade(level, player);
                break;
            case UpgradesEnum.Vigor:
                // TODO: Implement when health system exists
                Debug.Log($"Vigor upgrade level {level} - Not yet implemented");
                break;
            case UpgradesEnum.Vision:
                // TODO: Implement when camera/vision system exists
                Debug.Log($"Vision upgrade level {level} - Not yet implemented");
                break;
            default:
                Debug.LogError($"Unknown upgrade: {upgrade}");
                return;
        }
    }

    /// <summary>
    /// Applies mobility upgrade to player movement.
    /// </summary>
    private void ApplyMobilityUpgrade(int level, Player player)
    {
        if (player.Movement == null)
        {
            Debug.LogError("Player Movement component is null!");
            return;
        }

        // Calculate bonuses based on level
        float speedBonus = mobilitySpeedBonus * level;
        float turningBonus = mobilityTurningBonus * level;

        // Apply to player movement
        player.Movement.AddSpeedBonus(speedBonus);
        player.Movement.AddTurningBonus(turningBonus);

        Debug.Log($"Mobility upgrade level {level} applied! Speed +{speedBonus}, Turning +{turningBonus}");
    }


}
