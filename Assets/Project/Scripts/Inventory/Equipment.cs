using System.Collections.Generic;
using System.Linq;
public class Equipment
{
    private List<EquipmentSlot> _slots = new List<EquipmentSlot>();
    public IReadOnlyList<EquipmentSlot> Slots => _slots;

    public IReadOnlyList<EquipmentSlotCongiguration> EquipmentSlotCongigurations;
    public void CreateSlots(IReadOnlyList<EquipmentSlotCongiguration> equipmentSlotCongigurations)
    {
        EquipmentSlotCongigurations = equipmentSlotCongigurations;
        for (int i = 0; i < equipmentSlotCongigurations.Count; i++)
        {
            var slot = new EquipmentSlot(i, equipmentSlotCongigurations[i].PartOfBodyId);
            _slots.Add(slot);
        }
    }

    public ItemId GetItemBySlot(string partOfBodyId)
    {
        var slot = Slots.FirstOrDefault(x => x.AcceptableId == partOfBodyId);
        if (slot != null)
        {
            if (!slot.IsEmpty)
            {
                return slot.CurrentItem.ItemId;
            }
        }
        return null;
    }

    public virtual void PutFirst(Cloth item)
    {
        var needSlot = _slots.FirstOrDefault(x => (x.AcceptableId == item.BodyId));

        if (needSlot != null)
        {
            needSlot.Add(new InventoryItem(item.Id as ItemId, item.RemoteConfigDto.DefaultCapacityPerSlot, item.Configuration.Icon ), 1);
        }
    }
}
