public class Ammo : Item
{
    public int Damage {  get; private set; }

    public override void Setup()
    {
        Damage = int.Parse(RemoteConfigDto.MetaValues[0]);
    }
}
