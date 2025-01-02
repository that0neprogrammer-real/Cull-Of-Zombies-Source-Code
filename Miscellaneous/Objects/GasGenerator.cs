using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectData data;
    [SerializeField] private bool hasInteracted = false;

    public bool HasInteracted() => hasInteracted;

    public void Interact() => hasInteracted = true;

    public int InteractType() => (int)data.interactType;
}
