public class Cloth : Item
{
    public int ArmorValue {  get; private set; }
    public string BodyId { get; private set; }
    public override void Setup()
    { 
        ArmorValue = int.Parse(RemoteConfigDto.MetaValues[0]);
        BodyId = RemoteConfigDto.MetaValues[1];
    }
}
