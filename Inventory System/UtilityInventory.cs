using TMPro;
using UnityEngine;

public class UtilityInventory : MonoBehaviour
{
    [SerializeField] private ItemPrefabs objectToSpawn;

    [Space]
    [SerializeField] private ItemData grenadeSlot;
    [SerializeField] private ItemData healthItem, healthPack;

    private ItemData lastGrenade = null, lastHealthItem = null;

    public void AddNewGrenade(ItemData newGrenade, Item data)
    {
        if (grenadeSlot == newGrenade)
            return;

        if (grenadeSlot != null)
            RemoveGrenade(false);

        grenadeSlot = newGrenade;
        if (lastGrenade != null) objectToSpawn.SwapGrenade(lastGrenade.id);

        data.OnPickup();
    }
    public void AddNewHealthItem(ItemData newHealthItem, Item data)
    {
        if (healthItem == newHealthItem)
            return;

        if (healthItem != null)
            RemoveHealthItem(false);

        healthItem = newHealthItem;
        if (lastHealthItem != null) objectToSpawn.SwapHealthBooster(lastHealthItem.id);

        data.OnPickup();
    }
    public void AddNewHealtPack(ItemData newHealthPack, Item data)
    {
        if (healthPack == newHealthPack) return;

        healthPack = newHealthPack;
        data.OnPickup();
    }

    public void RemoveGrenade(bool remove)
    {
        if (!remove) lastGrenade = grenadeSlot;
        else
        {
            lastGrenade = null;
            grenadeSlot = null;
        }
    }
    public void RemoveHealthItem(bool remove)
    {
        if (!remove) lastHealthItem = healthItem;
        else
        {
            lastHealthItem = null;
            healthItem = null;
        }
    }
    public void RemoveHealthPack()
    {
        healthPack = null;
    }

    public bool HealthPackSlot() => healthPack != null;
    public bool HealthItemSlot() => healthItem != null;
    public int GetHealthItemID() => healthItem.id;

    public bool GrenadeSlot() => grenadeSlot != null;
    public int GetGrenadeID() => grenadeSlot.id;
}
