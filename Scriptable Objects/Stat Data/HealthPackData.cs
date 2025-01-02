using UnityEngine;

[CreateAssetMenu(fileName = "New Health Pack", menuName = "ScriptableObjects/Health Pack")]
public class HealthPackData : ScriptableObject
{
    public Sprite icon;
    public float iconSize;
    public Vector2 position;

    [Space]
    public int id;
    public new string name;
    public int maxStack;
    public HealthPackSlot healthPackType;

    [Space]
    [Header("Health Item Properties")]
    public float useTime;
    public float healingAmount;

    public enum HealthPackSlot
    {
        medKit
    }
}
