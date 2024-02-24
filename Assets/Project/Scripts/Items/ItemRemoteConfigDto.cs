using System.Collections;
using System.Collections.Generic;

public class ItemRemoteConfigDto 
{
    public string Name = "item";
    public string Description= "nice_item";
    public float Weight = 0;
    public int DefaultCapacityPerSlot = 1;
    public List<string> MetaValues = new List<string>();

    public ItemRemoteConfigDto(string name, string description, float weight, int defaultCapacityPerSlot, List<string> metaValues)
    {
        Name = name;
        Description = description;
        Weight = weight;
        DefaultCapacityPerSlot = defaultCapacityPerSlot;
        MetaValues = metaValues;
    }
}
