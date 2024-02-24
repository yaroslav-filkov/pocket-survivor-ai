using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    private static event Action _anySelceted;
    [SerializeField] private ItemRepository itemRepository;
    [SerializeField] private TextMeshProUGUI _damageValue;
    [SerializeField] private Weapon _attachedModel;

    [SerializeField] private Image  _selectedImage;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unSelectedColor;

    [SerializeField] private Button _selectButton;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelected);
        _anySelceted += OnUnSelected;
    }
    private void Start()
    {
        _damageValue.text = ((itemRepository.GetById(_attachedModel.RemoteConfigDto.AmmoId) as Ammo).Damage * _attachedModel.RemoteConfigDto.SeveralShots).ToString();
        _weaponIcon.sprite = _attachedModel.Configuration.Icon;
        _weaponIcon.SetNativeSize();
    }
    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelected);
        _anySelceted -= OnUnSelected;
    }
    private void OnSelected()
    {
        _anySelceted?.Invoke();
        _selectedImage.color = _selectedColor;
        _player.ChangeWeapon(_attachedModel);
    }
    private void OnUnSelected()
    {
        _selectedImage.color = _unSelectedColor;
    }
}
