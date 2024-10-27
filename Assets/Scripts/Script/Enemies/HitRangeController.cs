using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRangeController : MonoBehaviour
{
    public float damage = 1f;

    private string tagTarget = "Player";
    public Vector3 faceRight = new Vector3(0.15f, 0, 0);
    public Vector3 faceLeft = new Vector3(-0.15f, 0, 0);

    public Collider2D localCollider = null;

    Animator an;
    void Start()
    {
        an = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
            IDamageAble damageAble = collider.GetComponent<IDamageAble>();
            if (damageAble != null)
            {
                an.SetBool("Attacking", true);
            }

            localCollider = collider;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
            localCollider = null;
        }
        an.SetBool("Attacking", false);
    }

    void IsFacingRight(bool isRight)
    {
        if (isRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }

    public void CheckHitPlayer()
    {
        if (localCollider != null)
        {
            IDamageAble damageAble = localCollider.GetComponent<IDamageAble>();
            if (damageAble != null)
            {
                damageAble.OnHit(damage);
            }
        }
    }

}
