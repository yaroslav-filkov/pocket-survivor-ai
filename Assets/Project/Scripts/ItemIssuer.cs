using System.Collections.Generic;
using UnityEngine;

public class ItemIssuer : MonoBehaviour
{
    [SerializeField] private InventoryRepository _inventoryRepository;
    [SerializeField] private ItemRepository _itemRepository;
    [SerializeField] private InventoryId _inventoryId;
    [SerializeField] private List<ItemId> _giftItems = new List<ItemId>();

    [SerializeField] private Creature _creatureObserveDeath;

    private void OnEnable()
    {
        _creatureObserveDeath.Died += OnDied;
    }
    private void OnDisable()
    {
        _creatureObserveDeath.Died -= OnDied;
    }
    private void OnDied(DeathData data)
    {
        var inventory = _inventoryRepository.GetById(_inventoryId);
        var item = _itemRepository.GetById(_giftItems[Random.Range(0, _giftItems.Count)]);
        inventory.PutFirst(new InventoryItem(item.Configuration.Id, item.RemoteConfigDto.DefaultCapacityPerSlot, item.Configuration.Icon), 1);
    }
}
