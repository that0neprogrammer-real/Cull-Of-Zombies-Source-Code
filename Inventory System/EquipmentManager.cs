using System.Collections;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private WeaponInventory weapons;
    [SerializeField] private UtilityInventory utility;
    [SerializeField] private P_HealthManager state;

    [Space]
    [SerializeField] private GameObject[] inventorySlots;
    [SerializeField] private GameObject[] primaryWeapons, sidearmWeapons, grenades, healthItems, medPack;

    void Update() => DisplayEquipment();

    private void DisplayEquipment()
    {
        ActivateGameObject();

        //stop player from switching when healing
        if (state.isHealing) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) //Weapons
            ToggleSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ToggleSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //Utility
            ToggleSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ToggleSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ToggleSlot(4);
    }

    private void ActivateGameObject()
    {
        if (weapons.WeaponSlot(0)) ActivateWeapon(weapons.GetWeaponID(0), primaryWeapons);
        if (weapons.WeaponSlot(1)) ActivateWeapon(weapons.GetWeaponID(1), sidearmWeapons);

        if (utility.GrenadeSlot()) ActivateUtility(utility.GetGrenadeID(), grenades);
        else DeactivateGameObject(grenades);

        if (utility.HealthItemSlot()) ActivateUtility(utility.GetHealthItemID(), healthItems);
        else DeactivateGameObject(healthItems);

        if (utility.HealthPackSlot()) ActivateUtility(0, medPack);
        else DeactivateGameObject(medPack);
    }

    private void ToggleSlot(int slotIndex)
    {
        if (inventorySlots[slotIndex].activeSelf) return;
        else
        {
            for (int i = 0; i < inventorySlots.Length; i++)
                inventorySlots[i].SetActive(i == slotIndex);
        }
    }

    private void ActivateWeapon(int index, GameObject[] slot)
    {
        if (slot[index].activeSelf) return;
        else
        {
            for (int i = 0; i < slot.Length; i++)
            {
                Weapons id = slot[i].GetComponent<Weapons>();
                if (id != null && id.WeaponID() == index) slot[i].SetActive(true);
                else slot[i].SetActive(false);
            }
        }
    }

    private void ActivateUtility(int index, GameObject[] slot)
    {
        if (slot[index].activeSelf) return;
        else
        {
            for (int i = 0; i < slot.Length; i++)
                slot[i].SetActive(i == index);
        }
    }

    private void DeactivateGameObject(GameObject[] slot)
    {
        bool allDeactivated = true; // Flag to track if all GameObjects are deactivated
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].activeSelf) // Check if the GameObject is active
            {
                slot[i].SetActive(false); // Deactivate the GameObject
                allDeactivated = false; // Set the flag to false since there's at least one GameObject still active
            }
        }

        if (allDeactivated) return; // If all GameObjects are deactivated, exit the loop
    }

}
