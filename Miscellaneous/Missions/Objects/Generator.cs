using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectData data;
    [SerializeField] private bool hasInteracted;

    public bool HasInteracted() => hasInteracted;
    public void Interact() => hasInteracted = true;
    public int InteractType() => (int)data.interactType;
}
