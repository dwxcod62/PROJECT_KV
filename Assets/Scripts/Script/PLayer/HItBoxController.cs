using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItBoxController : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float swordDamage = 1f;
    [SerializeField] private float knockBackForce = 5000f;
    [SerializeField] private Collider2D swordCollider;

    [SerializeField] private bool disappearOnCollision = false;


    public Vector3 faceRight = new Vector3(0.18f, 0, 0);
    public Vector3 faceLeft = new Vector3(-0.18f, 0, 0);

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();


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

            damageAble.OnHit(swordDamage, knockBackValue);

            if (disappearOnCollision)
            {
                Destroy(gameObject);
            }
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
