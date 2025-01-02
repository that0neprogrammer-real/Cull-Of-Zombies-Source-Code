using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectData data;
    public float objectHealth;
    private bool hasInteracted = false;

    private Vector3 target = Vector3.zero;
    private Transform playerPosition;

    private void Start()
    {
        playerPosition = FindObjectOfType<CharacterController>().transform;
        objectHealth = data.entityHealth;
    }

    private void Update()
    {
        target = playerPosition.position - transform.parent.position;

        if (objectHealth <= 0)
        {
            transform.DOLocalRotate(new Vector3(0f, -90f, 0f), 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
            hasInteracted = true;
        }
    }

    public void TakeDamage(float damage) => objectHealth -= damage;

    public bool HasInteracted() => hasInteracted;

    public void Interact()
    {
        if (HasInteracted())
        {
            transform.DOLocalRotate(Vector3.zero, 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
            hasInteracted = false;
        }
        else
        {
            if (Vector3.Dot(target.normalized, transform.parent.forward) > 0f)
            {
                transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
                hasInteracted = true;
            }
            else
            {
                transform.DOLocalRotate(new Vector3(0f, -90f, 0f), 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
                hasInteracted = true;
            }
        }
    }

    public int InteractType() => (int)data.interactType;
}
