using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitbox : MonoBehaviour, IDamageable
{
    [SerializeField] private float multiplier = 1f;
    private E_HealthManager entity;
    private Rigidbody rb;

    private void Awake()
    {
        entity = GetComponentInParent<E_HealthManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float amount, float shootingForce, Vector3 direction)
    {
        entity.SetCurrentRigidbody(rb);
        entity.ReceiveDamage(amount * multiplier, shootingForce, direction);
    }
}
