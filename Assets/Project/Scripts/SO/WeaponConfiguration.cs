using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/weapon_configuration")]
public class WeaponConfiguration : ScriptableObject, IIdentifiable<WeaponId>
{
    [field:SerializeField] public WeaponId Id {  get;  set; }
    public Sprite Icon;
}
