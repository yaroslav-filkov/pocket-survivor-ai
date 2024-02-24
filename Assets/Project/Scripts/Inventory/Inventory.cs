using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Inventory : IIndentity
{
    public InventoryConfiguration Configuration;
    public InventoryRemoteConfigDto RemoteConfigDto {  get; private set; }

    public Identificator Id => Configuration.Id;

    protected List<InventorySlot> _slots = new List<InventorySlot>();
    
    public IReadOnlyList<InventorySlot> Slots => _slots;
    public void CreateSlots(int amountSlots)
    {
        for (int i = 0; i < amountSlots; i++)
        {
            var slot = new InventorySlot(i);
            _slots.Add(slot);
        }
    }

    public void SetupRemoteConfig(InventoryRemoteConfigDto remoteConfig)
    {
        RemoteConfigDto = remoteConfig;
    }

    public virtual void Put(int slotNumber, in InventoryItem item, int amount)
    {
        var needSlot = _slots.FirstOrDefault(x => x.IdSlot == slotNumber);
        if(needSlot != null)
        {
            needSlot.Add(in item, amount);
        }
    }

    public virtual void PutFirst(InventoryItem item, int amount)
    {
        var needSlot = _slots.FirstOrDefault(x => (!x.IsEmpty && !x.IsFull &&  x.CurrentItem.ItemId.Id == item.ItemId.Id && x.CurrentItem.CurrentAmount + amount <= x.CurrentItem.DefaultCapacityPerSlot));
        
        if(needSlot == null)
        {
            needSlot = _slots.FirstOrDefault(x => x.IsEmpty);
        }

        if (needSlot != null)
        {
            needSlot.Add(in item, amount);
        }
    }
    public virtual void Take(string itemId, int value)
    {
        var needSlot = _slots.FirstOrDefault(x => (!x.IsEmpty && x.CurrentItem.ItemId == itemId) );
        if (needSlot != null)
        {
            needSlot.Remove(value);
        }
    }

    public int GetItemValue(string itemId)
    {
        foreach (var slot in _slots)
        {
            if (!slot.IsEmpty && slot.CurrentItem.ItemId == itemId)
            {
                return slot.CurrentItem.CurrentAmount;
            }
        }
        return 0;
    }
    public bool HasItemValue(string itemId, int needValue)
    {
        foreach (var slot in _slots)
        {
            if (!slot.IsEmpty && slot.CurrentItem.ItemId == itemId && slot.CurrentItem.CurrentAmount >= needValue)
            {
                return true;
            }
        }
        return false;
    }
}
