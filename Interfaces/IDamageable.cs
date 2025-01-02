using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount, float shootingForce, Vector3 direction);
}
