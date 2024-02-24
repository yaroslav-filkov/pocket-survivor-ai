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

    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private Transform _parent;
    private bool _isClickable = true;
    public void Setup(in InventoryItem model, bool isClickable = true)
    {
        Model = model; 
        _isClickable = isClickable;
        UpdateView();
    }


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _parent = _rectTransform.parent;
    }

    private void UpdateView()
    {
        _itemIcon.sprite = Model.Icon;
        UpdateValue(Model.CurrentAmount);
    }

    public void UpdateValue(int value) 
    {
        _amountInSlot.text = value.ToString();
        UpdateAmountVisible();
    }
    private void UpdateAmountVisible()
    {
        _amountInSlot.gameObject.SetActive(Model.CurrentAmount > 1);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_mainCanvas.transform);
        transform.SetAsLastSibling();
        _itemIcon.maskable = false;
        _amountInSlot.gameObject.SetActive(false);
        _itemIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<SlotView>() == null)
        {
            transform.SetParent(_parent);
        }
       
        transform.localPosition = Vector3.zero;
        _itemIcon.maskable = true;
        _itemIcon.raycastTarget = true;
        UpdateAmountVisible();
    }
    public void SetupNewParent(Transform newParent)
    {
        transform.SetParent(newParent);
        _parent = newParent;
    }
  
    public void OnPointerClick(PointerEventData eventData)
    {
        if(_isClickable)
            ShowInfo();
    }

    private void ShowInfo()
    {
        PopupItem.Show(Model.ItemId);
    }
}
