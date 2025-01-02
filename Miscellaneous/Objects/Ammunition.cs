using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour, IInteractable, IDisplay
{
    [SerializeField] private ObjectData data;
    [SerializeField] private Transform primaryHolder;
    [SerializeField] private Weapons currentWeapon;
    [SerializeField] private bool hasInteracted = false;

    public void Interact() => RefillAmmo();

    public void RefillAmmo()
    {
        FindActiveWeapon();
        if (currentWeapon == null) return;

        currentWeapon.Refill();
    }

    private void FindActiveWeapon()
    {
        for (int i = 0; i < primaryHolder.childCount; i++)
        {
            Transform currentChild = primaryHolder.GetChild(i);
            Weapons currWeapon = currentChild.GetComponent<Weapons>();

            if (currWeapon.gameObject.activeSelf)
            {
                currentWeapon = currWeapon;
                return; // Exit the loop after finding the current weapon
            }
        }
    }

    public bool HasInteracted() => hasInteracted;

    public int InteractType() => (int)data.interactType;

    public string DisplayName() => "Replenish " + data.name;
}
