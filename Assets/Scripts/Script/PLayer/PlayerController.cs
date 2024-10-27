using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Character
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
    [SerializeField] private BulletController bulletPrefab;

    private bool canAttack = true;
    private bool isMoving = false;
    private bool canMove = true;
    public bool facingRight = true;



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
        if (canMove)
        {
            HandleMovement();
        }
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
        facingRight = isRight;
    }

    // Lock and Unlock Movement
    public void LockMovement() => canMove = false;
    public void UnLockMovement() => canMove = true;


    // PLAYER INPUT
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (canAttack)
        {
            animator.SetTrigger("isAttack");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateFacingDirectionByMouse(mousePos);

            if (weapon == 2)
                ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Vector3 screenPosition = gameObject.transform.position;

        // Offset calculation for bullet spawning based on facing direction
        float offsetWidth = spriteRenderer.bounds.size.x / 4 * (facingRight ? 1 : -1);
        Vector3 newPosition = new Vector3(screenPosition.x + offsetWidth, screenPosition.y, screenPosition.z);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Set z to 0 because we're working in 2D

        Vector3 direction = (mousePosition - newPosition).normalized;

        var bullet = Instantiate(bulletPrefab, newPosition, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        float bulletSpeed = 1.5f;
        bulletRb.velocity = direction * bulletSpeed;
    }

    void OnChangeWeapon()
    {
        weapon = (weapon + 1) % 3;
        animator.SetInteger("idWeapon", weapon);
    }

    // LOOTING
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            uIInventory.AddItem(col.gameObject);
        }
    }

    public void LockAttack() => canAttack = false;
    public void UnLockAttack() => canAttack = true;

}
