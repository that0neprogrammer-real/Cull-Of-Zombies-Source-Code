using UnityEngine;

public interface IInteractable
{
    void Interact();
    int InteractType();
    bool HasInteracted();
}
