using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageHit : MonoBehaviour
{

    private string tagTarget = "Player";
    public bool playerInHitRange = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
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
            playerInHitRange = false;
        }
    }
}
