using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.localPosition);
    }
}
