using System;
using static UnityEditor.Progress;

[Serializable]
public class InventorySlot 
{
    public readonly int IdSlot;

    public InventoryItem CurrentItem = new InventoryItem();

    public Action<int> Added;
    public Action<int> Removed;
    public Action Taked;
    public InventorySlot(int numberSlot)
    {
        IdSlot = numberSlot;
    }
    public bool IsEmpty => CurrentItem.CurrentAmount == 0;
    public bool IsFull => !IsEmpty && CurrentItem.CurrentAmount == CurrentItem.DefaultCapacityPerSlot;

    public virtual void Add(in InventoryItem item, int amount) 
    {
        if (IsEmpty)
        {
            CurrentItem = item;
        }
       
        CurrentItem.CurrentAmount += amount;
        Added?.Invoke(CurrentItem.CurrentAmount);
    }
    public virtual void Remove(int amount)
    {
        CurrentItem.CurrentAmount -= amount;
        Removed?.Invoke(CurrentItem.CurrentAmount);
    }

    public virtual void Take(int amount)
    {
        CurrentItem.CurrentAmount -= amount;
        if (CurrentItem.CurrentAmount < 0)
            CurrentItem.CurrentAmount = 0;
        Taked?.Invoke(); 
        
    }

    public virtual void Move(in InventoryItem from)
    {
         CurrentItem = from;
    }
}
