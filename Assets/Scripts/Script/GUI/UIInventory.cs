using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private int inventorySize = 9;
    // [SerializeField] private Sprite ImageItem;

    [SerializeField] private UIInventoryItem itemPrefab;

    [SerializeField] private RectTransform contentPanel;

    List<UIInventoryItem> listOfUIItem = new List<UIInventoryItem>();

    public void InitializeInventoryUI()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector2.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            listOfUIItem.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
    }

    private void HandleSwap(UIInventoryItem item)
    {
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log(item.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);

    }

    public void addItem(Loot item)
    {
        listOfUIItem[0].SetData(item.lootSprite, 1);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
