using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackpackView : MonoBehaviour
{
    [SerializeField] private Transform _slotsContent;
    [SerializeField] private SlotView _prefabSlotView;

    [SerializeField] private GridLayoutGroup  _gridLayoutGroup;

    [SerializeField] private InventoryRepository _inventoryRepository;
    [SerializeField] private ItemRepository _itemRepository;
    [SerializeField] private InventoryId _inventoryId;

    private List<SlotView> _createdSlotsView = new List<SlotView>();

    private void Start()
    {
        CreateInventory();
    }

    private void CreateInventory()
    {
        var backPack = _inventoryRepository.GetById(_inventoryId);
        var remote = backPack.RemoteConfigDto;
        var startItemData = backPack.Configuration.InventoryStartData;
        backPack.CreateSlots(remote.InventoryAmountOfSlots);
        _gridLayoutGroup.constraintCount = remote.NumberOfColumnsInventory;

        for (int i = 0; i < remote.InventoryAmountOfSlots; i++)
        {
            var slot = Instantiate(_prefabSlotView, _slotsContent);
            slot.Setup(backPack.Slots.First(x => x.IdSlot == i));
            _createdSlotsView.Add(slot);
        }

        foreach (var data in startItemData)
        {
            var item = _itemRepository.GetById(data.ItemId);
            backPack.Put(data.NumberSlot, new InventoryItem(item.Configuration.Id, item.RemoteConfigDto.DefaultCapacityPerSlot, item.Configuration.Icon), data.AmountItems);
        }
      

    }
}
