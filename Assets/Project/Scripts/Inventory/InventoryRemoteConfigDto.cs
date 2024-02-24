public class InventoryRemoteConfigDto 
{
    public int InventoryAmountOfSlots;
    public int NumberOfColumnsInventory;

    public InventoryRemoteConfigDto(int inventoryAmountOfSlots, int numberOfColumnsInventory)
    {
        InventoryAmountOfSlots = inventoryAmountOfSlots;
        NumberOfColumnsInventory = numberOfColumnsInventory;
    }
}
