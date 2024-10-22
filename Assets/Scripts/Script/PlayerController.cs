using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components and Input
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Movement and Control Variables
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float maxSpeed = 2.5f;
    [SerializeField] private int weapon = 1;
    [SerializeField] private UIInventory uIInventory;

    private bool isMoving = false;
    private bool canMove = true;

    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (canMove && movementInput != Vector2.zero)
        {
            // Apply movement with speed limit
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

            // Handle character flipping and facing direction
            UpdateFacingDirection();

            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    private void UpdateFacingDirection()
    {
        if (movementInput.x < 0)
        {
            FaceRight(false);
        }
        else if (movementInput.x > 0)
        {
            FaceRight(true);
        }
    }


    void LockMovement()
    {
        canMove = false;
    }

    void UnLockMovement()
    {
        canMove = true;
    }

    private void UpdateFacingDirectionByMouse(Vector3 mousePos)
    {
        if (mousePos.x > transform.position.x)
        {
            FaceRight(true);
        }
        else
        {
            FaceRight(false);
        }
    }

    private void FaceRight(bool isRight)
    {
        spriteRenderer.flipX = !isRight;
        gameObject.BroadcastMessage("IsFacingRight", isRight);
    }

    // PLAYER INPUT
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("isAttack");

        // Determine mouse position relative to player
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateFacingDirectionByMouse(mousePos);
    }

    void OnChangeWeapon()
    {
        weapon = weapon == 0 ? 1 : 0;
        animator.SetInteger("idWeapon", weapon);
    }

    // LOOTING
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item")
        {
            // col.gameObject.GetComponent<ItemPrefab>().test();
        }
    }
}
