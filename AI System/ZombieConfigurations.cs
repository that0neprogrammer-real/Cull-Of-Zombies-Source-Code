using UnityEngine.AI;
using Nomnom.RaycastVisualization;
using DG.Tweening;
using UnityEngine;

public class ZombieConfigurations : MonoBehaviour
{
    protected NavMeshPath path;
    [SerializeField] protected ZombieData data;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform raycastTransform;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected CurrentState currentState;
    protected Collider col;
    protected P_HealthManager player;
    
    protected bool hordeMode = false;
    protected bool inRange = false;
    protected bool inRangeDoor = false;
    protected bool hasDied = false;

    protected float range;
    protected float separationRadius = 0.5f;
    protected float separationStrength = 0.25f;
    protected float dotProduct; //1 - entity in front, 0 - entity is behind

    protected RaycastHit hit;
    protected RaycastHit obstacle;
    protected Vector3 targetDirection = Vector3.zero;
    protected Vector3 playerDirection = Vector3.zero;
    protected Vector3 currentPosition = Vector3.zero;

    public enum CurrentState
    {
        Roaming,
        Chasing,
        Attacking,
        Dead
    }

    private void Awake()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<P_HealthManager>();
        col = GetComponent<Collider>();
    }

    protected CurrentState HordeBehavior()
    {
        if (hasDied) return CurrentState.Dead;
        transform.DOLookAt(playerDirection, 0.55f, AxisConstraint.Y); //Smooth rotation when looking at the target

        if (Physics.Raycast(currentPosition + new Vector3(0f, 1.5f, 0f), 
            transform.forward, out RaycastHit hit, 1.4f))
        {
            Door obj = hit.transform.GetComponent<Door>();
            if (obj != null && obj.objectHealth > 0)
            {
                inRangeDoor = true;
                return CurrentState.Attacking;
            }
        }

        if (DistanceFromTarget(playerDirection, currentPosition) <= range)
        {
            inRange = true;
            return CurrentState.Attacking;
        }

        inRangeDoor = false;
        inRange = false;
        return CurrentState.Chasing;
    }

    protected CurrentState NormalBehavior()
    {
        if (hasDied) return CurrentState.Dead;

        Collider[] playerInRadius = Physics.OverlapSphere(currentPosition, data.viewRadius, playerLayer);
        foreach (Collider collider in playerInRadius)
        {
            playerDirection = collider.transform.position;
            targetDirection = playerDirection - currentPosition;
            dotProduct = Vector3.Dot(targetDirection.normalized, transform.forward);

            if (VisualPhysics.Raycast(raycastTransform.position, // => Adjust to head level height for better detection
               targetDirection, out hit) && dotProduct > 0f)
            {
                if (Physics.Raycast(currentPosition + new Vector3(0f, 1f, 0f), transform.forward, 
                    out obstacle, 1.5f))
                {
                    Door obj = obstacle.transform.GetComponent<Door>();
                    if (obj != null && obj.objectHealth > 0)
                    {
                        inRangeDoor = true;
                        return CurrentState.Attacking;
                    }
                }

                if (hit.collider.CompareTag("Player"))
                {
                    transform.DOLookAt(playerDirection, 0.55f, AxisConstraint.Y); //Smooth rotation when looking at the target

                    if (DistanceFromTarget(playerDirection, currentPosition) <= range)
                    {
                        inRange = true;
                        return CurrentState.Attacking;
                    }

                    inRange = false;
                    return CurrentState.Chasing;
                }
            }
        }


        inRange = false;
        inRangeDoor = false;
        return CurrentState.Roaming;
    }

    protected Vector3 GenerateRandomPosition()
    {
        Vector3 randomPointInSphere;
        NavMeshHit hit;

        do
        {

            randomPointInSphere = Random.insideUnitSphere * data.roamingRadius;
            randomPointInSphere += transform.position;
        } while (!NavMesh.SamplePosition(randomPointInSphere, out hit, data.roamingRadius, NavMesh.AllAreas));

        return hit.position;
    }

    protected void Move(Vector3 target)
    {
        agent.SetDestination(target);
        agent.isStopped = false;
    }

    protected void Stop() => agent.isStopped = true;

    protected float DistanceFromTarget(Vector3 pos1, Vector3 pos2) => Vector3.Distance(pos1, pos2);

    protected bool HasReachedDestination() => agent.remainingDistance <= agent.stoppingDistance;
}
