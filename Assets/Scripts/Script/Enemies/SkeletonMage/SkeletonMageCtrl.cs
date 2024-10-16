using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageCtrl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private float obstacleDetectionRange = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private DetectionZone detectionZone;
    [SerializeField] private SkeletonMageHit skeletonMageHit;
    [SerializeField] private GameObject spellPrefab;

    private GameObject localTarget = null;

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

        bool playerInHitRange = skeletonMageHit.playerInHitRange;
        GameObject target = detectionZone.detectedObjs != null ? detectionZone.detectedObjs.gameObject : null;
        localTarget = target;

        if (target != null && !playerInHitRange)
        {
            HandleMovement(target.transform.position, true);
        }
        else if (target != null && playerInHitRange)
        {
            HandleMovement(target.transform.position, false);
            an.SetTrigger("Attack");
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
        an.SetBool("isMoving", true);
    }

    private void MoveInDirection(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    private void FlipSprite(Vector2 direction)
    {
        spriteRenderer.flipX = direction.x < 0;
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
        Destroy(gameObject);
    }

    public void AttackCastSpell()
    {
        if (localTarget != null)
        {
            Vector3 targetPosition = localTarget.transform.position;
            GameObject SpellFireBall = Instantiate(spellPrefab, targetPosition, Quaternion.identity);
        }
    }

}
