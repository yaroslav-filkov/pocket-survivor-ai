using System;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour , IIndentity
{
    public WeaponConfiguration Configuration;
    public WeaponRemoteConfigDto RemoteConfigDto {  get; private set; }
    public Identificator Id => Configuration.Id;

    public abstract void Attack();

    public void SetupRemoteConfig(WeaponRemoteConfigDto remoteConfig)
    {
        RemoteConfigDto = remoteConfig;
    }

}
