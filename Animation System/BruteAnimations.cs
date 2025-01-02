using UnityEngine;

public class BruteAnimations : MonoBehaviour
{
    public Transform eyeball;
    [SerializeField] private Animator animator;
    [SerializeField] private ZombieNavigation state;
    private ZombieSoundHandler sound;

    private int currentAttackIndex = -1, lastAttackIndex = -1;
    private bool hasAttacked = false;

    private void Awake()
    {
        state = GetComponent<ZombieNavigation>();
        animator = GetComponent<Animator>();
        sound = GetComponent<ZombieSoundHandler>();
    }

    private void Start()
    {
        currentAttackIndex = Random.Range(0, 4);
        animator.SetInteger("AttackIndex", currentAttackIndex);
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
        }
    }

    private void HandleRoaming()
    {
        if (!state.isMoving)
        {
            animator.SetBool("GoToRun", false);
        }
        else
        {
            animator.SetBool("GoToRun", true);
        }
    }

    private void HandleChasing()
    {
        hasAttacked = false;
        animator.SetBool("GoToRun", true);
    }

    private void HandleAttacking()
    {
        if (!hasAttacked)
        {
            animator.SetTrigger("GoToAttack");
            hasAttacked = true;
        }

        animator.SetBool("GoToRun", false);
    }

    private void OnAttackAction()
    {
        if (state.canAttack)
        {
            if (state.attackCoroutine != null)
                StopCoroutine(state.attackCoroutine);

            sound.PlayAudio(sound.attack, sound.talk);
            state.attackCoroutine = StartCoroutine(state.StartAttack());
        }

        do { currentAttackIndex = Random.Range(0, 5); } while (currentAttackIndex == lastAttackIndex);
        animator.SetInteger("AttackIndex", currentAttackIndex);
    }

    private void OnAttackFinish()
    {
        lastAttackIndex = currentAttackIndex;
        animator.SetTrigger("GoToAttack");
    }
}
