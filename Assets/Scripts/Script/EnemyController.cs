using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{

    public float moveSpeed = 500f;
    public float obstacleDetectionRange = 1f;
    public LayerMask obstacleLayer;
    public DetectionZone detectionZone;

    Animator an;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public Image attentionIcon;
    public event Action<GameObject> OnEnemyDestroyed;

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
            HandleMovement(target.transform.position);
        }
    }

    private void HandleMovement(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (!IsObstacleInDirection(direction))
        {
            MoveInDirection(direction);
        }
        else
        {
            Vector2 newDirection = FindAlternativeDirection(direction);
            MoveInDirection(newDirection);
        }

        FlipSprite(direction);
        an.SetBool("isMoving", true);
    }

    private void MoveInDirection(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    private void FlipSprite(Vector2 direction)
    {
        bool isFacingRight = direction.x >= 0;
        spriteRenderer.flipX = !isFacingRight;
        gameObject.BroadcastMessage("IsFacingRight", isFacingRight);
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

    public void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.localPosition);

        Destroy(gameObject);
    }

    public void hitPlayerCheck()
    {
        gameObject.BroadcastMessage("CheckHitPlayer");
    }

    private void OnDestroy()
    {
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

}
