using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "ScriptableObjects/Grenade")]
public class GrenadeItemData : ScriptableObject
{
    public Sprite icon;
    public float iconSize;

    [Space]
    public GrenadeType grenadeType;
    public new string name;
    public int id;

    [Space]
    [Header("Grenade Properties")]
    public bool explodeOnHit;
    public float delayTime;

    public enum GrenadeType
    {
        frag,
        molotov,
        stun
    }
}
