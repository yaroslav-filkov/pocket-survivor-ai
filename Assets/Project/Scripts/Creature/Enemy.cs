using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PartOfBodyId> _partOfBodyId;

    private int _bodyIterator = 0;
    protected override void OnEnable()
    {
        base.OnEnable();
        DamageDone += OnDamageDone;
    }


    private void OnDisable()
    {
        DamageDone -= OnDamageDone;
    }
    private void OnDamageDone(DamageData damageData)
    {
        Attack();
    }
    protected override void Hit()
    {
        _player.ApplyDamage(new DamageData(RemoteConfigDto.Damage, _partOfBodyId[_bodyIterator]));
        _bodyIterator++;
        if (_bodyIterator>= _partOfBodyId.Count)
        {
            _bodyIterator = 0;
        }
    }
}
