using UnityEngine;

public interface IPickable
{
    ItemData GetData();
    Item GetPickupData();
    void OnPickup();
}
