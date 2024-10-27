using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleMoving : MonoBehaviour
{
    [SerializeField] private float obstacleDetectionRange = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private DetectionZone detectionZone;

    [SerializeField] private bool allowMove;

    public bool facingRight = true;
    private bool targetInZone = false;


    Animator an;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        GameObject target = detectionZone.detectedObjs != null ? detectionZone.detectedObjs.gameObject : null;
        if (target != null)
        {
            HandleMovement(target.transform.position, (allowMove && !targetInZone));
        }
    }

    private void HandleMovement(Vector2 targetPosition, bool move)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (IsObstacleInDirection(direction))
        {
            Vector2 newDirection = FindAlternativeDirection(direction);
            direction = newDirection;
        }

        if (move)
            MoveInDirection(direction);

        FlipSprite(direction);

        an.SetBool("isMoving", move);
    }

    private bool IsObstacleInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionRange, obstacleLayer);
        return hit.collider != null;
    }
    private Vector2 FindAlternativeDirection(Vector2 currentDirection)
    {
        float randomAngle = -90f;

        Vector2 newDirection = Quaternion.Euler(0, 0, randomAngle) * currentDirection;

        return newDirection.normalized;
    }

    private void FlipSprite(Vector2 direction)
    {
        spriteRenderer.flipX = direction.x < 0;
        facingRight = direction.x > 0;
        gameObject.GetComponentInChildren<HitBoxEnemy>().IsFacingRight(facingRight);
    }

    private void MoveInDirection(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    public bool checkIsFacingRight()
    {
        return facingRight;
    }

    public void setTargetInZone(bool targetInZone)
    {
        this.targetInZone = targetInZone;
    }
}
