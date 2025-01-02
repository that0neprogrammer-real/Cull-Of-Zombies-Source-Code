using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsConfigurations : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead = false;

    protected virtual void Start() => maxHealth = currentHealth;

    protected virtual void GetDamaged(float damage) => 
        currentHealth = (currentHealth - damage) <= 0 ? 0 : (currentHealth - damage);
}
