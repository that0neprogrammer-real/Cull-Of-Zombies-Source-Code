using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]

public class ItemData : ScriptableObject
{
    [Space]
    public int id;
    public new string name;
    public ItemType itemType;

    public enum ItemType
    {
       primary,
       sidearm,
       grenade,
       healthItem,
       healthPack
    }
}
