using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController : MonoBehaviour, IBossCharacter
{
    [SerializeField] private GameObject firstCastPrefab;
    [SerializeField] private float damageAttack = 1f;
    [SerializeField] private float poisonRate = 20f;

    Animator an;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private GameObject localTarget = null;

    void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        an.SetBool("isMoving", false);
        Attack(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(int skill)
    {
        an.SetTrigger("Attack");
        an.SetInteger("idSkill", skill);
    }

    public void CastSecondSkill()
    {
        bool facingRight = GetComponent<HandleMoving>().checkIsFacingRight();

        // Debug for 
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);

        if (localTarget != null)
        {
            targetPosition = localTarget.transform.localPosition;
        }

        Vector3 screenPosition = gameObject.transform.position;
        float offsetWidth = spriteRenderer.bounds.size.x / 2 * (facingRight ? 1 : -1);
        Vector3 newPosition = new Vector3(screenPosition.x + offsetWidth, screenPosition.y, screenPosition.z);

        targetPosition.z = 0f;  // Set z to 0 because we're working in 2D

        // Calculate the direction for the projectile
        Vector3 direction = (targetPosition - newPosition).normalized;

        // Instantiate the bullet prefab
        var bullet = Instantiate(firstCastPrefab, newPosition, Quaternion.identity);
        bullet.GetComponent<BulletController>().activeEffect += applyEffectAttack;

        // Calculate the angle in degrees for rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the bullet's rotation
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float bulletSpeed = 1.5f;

        // Set the velocity for the bullet
        bulletRb.velocity = direction * bulletSpeed;
    }

    public void CastFirstSkill()
    {

    }

    public void CastThirdSkill()
    {
        Vector3 currentPosition = transform.position;

        Vector2 pointA = new Vector2(currentPosition.x + 10, currentPosition.y + 10);
        Vector2 pointB = new Vector2(currentPosition.x, currentPosition.y - 10);

        Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA, pointB);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                bool isEyeContact = GetComponent<HandleMoving>().facingRight != collider.GetComponent<PlayerController>().facingRight;

                if (isEyeContact)
                {
                    collider.GetComponent<IEffect>().ApplyPetrifyEffect();
                }
            }
        }

    }

    public void SetMoveAnimation()
    {
        bool a = an.GetBool("isMoving");
        an.SetBool("isMoving", !a);
    }

    public void SetLocalTarget(GameObject target)
    {
        localTarget = target;
    }

    public void applyEffectAttack(Collider2D col)
    {
        int chance = Random.Range(0, 100);

        if (chance < poisonRate)
        {
            col.GetComponent<IEffect>().ApplyPoisonEffect();
        }
    }

}
