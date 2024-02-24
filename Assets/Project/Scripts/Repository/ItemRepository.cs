
using System.Collections.Generic;

public class ItemRepository : Repository<Item>
{
    protected override void Setup()
    {
        base.Setup();

        foreach (Item item in RepositoryObjects)
        {
            var itemId = item.Id;
            var name = GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, itemId, "name_localization_id");
            var description = GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, itemId, "description_localization_id");
            var weight = GeneralGameConfig.GetConfigRemoteDto<float>(DictionaryRemoteDto, itemId, "weight");
            var defaultCapacityPerSlot = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, itemId, "default_capacity_per_slot");
            var metaValue1 = GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, itemId, "meta_value_1");
            var metaValue2= GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, itemId, "meta_value_2");

       

            item.SetupRemoteConfig(
                new ItemRemoteConfigDto(name, description, weight, 
                defaultCapacityPerSlot, new List<string>()
                {
                     metaValue1,
                     metaValue2,            
                }));
            item.Setup();
        }
    }
}
