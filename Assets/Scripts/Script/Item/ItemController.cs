using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private readonly string TagTarget = "Player";

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagTarget))
        {
            print("Player take");
        }
    }
}
