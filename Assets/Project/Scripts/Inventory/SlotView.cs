using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotView : MonoBehaviour,
        IDropHandler
{
    [SerializeField] private Image _image;


    [SerializeField] private ItemView _prefab;

    private ItemView _currentItemInSlot;
    private InventorySlot Model;

    public void Setup(InventorySlot model)
    {
        Model = model;
        Model.Added = OnAdded;
        Model.Removed = OnRemoved;
        Model.Taked = OnTaked;
    }

    private void OnTaked()
    {
        _currentItemInSlot = null;
    }

    public void OnAdded(int currentAmount)
    {
        if (_currentItemInSlot == null)
            _currentItemInSlot = Instantiate(_prefab, transform);
        _currentItemInSlot.Setup(in Model.CurrentItem, Model);
    }
    public void OnRemoved(int currentAmount)
    {
        if (currentAmount == 0)
        {
            Destroy(_currentItemInSlot.gameObject);
        }
        else
        {
            _currentItemInSlot.UpdateValue(currentAmount);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            if (eventData.pointerDrag.TryGetComponent(out ItemView itemView) && Model.IsEmpty)
            {
                itemView.SetupNewParent(transform);

                Model.Move(itemView.Model);
                itemView.SlotModel.Take(itemView.Model.CurrentAmount);
                itemView.Setup(Model);
               _currentItemInSlot = itemView;
            }
        }
       

    }
}
