using System;
public interface IDamagable 
{
    public GameValue HealthPoint { get; }
    public void ApplyDamage(in DamageData damageData);
    public void Restore(in RestoreData restoreData);

    public event Action<DamageData> DamageDone;

    public event Action<RestoreData> RestoreDone;

}
