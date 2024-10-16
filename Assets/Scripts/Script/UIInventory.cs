using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private int inventorySize = 9;
    [SerializeField] private Sprite ImageItem;

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
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
