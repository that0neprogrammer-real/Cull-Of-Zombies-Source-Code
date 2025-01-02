using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] private ItemPrefabs prefabs;

    [Space]
    public ItemData[] weaponSlots;
    public ItemData lastPrimary = null, lastSidearm = null; //this will be the data of the previous weapons picked up by the player

    public void AddPrimary(ItemData newPrimary, Item data)
    {
        if (weaponSlots[0] == newPrimary) return;

        if (weaponSlots[0] == null) weaponSlots[0] = newPrimary;
        else
        {
            lastPrimary = weaponSlots[0];
            weaponSlots[0] = newPrimary;

            prefabs.SwapWeapon(lastPrimary.id, prefabs.primaryWeaponPrefabs);
        }

        data.OnPickup();
    }

    public void AddSidearm(ItemData newSidearm, Item data)
    {
        if (weaponSlots[1] == newSidearm) return;

        if (weaponSlots[1] == null) weaponSlots[1] = newSidearm;
        else
        {
            lastSidearm = weaponSlots[1];
            weaponSlots[1] = newSidearm;

            prefabs.SwapWeapon(lastSidearm.id, prefabs.secondaryWeaponPrefabs);
        }

        data.OnPickup();
    }

    public bool WeaponSlot(int index) => weaponSlots[index] != null; //checks if the current slot has a weapon
    public int GetWeaponID(int index) => weaponSlots[index].id; //accesses the id of the current weapon equipped
}
