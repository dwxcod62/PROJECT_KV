using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : MonoBehaviour
{
    public Vector3 faceRight = new Vector3(0.14f, -0.01f, 0);
    public Vector3 faceLeft = new Vector3(-0.14f, -0.01f, 0);

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageAble damageAble = col.GetComponent<IDamageAble>();

        if (damageAble != null)
        {
            damageAble.OnHit(1);
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
