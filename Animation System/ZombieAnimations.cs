using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    public Transform eyeball;
    [SerializeField] private Animator animator;
    [SerializeField] private ZombieNavigation state;
    private ZombieSoundHandler sound;

    private int currentAttackIndex = -1, lastAttackIndex = -1;
    private bool hasAttacked= false;

    private void Awake()
    {
        state = GetComponentInParent<ZombieNavigation>();
        animator = GetComponent<Animator>();
        sound = GetComponent<ZombieSoundHandler>();
    }

    private void Start()
    {
        currentAttackIndex = Random.Range(0, 4);
        animator.SetInteger("AttackIndex", currentAttackIndex);

        animator.SetInteger("IdleIndex", Random.Range(0, 5));
        animator.SetInteger("WalkIndex", Random.Range(0, 5));
        animator.SetInteger("ChaseIndex", Random.Range(0, 5));


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
        }
    }

    private void HandleRoaming()
    {
        animator.SetBool("GoToChase", false);

        if (!state.isMoving)
        {
            animator.SetBool("GoToWalk", false);
            animator.SetBool("GoToIdle", true);
        }
        else
        {
            animator.SetBool("GoToWalk", true);
            animator.SetBool("GoToIdle", false);
        }
    }

    private void HandleChasing()
    {
        hasAttacked = false;
        animator.SetBool("GoToChase", true);

        if (animator.GetBool("GoToIdle")) animator.SetBool("GoToIdle", false);
        else if (animator.GetBool("GoToWalk")) animator.SetBool("GoToWalk", false);
    }

    private void HandleAttacking()
    {
        if (!hasAttacked)
        {
            animator.SetTrigger("GoToAttack");
            hasAttacked = true;
        }

        animator.SetBool("GoToIdle", false);
        animator.SetBool("GoToWalk", false);
        animator.SetBool("GoToChase", false);
    }

    private void OnAttackAction()
    {
        if (state.canAttack)
        {
            if (state.attackCoroutine != null)
                StopCoroutine(state.attackCoroutine);

            sound.PlayAudio(sound.attack, sound.talk);
            if (state.atDoorRange) sound.PlayDoor();
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
