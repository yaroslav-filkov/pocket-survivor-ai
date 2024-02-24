using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/creature_configuration")]
public class CreatureConfiguration : ScriptableObject, IIdentifiable<CreatureId>
{
    [field:SerializeField] public CreatureId Id { get; set; }

    public Sprite Sprite;
}
  
