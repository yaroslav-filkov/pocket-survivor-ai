public readonly struct DamageData
{
    public readonly int Damage;
    public readonly string TagHitPoint;
    public readonly Creature Damager;


    public DamageData(int damage, string tagHitPoint = "", Creature damager = null)
    {
        Damage = damage;
        Damager = damager;
        TagHitPoint = tagHitPoint;
    }
}
