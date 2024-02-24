using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Item : MonoBehaviour , IComparer<Item>, IComparable<Item>, IIndentity
{
    public ItemConfiguration Configuration;
    public ItemRemoteConfigDto RemoteConfigDto {  get; private set; }

    public Identificator Id => Configuration.Id;


    public void SetupRemoteConfig(ItemRemoteConfigDto remoteConfigDto)
    {
        RemoteConfigDto = remoteConfigDto;
    }
    public virtual void Setup() { }
    public int Compare(Item x, Item y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.Configuration.Id, y.Configuration.Id, StringComparison.InvariantCultureIgnoreCase);
    }

    public int CompareTo(Item other)
    {
        if (other == null) return 1;

        return string.Compare(Configuration.Id, other.Configuration.Id, StringComparison.InvariantCultureIgnoreCase);
    }
}
