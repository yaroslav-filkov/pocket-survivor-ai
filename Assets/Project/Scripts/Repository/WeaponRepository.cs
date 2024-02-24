public class WeaponRepository : Repository<Weapon>
{

    protected override void Setup()
    {
        base.Setup();

        foreach (var weapon in RepositoryObjects)
        {
            var weaponId = weapon.Id;
            var ammoId = GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, weaponId, "ammo_item_id");
            var severalShots = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, weaponId, "several_shots");
            weapon.SetupRemoteConfig(new WeaponRemoteConfigDto(ammoId, severalShots));
        }
    }
}
