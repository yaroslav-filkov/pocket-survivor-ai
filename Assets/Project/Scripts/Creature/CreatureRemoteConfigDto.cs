public class CreatureRemoteConfigDto 
{
    public string Name;
    public int Health;
    public int Damage;
    public CreatureRemoteConfigDto(string name, int health , int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
}
