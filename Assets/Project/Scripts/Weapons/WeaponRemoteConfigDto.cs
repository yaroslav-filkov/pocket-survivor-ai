public class WeaponRemoteConfigDto
{
    public string AmmoId;
    public int SeveralShots;
    public WeaponRemoteConfigDto(string ammoId, int severalShots)
    {
        AmmoId = ammoId;
        SeveralShots = severalShots;
    }
}
