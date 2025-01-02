using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldZombieAnimations : MonoBehaviour
{
    [SerializeField] private ZombieNavigation state;
    [SerializeField] private Animator animator;

    private int currentAttackIndex = -1, lastAttackIndex = -1;
    private bool hasDied = false;

    private void Start()
    {
        state = transform.parent.GetComponent<ZombieNavigation>();
        animator = GetComponent<Animator>();


        animator.SetInteger("ChaseIndex", Random.Range(0, 3));
        animator.SetInteger("IdleIndex", Random.Range(0, 3));

        currentAttackIndex = Random.Range(0, 3);
        animator.SetInteger("AttackIndex", currentAttackIndex);

        animator.SetTrigger("Start");
    }

    private void Update()
    {
        switch (state.CheckState())
        {
            case ZombieConfigurations.CurrentState.Roaming:
                HandleRoaming();
                break;
            case ZombieConfigurations.CurrentState.Chasing:
                HandleChasing();
                break;
            case ZombieConfigurations.CurrentState.Attacking:
                HandleAttacking();
                break;
            case ZombieConfigurations.CurrentState.Dead:
                HandleDeath();
                break;
        }

        transform.localPosition = Vector3.zero;
    }

    private void HandleRoaming()
    {
        animator.SetBool("GoToAttack", false);

        if (!state.isMoving)
        {
            animator.SetBool("GoToIdle", true);

            if (animator.GetBool("GoToChase"))
                animator.SetBool("GoToChase", false);
        }
        else
        {
            animator.SetBool("GoToIdle", false);

            if (!animator.GetBool("GoToChase"))
                animator.SetBool("GoToChase", true);
        }
    }

    private void HandleChasing()
    {
        if (animator.GetBool("GoToIdle") || animator.GetBool("GoToAttack"))
        {
            if (animator.GetBool("GoToIdle")) animator.SetBool("GoToIdle", false);
            else if (animator.GetBool("GoToAttack")) animator.SetBool("GoToAttack", false);
        }

        animator.SetBool("GoToChase", true);
    }

    private void HandleAttacking()
    {
        if (animator.GetBool("GoToChase"))
            animator.SetBool("GoToChase", false);

        animator.SetBool("GoToAttack", true);
    }

    private void HandleDeath()
    {
        if (!hasDied)
        {
            Destroy(transform.parent.gameObject, 5f);
            animator.SetTrigger("HasDied");
            hasDied = true;
        }
    }

    #region //Attack Animations
    private void OnAttackAction()
    {
        if (state.canAttack)
        {
            if (state.attackCoroutine != null)
                StopCoroutine(state.attackCoroutine);

            state.attackCoroutine = StartCoroutine(state.StartAttack());
        }

        while (currentAttackIndex == lastAttackIndex) currentAttackIndex = Random.Range(0, 3);
    }

    private void OnAttackFinish()
    {
        animator.SetTrigger("Attack" + currentAttackIndex);
        lastAttackIndex = currentAttackIndex;
    }
    #endregion
}
