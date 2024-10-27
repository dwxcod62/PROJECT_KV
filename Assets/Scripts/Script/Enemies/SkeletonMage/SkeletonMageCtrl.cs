using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageCtrl : MonoBehaviour, IBossCharacter
{
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private float obstacleDetectionRange = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private DetectionZone detectionZone;
    [SerializeField] private SkeletonMageHit skeletonMageHit;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject firstCastPrefab;

    [SerializeField] private GameObject summonCastPrefab;
    private GameObject localTarget = null;
    private bool canAttack = true;
    private bool facingRight = true;

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
            if (canAttack)
            {
                int skill = RandomSkill();
                Attack(skill);
            }
        }
    }

    public void Attack(int skill)
    {
        an.SetTrigger("Attack");
        an.SetInteger("idSkill", skill);
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
        facingRight = direction.x > 0;
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

    public void LockAttack() => canAttack = false;
    public void UnLockAttack() => canAttack = true;

    private int RandomSkill()
    {
        int random = Random.Range(1, 101);
        return random <= 60 ? 1 : (random <= 90 ? 2 : 3); // 50% for 1, 25% for 2, 25% for 3
    }

    // SKILL SET
    public void CastThirdSkill()
    {
        Vector3 currentPosition = transform.position;

        List<Vector3> surroundingPositions = new List<Vector3>();

        surroundingPositions.Add(new Vector3(currentPosition.x - 0.3f, currentPosition.y, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x + 0.3f, currentPosition.y, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x, currentPosition.y - 0.3f, currentPosition.z));

        foreach (Vector3 position in surroundingPositions)
        {
            Instantiate(summonCastPrefab, position, Quaternion.identity);
        }

    }

    public void CastFirstSkill()
    {
        // For BossTestScene
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);
        // End For BossTestScene

        if (localTarget != null)
        {
            targetPosition = localTarget.transform.localPosition;
        }

        Vector3 screenPosition = gameObject.transform.position;
        float offsetWidth = spriteRenderer.bounds.size.x / 2 * (facingRight ? 1 : -1);
        Vector3 newPosition = new Vector3(screenPosition.x + offsetWidth, screenPosition.y, screenPosition.z);


        targetPosition.z = 0f;  // Set z to 0 because we're working in 2D

        // Determine the number of projectiles to spawn
        int projectileCount = Random.Range(1, 10); // 50% chance for 3 projectiles

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the direction for each projectile (spread them slightly)
            Vector3 direction = (targetPosition - newPosition).normalized;

            // Instantiate the bullet prefab
            var bullet = Instantiate(firstCastPrefab, newPosition, Quaternion.identity);

            // Calculate the angle in degrees for rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the bullet's rotation
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            float bulletSpeed = 1.5f;

            // Add a slight spread for the projectiles if spawning multiple
            if (projectileCount > 3)
            {
                // Adjust direction for each projectile in the spread
                float spreadAngle = 10f; // Spread angle in degrees
                angle += (i - 1) * spreadAngle; // Adjust angles to create spread effect
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Set the new angle

                // Recalculate the direction with the new angle
                direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            }

            // Set the velocity for the bullet
            bulletRb.velocity = direction * bulletSpeed;
        }
    }

    public void CastSecondSkill()
    {
        // For BossTestScene
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);
        // End For BossTestScene

        if (localTarget != null)
        {
            targetPosition = localTarget.transform.position;
        }
        GameObject SpellFireBall = Instantiate(spellPrefab, targetPosition, Quaternion.identity);

    }


    // Debug
    public void SetMoveAnimation()
    {
        bool a = an.GetBool("isMoving");
        an.SetBool("isMoving", !a);
    }

    public void SetLocalTarget(GameObject target)
    {
        localTarget = target;
    }


}
