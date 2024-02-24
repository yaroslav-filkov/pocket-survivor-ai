public class CreatureRepository : Repository<Creature>
{

    protected override void Setup()
    {
        base.Setup();

        foreach (Creature creature in RepositoryObjects)
        {
            var creatureId = creature.Configuration.Id;
            var name = GeneralGameConfig.GetConfigRemoteDto<string>(DictionaryRemoteDto, creatureId, "name_localization");
            var hp = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, creatureId, "hp");
            var damage = GeneralGameConfig.GetConfigRemoteDto<int>(DictionaryRemoteDto, creatureId, "damage");
            creature.SetupRemoteConfig(new CreatureRemoteConfigDto(name, hp, damage));
        }
    }
}
