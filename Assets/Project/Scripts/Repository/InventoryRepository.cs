public class InventoryRepository : Repository<Inventory>
{
    protected override void Setup()
    {
        base.Setup();

        foreach (Inventory inventory in RepositoryObjects)
        {
            var inventoryId = inventory.Id;
            var amountSlots = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, inventoryId, "inventory_slot_amount");
            var numberOfColumns = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, inventoryId, "number_of_columns_inventory");
            inventory.SetupRemoteConfig(new InventoryRemoteConfigDto(amountSlots, numberOfColumns));
           
        }
    }
}
