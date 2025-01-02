using System.Collections;
using UnityEngine;

public class ZombieNavigation : ZombieConfigurations
{
    [SerializeField] [Space] private float idleTimer = 0f;
    [SerializeField] private float attackRange;
    [HideInInspector] public bool canAttack = true;
    public Coroutine attackCoroutine = null;
    public bool isMoving = false;
    public bool atDoorRange => inRangeDoor;

    private void Start()
    {
        idleTimer = Random.Range(data.idleTime.x, data.idleTime.y);
        agent.speed = data.normalSpeed;
        currentState = CurrentState.Roaming;

        SetRange();
    }

    private void Update()
    {
        if (!agent.enabled) return;
        if (hordeMode) playerDirection = player.transform.parent.position;
        currentPosition = transform.position;
        
        currentState = hordeMode ? HordeBehavior() : NormalBehavior();
        switch (currentState)
        {
            case CurrentState.Roaming:
                Roam();
                break;
            case CurrentState.Chasing:
                Chase();
                break;
            case CurrentState.Attacking:
                Stop();
                break;
            case CurrentState.Dead:
                Stop();
                break;
        }

        isMoving = agent.velocity.magnitude > 0f;
    }

    private void Roam()
    {
        if (isMoving) return;
        agent.speed = data.normalSpeed;

        if (idleTimer > 0f)
        {
            if (HasReachedDestination()) agent.ResetPath();
            idleTimer -= Time.deltaTime;
        }
        else
        {
            idleTimer = Random.Range(data.idleTime.x, data.idleTime.y);
            Move(GenerateRandomPosition());
        }
    }

    private void Chase()
    {
        if (!canAttack) return;
        agent.speed = data.chasingSpeed;

        Move(playerDirection);
    }

    public bool IsRoaming() => agent.velocity.magnitude > 0.05f && currentState == CurrentState.Roaming;
    public bool IsChasing() => agent.velocity.magnitude > 0.05f && currentState == CurrentState.Chasing;

    public void EntityDied()
    {
        currentState = CurrentState.Dead;
        col.enabled = false;
    }

    public IEnumerator StartAttack()
    {
        canAttack = false;
        yield return null;

        if (inRange)
        {
            CameraShake shake = player.transform.GetComponent<CameraShake>();
            player.TakeDamage(data.damage);
            shake.Shake(shake.damaged);

        }
        else if (inRangeDoor) obstacle.transform.GetComponent<Door>().TakeDamage(data.damage * 4);

        attackCoroutine = null;
        canAttack = true;
    }

    public CurrentState CheckState() => currentState;

    public void ActivateHorde() => hordeMode = true;

    public void SetRange() => range = attackRange;
}
