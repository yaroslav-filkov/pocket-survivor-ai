using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlotView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemView _prefab;
    [SerializeField] private TextMeshProUGUI _armorValue;
    [SerializeField] private TextMeshProUGUI _namePart;
    [SerializeField] private Image _partIcon;

    private EquipmentSlot _model;
    private ItemView _currentItemInSlot;
    private ItemRepository _itemRepository;

    public void Setup(EquipmentSlot model, Sprite icon, ItemRepository itemRepository)
    {
        _model = model;
        _itemRepository = itemRepository;

        _model.Added = OnAdded;
        _model.Removed = OnRemoved;

        if (!_model.IsEmpty)
            OnAdded(_model.CurrentItem.CurrentAmount);

        _partIcon.sprite = icon;
        _namePart.text = LocalizationService.GetTextTranslation(_model.AcceptableId);
    }



    public void OnAdded(int currentAmount)
    {
        if (currentAmount > 0)
        {
            _currentItemInSlot = Instantiate(_prefab, transform);
            _armorValue.text = (_itemRepository.GetById(_model.CurrentItem.ItemId) as Cloth).ArmorValue.ToString();
            _currentItemInSlot.Setup(in _model.CurrentItem, _model, false);

        }
    }
    public void OnRemoved(int currentAmount)
    {
        if (currentAmount == 0)
        {
            Destroy(_currentItemInSlot.gameObject);
            _armorValue.text = "0";
        }
        else
        {
            _currentItemInSlot.UpdateValue(currentAmount);
        }
    }

}
