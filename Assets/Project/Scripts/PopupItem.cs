using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItem : MonoBehaviour
{
    private static PopupItem _instance;
    [SerializeField] private string LOCALIZTION_EQUIP = "equip";
    [SerializeField] private string LOCALIZTION_TREAT = "treat";
    [SerializeField] private string LOCALIZTION_BUY = "buy";
    [SerializeField] private string LOCALIZTION_WEIGHT = "kg";


    [SerializeField] private TextMeshProUGUI _nameItem;
    [SerializeField] private TextMeshProUGUI _metaValue;
    [SerializeField] private TextMeshProUGUI _weightValue;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private Image _icon;
    [SerializeField] private Button _delete;
    [SerializeField] private Button _dynamicButton;
    [SerializeField] private GameObject _popup;
    [SerializeField] private ItemRepository _itemRepository;
    [SerializeField] private InventoryRepository _inventoryRepository;
    [SerializeField] private InventoryId _needInventory;
    [SerializeField] private Player _player;

    private ItemId _currentItem;

    private void Awake()
    {
        _instance = this; 
    }
    public static void Show(ItemId itemId)
    {
        _instance.ShowPopup(itemId);
    }
    private void OnEnable()
    {
        _delete.onClick.AddListener(DeleteItem);
        _dynamicButton.onClick.AddListener(UseItem);
    }
    private void OnDisable()
    {
        _delete.onClick.RemoveListener(DeleteItem);
        _dynamicButton.onClick.RemoveListener(UseItem);
    }
    private void ShowPopup(ItemId itemId)
    {
        _popup.gameObject.SetActive(true);
        LoadViewDataPopup(itemId);
    }

    private void LoadViewDataPopup(ItemId itemId)
    {
        _currentItem = itemId;

        var item = _itemRepository.GetById(_currentItem);
        _nameItem.text = LocalizationService.GetTextTranslation(item.RemoteConfigDto.Name);
        _weightValue.text = item.RemoteConfigDto.Weight + LocalizationService.GetTextTranslation(LOCALIZTION_WEIGHT);
        _metaValue.text = item.RemoteConfigDto.MetaValues[0];
        _icon.sprite = item.Configuration.Icon;
        _buttonText.text = LocalizationService.GetTextTranslation(GetLocal(item));
    }
    private void UseItem()
    {
        var item = _itemRepository.GetById(_currentItem);
        var inventory = _inventoryRepository.GetById(_needInventory);
        if (item is Ammo ammo)
        {
            var itemValue = inventory.GetItemValue(ammo.Id);
            var needValue = ammo.RemoteConfigDto.DefaultCapacityPerSlot - itemValue;
            inventory.PutFirst(new InventoryItem(ammo.Id as ItemId, ammo.RemoteConfigDto.DefaultCapacityPerSlot, ammo.Configuration.Icon), needValue);
        }
        else if (item is FirstAidKit firstAidKit)
        {
            _player.Restore(new RestoreData(firstAidKit.RestoreValue));
          
            inventory.Take(_currentItem, 1);
        }
        else if (item is Cloth  cloth)
        {
            _player.Equipment.PutFirst(cloth);
            inventory.Take(_currentItem, 1);
        }

      

        _popup.SetActive(false);
        _currentItem = null;
    }
    private void DeleteItem()
    {
        var inventory = _inventoryRepository.GetById(_needInventory);
        inventory.Take(_currentItem, 1);

        _popup.SetActive(false);
        _currentItem = null;
    }

    private string GetLocal(Item item)
    {
        string local = null;

        if (item as Ammo)
        {
            local = LOCALIZTION_BUY;
        }
        else if (item as FirstAidKit)
        {
            local = LOCALIZTION_TREAT;
        }
        else if (item as Cloth)
        {
            local = LOCALIZTION_EQUIP;
        }
        return local;
    }
}
