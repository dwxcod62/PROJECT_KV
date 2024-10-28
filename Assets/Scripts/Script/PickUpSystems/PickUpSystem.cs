using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D col)
    {
        ItemController item = col.GetComponent<ItemController>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item, 1);
            if (reminder == 0)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
