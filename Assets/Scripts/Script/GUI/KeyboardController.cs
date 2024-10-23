using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private UIInventory inventoryUI;
    [SerializeField] private GameObject player;

    private PlayerInput playerInput;
    void Start()
    {
        inventoryUI.InitializeInventoryUI();
        playerInput = player.GetComponent<PlayerInput>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                Time.timeScale = 0f;
                inventoryUI.Show();
                playerInput.enabled = false;
            }
            else
            {
                Time.timeScale = 1f;
                inventoryUI.Hide();
                playerInput.enabled = true;
            }
        }
    }
}
