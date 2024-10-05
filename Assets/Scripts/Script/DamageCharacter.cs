using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacter : MonoBehaviour, IDamageAble
{

    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;

    Collider2D physicCollider;

    bool IsAlive = true;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physicCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsAlive", IsAlive);
    }

    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
            }

            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("IsAlive", false);
                TargetAble = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public bool TargetAble
    {
        get { return _targetAble; }
        set
        {
            _targetAble = value;

            if (disableSimulation)
            {
                rb.simulated = false;
            }

            physicCollider.enabled = value;

        }
    }

    public float _health = 100f;
    public bool _targetAble = true;



    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockValue)
    {
        Health -= damage;
        rb.AddForce(knockValue);

    }

    void IDamageAble.OnHit(float damage)
    {
        Health -= damage;
    }
}