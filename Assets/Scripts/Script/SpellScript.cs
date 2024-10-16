using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StopAnimationFor1Seconds()
    {
        StartCoroutine(StopAnimationCoroutine());
    }

    IEnumerator StopAnimationCoroutine()
    {
        animator.speed = 0;
        yield return new WaitForSeconds(0.5f);
        animator.speed = 1;
    }

}
