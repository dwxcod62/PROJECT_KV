using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    private string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagTarget)
        {
            IDamageAble damageAble = collider.GetComponent<IDamageAble>();

            if (damageAble.Health > 0)
            {
                detectedObjs.Add(collider);
            }
            else if (damageAble.Health <= 0 && detectedObjs.Count > 0)
            {
                detectedObjs.Remove(collider);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == tagTarget && detectedObjs.Count > 0)
        {
            detectedObjs.Remove(collider);
        }
    }
}
