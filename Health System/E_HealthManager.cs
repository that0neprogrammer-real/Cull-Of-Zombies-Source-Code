using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class E_HealthManager : StatsConfigurations
{
    [Space]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private bool destroyParentAfterDeath = true;
    private ZombieNavigation zombie;
    private ZombieSoundHandler sound;
    private Transform hitBoxRoot;
    private Rigidbody[] rigidbodies;
    private Rigidbody rbShot;

    private Vector3 forceDirection = Vector3.zero;
    private float forceApplied = 0f;
    private bool deathByExplosion = false;

    private void Awake()
    {
        hitBoxRoot = transform.GetComponent<Transform>();
        animator = GetComponentInParent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        sound = GetComponent<ZombieSoundHandler>();
        zombie = GetComponentInParent<ZombieNavigation>();
    }

    protected override void Start()
    {
        base.Start();

        rigidbodies = hitBoxRoot.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }

    private void Update()
    {
        if (currentHealth <= 0 && !isDead && !deathByExplosion)
        {
            StartCoroutine(Death());
            isDead = true;
        }
    }

    private void EnableRagdoll(bool isRagdoll)
    {
        agent.enabled = !isRagdoll;

        foreach (Rigidbody rb in rigidbodies) rb.isKinematic = !isRagdoll;
        animator.enabled = !isRagdoll;
    }

    public void ReceiveDamage(float amount, float force, Vector3 direction)
    {
        forceApplied = force;
        forceDirection = direction;

        if (sound.AudioPlaying()) sound.StopAudio();
        sound.PlayAudio(sound.hit, sound.talk);
        base.GetDamaged(amount);
    }

    public void SetCurrentRigidbody(Rigidbody current) => rbShot = current;

    private IEnumerator Death()
    {
        EnableRagdoll(true);
        zombie.EntityDied();
        float randomY = Random.Range(0.5f, 3f);
        forceDirection.y = randomY;

        yield return new WaitForFixedUpdate();
        rbShot.AddForce(forceDirection * forceApplied, ForceMode.VelocityChange);

        sound.PlayAudio(sound.death, sound.talk);
        sound.enabled = false;

        GameObject destroy = destroyParentAfterDeath ? transform.parent.gameObject : transform.gameObject;
        Destroy(destroy, 3f);
    }
}
