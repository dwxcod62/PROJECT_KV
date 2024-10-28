using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        private string _itemName;
        private int _quantity;

        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image borderImage;

        public event Action<UIInventoryItem> OnItemClicked;
        public event Action<UIInventoryItem> OnItemDroppedOn;
        public event Action<UIInventoryItem> OnItemBeginDrag;
        public event Action<UIInventoryItem> OnItemEndDrag;
        public event Action<UIInventoryItem> OnItemMouseBtnClick;

        private bool _isEmpty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity, string itemName)
        {
            _itemName = itemName;
            _quantity = quantity;

            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = _quantity.ToString();
            _isEmpty = false;
        }

        public bool CheckItemAlready(string itemName)
        {
            return _itemName == itemName;
        }

        public void AddQuantity(int quantity)
        {
            _quantity += quantity;
            quantityTxt.text = _quantity.ToString();
        }

        public bool IsEmpty() => _isEmpty;

        public void Select()
        {
            borderImage.enabled = true;
        }

        public void OnBeginDrag()
        {
            if (_isEmpty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(BaseEventData data)
        {
            if (_isEmpty) return;

            var pointerData = (PointerEventData)data;

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnItemMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }
    }

}

