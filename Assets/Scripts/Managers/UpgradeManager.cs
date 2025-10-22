using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private HashSet<UpgradesEnum> activeUpgrades = new HashSet<UpgradesEnum>();

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

    public void ApplyUpgrade(UpgradesEnum upgrade)
    {
        if(activeUpgrades.Contains(upgrade))
        {
            Debug.LogWarning($"Upgrade: `{upgrade}` is already active");
            return;
        }

        activeUpgrades.Add(upgrade);

        // We will need to reference a player manager/controller script here to actually apply upgrades
        switch(upgrade)
        {
            case UpgradesEnum.Mobility:
                break;
            case UpgradesEnum.Vigor:
                break;
            case UpgradesEnum.Vision:
                break;
            default:
                Debug.LogError($"Unknown upgrade: {upgrade}");
                return;
        }

        Debug.Log($"Upgrade applied: {upgrade}");
    }


}
