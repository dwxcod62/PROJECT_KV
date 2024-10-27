using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    private string tagTarget = "Player";
    public Collider2D detectedObjs = null;
    public Collider2D col;
    public bool playerIn = false;

    void Start()
    {
        detectedObjs = null;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
            IDamageAble damageAble = collider.GetComponent<IDamageAble>();

            if (damageAble.Health > 0)
            {
                detectedObjs = collider;
                playerIn = true;
            }
            else if (damageAble.Health <= 0 && detectedObjs != null)
            {
                detectedObjs = null;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == tagTarget && detectedObjs != null)
        {
            detectedObjs = null;
            playerIn = false;
        }
    }
}
