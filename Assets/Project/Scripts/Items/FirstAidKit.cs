public class FirstAidKit : Item
{
    public int RestoreValue {  get; private set; }

    public override void Setup()
    {
        RestoreValue = int.Parse(RemoteConfigDto.MetaValues[0]);
    }
}
