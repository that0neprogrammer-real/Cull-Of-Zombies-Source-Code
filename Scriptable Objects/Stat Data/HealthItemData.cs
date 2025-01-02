using UnityEngine;

[CreateAssetMenu(fileName = "New Utility", menuName = "ScriptableObjects/Utility")]
public class HealthItemData : ScriptableObject
{
    public Sprite icon;
    public float iconSize;

    [Space]
    public int id;
    public float healingAmount;
    public float useTime;
    public UtilitySlot utilityType;
    [Space] public int maxStack;

    public enum UtilitySlot
    {
        bandage,
        painkiller,
        energyDrink
    }
}
