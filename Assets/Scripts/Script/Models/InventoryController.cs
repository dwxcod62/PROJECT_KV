using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventory inventoryUI;

        [SerializeField] private InventorySO inventoryData;

        

        private void Start()
        {
            PrepareUI();
            inventoryData.Initialize();
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    Time.timeScale = 0f;
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.ItemName);
                    }
                }
                else
                {
                    Time.timeScale = 1f;
                    inventoryUI.Hide();
                }
            }
        }
    }
}

