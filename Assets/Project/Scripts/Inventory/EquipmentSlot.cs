public class EquipmentSlot : InventorySlot
{
    public PartOfBodyId AcceptableId;

    public EquipmentSlot(int numberSlot, PartOfBodyId acceptableId) : base(numberSlot)
    {
        AcceptableId = acceptableId;
    }
}
