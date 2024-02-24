public readonly struct DeathData
{
    public readonly DamageData LastDamage;
    public DeathData(in DamageData deathData)
    {
        LastDamage = deathData;
    }
}
