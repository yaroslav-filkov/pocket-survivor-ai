using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/item_configuration")]
public class ItemConfiguration : ScriptableObject, IIdentifiable<ItemId>
{
    [field: SerializeField] public ItemId Id {  get;  set; }
    public Sprite Icon;
}
