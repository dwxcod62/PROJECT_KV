using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItBoxController : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockBackForce = 5000f;
    public Collider2D swordCollider;

    public Vector3 faceRight = new Vector3(0.18f, 0, 0);
    public Vector3 faceLeft = new Vector3(-0.18f, 0, 0);

    void Start()
    {
        if (swordCollider == null)
        {
            Debug.Log("Sword collider not set");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageAble damageAble = col.GetComponent<IDamageAble>();

        if (damageAble != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(col.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockBackValue = direction * knockBackForce;

            // col.SendMessage("OnHit", swordDamage, knockBackValue);
            damageAble.OnHit(swordDamage, knockBackValue);
        }
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
}
