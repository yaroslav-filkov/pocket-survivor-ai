using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentSlotView : MonoBehaviour,
  IDropHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemView _prefab;
    [SerializeField] private TextMeshProUGUI _armorValue;
    [SerializeField] private TextMeshProUGUI _namePart;
    [SerializeField] private Image _partIcon;
    
    public EquipmentSlot Model;


    private ItemView _currentItemInSlot;
    private ItemRepository _itemRepository;

    public void Setup(EquipmentSlot model, Sprite icon, ItemRepository itemRepository)
    {
        Model = model;
        _itemRepository = itemRepository;

        Model.Added = OnAdded;
        Model.Removed = OnRemoved;

        if (!Model.IsEmpty)
            OnAdded(Model.CurrentItem.CurrentAmount);

        _partIcon.sprite = icon;
        _namePart.text = LocalizationService.GetTextTranslation(Model.AcceptableId);
    }



    public void OnAdded(int currentAmount)
    {
        if (currentAmount > 0)
        {
            _currentItemInSlot = Instantiate(_prefab, transform);
            _armorValue.text = (_itemRepository.GetById(Model.CurrentItem.ItemId) as Cloth).ArmorValue.ToString();
            _currentItemInSlot.Setup(in Model.CurrentItem, false);

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



 
    public void OnDrop(PointerEventData eventData)
    {
       
    }
}
