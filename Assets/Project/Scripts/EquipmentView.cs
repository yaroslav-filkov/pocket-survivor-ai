using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour
{
    [SerializeField] private Transform _slotsContent;
    [SerializeField] private EquipmentSlotView _prefabEquipmentSlotView;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    [SerializeField] private InventoryRepository _inventoryRepository;
    [SerializeField] private ItemRepository _itemRepository;

    private List<EquipmentSlotView> _createdSlotsView = new List<EquipmentSlotView>();


    [SerializeField] private Player _player;
    private void Start()
    {
        Create();
    }

    private void Create()
    {
        var model = _player.Equipment;
       
        _gridLayoutGroup.constraintCount = 1;

        for (int i = 0; i < model.Slots.Count; i++)
        {
            var slot = Instantiate(_prefabEquipmentSlotView, _slotsContent);
            slot.Setup(model.Slots.First(x => x.IdSlot == i), model.EquipmentSlotCongigurations[i].Icon, _itemRepository);
            _createdSlotsView.Add(slot);
        }

    }
}


