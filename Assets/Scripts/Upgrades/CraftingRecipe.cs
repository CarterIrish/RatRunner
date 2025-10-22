using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeRecipe", menuName = "Crafting Recipe/Upgrade", order = 1 )]
public class CraftingRecipe : ScriptableObject
{
    [Header("Recipe Info")]
    public UpgradesEnum _name;
    [TextArea(3, 5)]
    public string _description;

    [Header("Requirements")]
    public List<ItemRequirement> _requiredItems;

    [Header("Upgrade Granted")]
    public UpgradesEnum _upgradeGranted;

    [System.Serializable]
    public class ItemRequirement
    {
        public ItemsEnum _itemType;
        public int _quantity;
    }

    public bool CanCraft(Inventory playerInv)
    {
        bool craftable = _requiredItems.All(item =>
        playerInv.HasItem(item._itemType, item._quantity));

        return craftable;
    }
}
