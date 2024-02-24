using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IPointerClickHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _amountInSlot;

    public InventoryItem Model {  get; private set; }
    public InventorySlot SlotModel { get; private set; }

    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private Transform _parentAfterDraging;
    private bool _isClickable = true;
    public void Setup(in InventoryItem model, InventorySlot slotModel, bool isClickable = true)
    {
        Model = model; 
        _isClickable = isClickable;
        SlotModel = slotModel;
        UpdateView();
    }

    public void Setup(InventorySlot slotModel)
    {
        SlotModel = slotModel;
    }

    public void UpdateValue(int value) 
    {
        _amountInSlot.text = value.ToString();
        UpdateAmountVisible();
    }

    public void SetupNewParent(Transform newParent)
    {
        transform.SetParent(newParent);
        _parentAfterDraging = newParent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClickable)
            ShowInfo();
    }




    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isClickable)
            return;
        _itemIcon.raycastTarget = false;
        _parentAfterDraging = transform.parent;
        transform.SetParent(_mainCanvas.transform);

        transform.SetAsLastSibling();

        _itemIcon.maskable = false;

        _amountInSlot.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isClickable)
            return;
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isClickable)
            return;
        _itemIcon.raycastTarget = true;
        transform.SetParent(_parentAfterDraging);

       
        _itemIcon.maskable = true;
        UpdateAmountVisible();

    }

    private void UpdateAmountVisible()
    {
        _amountInSlot.gameObject.SetActive(Model.CurrentAmount > 1);
    }

    private void ShowInfo()
    {
        PopupItem.Show(Model.ItemId);
    }
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
    }
    private void UpdateView()
    {
        _itemIcon.sprite = Model.Icon;
        UpdateValue(Model.CurrentAmount);
    }
}
