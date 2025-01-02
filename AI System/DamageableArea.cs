using UnityEngine;

public class DamageableArea : MonoBehaviour
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private LayerMask canDamage;
    [SerializeField] private AudioSource explosionSound;

    [Space]
    [SerializeField] private float radius;
    [SerializeField] private float duration;
    [SerializeField] private float damageAmount;
    private Collider[] objectsInCollider;
    private bool alreadyExploded = false, hasPlayed = false;

    private enum DamageType
    {
        damageOverTime,
        splashDamage,
    }

    private void Update()
    {
        if (duration < 0 || alreadyExploded) Destroy(gameObject, 5f);
        objectsInCollider = Physics.OverlapSphere(transform.position, radius, canDamage);

        switch (damageType)
        {
            case DamageType.damageOverTime:
                DamageOverTime();
                break;
            case DamageType.splashDamage:
                SplashDamage();
                break;
        }
    }

    private void SplashDamage()
    {
        if (alreadyExploded) return;
        explosionSound.Play();

        foreach (Collider nearbyObjects in objectsInCollider)
        {
            IDamageable obj = nearbyObjects.GetComponentInChildren<IDamageable>();
            if (obj != null) obj.TakeDamage(damageAmount * 2f, 0.5f, transform.position);

            P_HealthManager player = nearbyObjects.GetComponentInChildren<P_HealthManager>();
            CameraShake shakeCamera = nearbyObjects.GetComponentInChildren<CameraShake>();
            if (player != null && shakeCamera != null)
            {
                player.TakeDamage(damageAmount / 2);
                shakeCamera.Shake(shakeCamera.explosion);
            }
        }

        alreadyExploded = true;
    }

    private void DamageOverTime()
    {
        if (!hasPlayed)
        {
            explosionSound.Play();
            hasPlayed = true;
        }

        if (duration > 0f)
        {
            foreach (Collider nearbyObjects in objectsInCollider)
            {
                IDamageable obj = nearbyObjects.GetComponentInChildren<IDamageable>();
                if (obj != null) obj.TakeDamage(damageAmount * 3f, 0f, transform.position);

                P_HealthManager player = nearbyObjects.GetComponentInChildren<P_HealthManager>();
                CameraShake shakeCamera = nearbyObjects.GetComponentInChildren<CameraShake>();
                if (player != null && shakeCamera != null)
                {
                    player.TakeDamage(damageAmount / 2);
                    shakeCamera.Shake(shakeCamera.damaged);
                }
            }

            duration -= Time.deltaTime;
        }
    }
}
