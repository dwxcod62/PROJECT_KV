using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] UIInventory inventoryUI;

    void Start()
    {
        inventoryUI.InitializeInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                Time.timeScale = 0f;
                inventoryUI.Show();
            }
            else
            {
                Time.timeScale = 1f;
                inventoryUI.Hide();
            }
        }
    }
}
