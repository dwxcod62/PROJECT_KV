using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRangeAttack : MonoBehaviour
{
    [SerializeField] private bool isRanged = false;
    [SerializeField] private GameObject MeleeObject = null;


    private string tagTarget = "Player";
    public bool playerInHitRange = false;
    private bool facingRight;

    private SpriteRenderer spriteRenderer;
    private IBossCharacter fatherObject;


    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        fatherObject = GetComponentInParent<IBossCharacter>();
    }

    void FixedUpdate()
    {
        if (isRanged)
        {
            HandleRanged();
        }
        else
        {
            HandleMelee();
        }
    }


    void HandleRanged()
    {
        bool Melee_PlayerInHitRange = MeleeObject.GetComponent<HandleRangeAttack>().playerInHitRange;


        if (playerInHitRange && !Melee_PlayerInHitRange)
        {
            fatherObject.Attack(2);
        }

    }

    void HandleMelee()
    {
        if (playerInHitRange)
        {
            fatherObject.Attack(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
            fatherObject.SetLocalTarget(collider.gameObject);
            GetComponentInParent<HandleMoving>().setTargetInZone(true);
            IDamageAble damageAble = collider.GetComponent<IDamageAble>();

            if (damageAble.Health > 0)
            {
                playerInHitRange = true;

            }
            else if (damageAble.Health <= 0)
            {
                playerInHitRange = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.tag == tagTarget)
        {
            var temp = GetComponentInParent<HandleMoving>();
            if (temp != null)
            {
                GetComponentInParent<HandleMoving>().setTargetInZone(false);

            }
            fatherObject.SetLocalTarget(null);
            playerInHitRange = false;
        }
    }

}
