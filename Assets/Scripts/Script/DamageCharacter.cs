using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCharacter : MonoBehaviour, IDamageAble
{

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private bool _targetAble = true;

    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;

    Collider2D physicCollider;

    bool IsAlive = true;
    public Image healthBarFill = null;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physicCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsAlive", IsAlive);

        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = _health / _maxHealth;
        }
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
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = _health / _maxHealth;
            }
            else
            {
                Debug.LogWarning("Health bar fill image is not assigned.");
            }

            if (_health <= 0)
            {
                animator.SetBool("IsAlive", false);
                animator.SetTrigger("Death");
                TargetAble = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
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

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockValue)
    {
        Health -= damage;
        rb.AddForce(knockValue);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
}