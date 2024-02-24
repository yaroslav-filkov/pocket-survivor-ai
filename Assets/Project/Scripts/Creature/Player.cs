using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Creature
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Button _shootButton;
    [SerializeField] private ItemRepository _itemRepository;
    [SerializeField] private List<EquipmentSlotCongiguration> _congigurations;
    public Equipment Equipment { get; private set; } 

    protected override void OnEnable()
    {
        base.OnEnable();
        Equipment = new Equipment();
        Equipment.CreateSlots(_congigurations);
        _shootButton.onClick.AddListener(Attack);
        ChangeWeapon(_startWeapon);
    }

    private void OnDisable()
    {
        _shootButton.onClick.AddListener(Attack);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        CurrentWeapon = weapon;
    }

    public override void ApplyDamage(in DamageData damageData)
    {
        DamageData damageDataNew = damageData;
        var bodyId = damageData.TagHitPoint;
        var needItem = Equipment.GetItemBySlot(bodyId);
        if(needItem != null)
        {
            var item = _itemRepository.GetById(needItem) as Cloth;
            damageDataNew = new DamageData(Mathf.FloorToInt(damageData.Damage * (1 - (item.ArmorValue * 0.01f))));
        }
        base.ApplyDamage(damageDataNew);
    }


}
