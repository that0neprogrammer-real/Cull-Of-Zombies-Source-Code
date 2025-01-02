using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable, IDisplay
{
    [SerializeField] private ItemData data;

    public ItemData GetData() => data;
    public string DisplayName() => data.name;
    public Item GetPickupData() => this;
    public void OnPickup() => Destroy(gameObject);
}
