using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : MonoBehaviour
{
    private string targetTag = "Player";
    [SerializeField] private float damageAttack = 1f;

    public Vector3 faceRight = new Vector3(0.14f, -0.01f, 0);
    public Vector3 faceLeft = new Vector3(-0.14f, -0.01f, 0);

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageAble damageAble = col.GetComponent<IDamageAble>();

        if (damageAble != null && col.CompareTag(targetTag))
        {
            damageAble.OnHit(damageAttack);
        }
    }

    public void IsFacingRight(bool isRight)
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
