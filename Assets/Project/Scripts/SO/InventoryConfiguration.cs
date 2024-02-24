using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/inventory_configuration")]
public class InventoryConfiguration : ScriptableObject, IIdentifiable<InventoryId>
{
   [field:SerializeField] public InventoryId Id {  get; set; }
   public List<InventoryStartData> InventoryStartData;
}

[Serializable]
public class InventoryStartData
{
    public int NumberSlot;
    public ItemId ItemId;
    public int AmountItems;
}