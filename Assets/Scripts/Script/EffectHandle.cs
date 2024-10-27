using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EffectHandle : MonoBehaviour, IEffect
{

    [SerializeField] private float interval = 2f;
    [SerializeField] private float durationPoison = 15f;
    [SerializeField] private float durationShocked = 3f;

    [SerializeField] private Sprite shocked;
    [SerializeField] private Sprite poison;
    [SerializeField] private Sprite petrify;

    private Animator animator;

    private bool isPoisoned = false;
    private bool isShocked = false;
    private bool isPetrified = false;

    private IDamageAble damageAble;
    private GUIEffectIcon gUIEffectIcon;

    void Start()
    {
        animator = GetComponent<Animator>();
        damageAble = GetComponent<IDamageAble>();
        gUIEffectIcon = FindAnyObjectByType<GUIEffectIcon>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ApplyPetrifyEffect();
        }
    }

    public void ApplyPetrifyEffect()
    {
        if (!isPetrified)
        {
            StartCoroutine(PetrifyCoroutine());
        }
    }

    private IEnumerator PetrifyCoroutine()
    {
        PlayerInput pi = GetComponent<PlayerInput>();
        isPetrified = true;

        animator.speed = 0;
        pi.enabled = false;
        if (gUIEffectIcon != null)
            gUIEffectIcon.setEffectIcon(petrify);

        yield return new WaitForSeconds(3f);

        if (gUIEffectIcon != null)
            gUIEffectIcon.RemoveIconBySprite(petrify);

        animator.speed = 1;
        pi.enabled = true;

        isPetrified = false;
    }

    public void ApplyPoisonEffect()
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            StartCoroutine(PoisonRoutine());
        }
    }

    private IEnumerator PoisonRoutine()
    {
        damageAble = GetComponent<IDamageAble>();
        float elapsedTime = 0f;

        if (gUIEffectIcon != null)
            gUIEffectIcon.setEffectIcon(poison);

        while (elapsedTime < durationPoison)
        {
            if (damageAble != null)
            {
                damageAble.OnHit(1);
            }
            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }
        if (gUIEffectIcon != null)
            gUIEffectIcon.RemoveIconBySprite(poison);

        isPoisoned = false;
    }

    public void ApplyShockEffect()
    {
        if (!isShocked)
        {
            isShocked = true;
            StartCoroutine(ShockRoutine());
        }
    }

    private IEnumerator ShockRoutine()
    {
        Character character = GetComponent<Character>();
        character.LockAttack();
        if (gUIEffectIcon != null)
            gUIEffectIcon.setEffectIcon(shocked);

        yield return new WaitForSeconds(5f);

        if (gUIEffectIcon != null)
            gUIEffectIcon.RemoveIconBySprite(shocked);


        character.UnLockAttack();
        isShocked = false;
    }

    public void RemoveEffect()
    {

    }

}
