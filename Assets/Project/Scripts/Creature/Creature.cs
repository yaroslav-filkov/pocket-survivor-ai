using System;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamagable , IIndentity
{
    public static event Action<Creature, DeathData> AnyDied;

    public event Action<DeathData> Died;
    public event Action Respawned;
    public event Action<DamageData> DamageDone;
    public event Action<RestoreData> RestoreDone;


    public CreatureConfiguration Configuration;

    public CreatureRemoteConfigDto RemoteConfigDto {  get; private set; }
    public bool IsDied { get; private set; }
    public Weapon CurrentWeapon { get; protected set; }
    protected GameValue Health { get; set; }

    public bool IsEquipWeapon => CurrentWeapon != null;
    public Identificator Id => Configuration.Id;
    public GameValue HealthPoint => Health;

    public void SetupRemoteConfig(CreatureRemoteConfigDto creatureRemoteConfigDto)
    {
        RemoteConfigDto = creatureRemoteConfigDto;
        Health = new GameValue(RemoteConfigDto.Health);
    }

    protected virtual void OnEnable()
    {
        ResetValues();
    }

    protected virtual void ResetValues()
    {
        IsDied = false;
        Health.Value = Health.StartValue;
    }

    public virtual void Respawn()
    {
        Respawned?.Invoke();
        ResetValues();
    }
    public virtual void Die(in DeathData deathData)
    {
        IsDied = true;
        AnyDied?.Invoke(this, deathData);
        Died?.Invoke( deathData);
    }

    public virtual void ApplyDamage(in DamageData damageData)
    {
        if (IsDied)
            return;

        if (Health.SubtractAndReturnCurrentValue(damageData.Damage) < 0)
        {
            Health.Value = 0;
            Die(new DeathData(in damageData));
        }
        DamageDone?.Invoke(damageData);
    }

    public virtual void Restore(in RestoreData restoreData)
    {
        Health.AddNeed(restoreData.RestoreValue);
        RestoreDone?.Invoke(restoreData);
    }

    protected virtual void Attack()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Attack();
        }
        else
        {
            Hit();
        }
    }

    protected virtual void Hit()
    {

    }
}
