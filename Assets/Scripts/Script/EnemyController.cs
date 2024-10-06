using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed = 500f;
    public float obstacleDetectionRange = 1f;
    public LayerMask obstacleLayer;
    public DetectionZone detectionZone;

    Animator an;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public event Action<GameObject> OnEnemyDestroyed;

    void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {

        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            if (!IsObstacleInDirection(direction))
            {
                rb.AddForce(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                Vector2 newDirection = FindAlternativeDirection(direction);
                rb.AddForce(newDirection * moveSpeed * Time.deltaTime);
            }

            //  Check direction 
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }

            an.SetBool("isMoving", true);
        }
        else
        {
            an.SetBool("isMoving", false);
        }
    }

    bool IsObstacleInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionRange, obstacleLayer);
        return hit.collider != null;
    }
    Vector2 FindAlternativeDirection(Vector2 currentDirection)
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
