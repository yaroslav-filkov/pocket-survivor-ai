using UnityEngine;

public struct InventoryItem
{
    public readonly ItemId ItemId;
    public readonly Sprite Icon;
    public readonly int DefaultCapacityPerSlot;

    public int CurrentAmount;

    public InventoryItem(ItemId itemId, int defaultCapacityPerSlot, Sprite icon)
    {
        ItemId = itemId;
        DefaultCapacityPerSlot = defaultCapacityPerSlot;
        Icon = icon;
        CurrentAmount = 0;
    }
}
