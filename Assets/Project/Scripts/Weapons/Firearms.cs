using UnityEngine;

public class Firearms : Weapon
{
    [SerializeField] private Creature _target;
    [SerializeField] private ItemRepository _itemRepository;
    [SerializeField] private InventoryRepository _inventoryRepository;
    [SerializeField] private InventoryId _needInventoryId;

    public override void Attack()
    {
        var inventory = _inventoryRepository.GetById(_needInventoryId);
        var ammo = Mathf.Min(inventory.GetItemValue(RemoteConfigDto.AmmoId), RemoteConfigDto.SeveralShots);
        if (inventory.HasItemValue(RemoteConfigDto.AmmoId, ammo))
        {
            var item = _itemRepository.GetById(RemoteConfigDto.AmmoId) as Ammo;
            inventory.Take(RemoteConfigDto.AmmoId, ammo);
            _target.ApplyDamage(new DamageData(ammo * item.Damage));
        }
    }
}