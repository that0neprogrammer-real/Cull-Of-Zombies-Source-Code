using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponItemData : ScriptableObject
{
    public int id;
    public int weaponType;

    [Space]
    [Header("UI Properties")]
    public Sprite icon;
    public float iconSize;
    public Vector2 widthHeight; //size of the icon

    [Space]
    [Header("Weapon Properties")]
    public bool infiniteAmmo;
    public float speedMultiplier;
    public int bulletsPerShot;
    [Range(0, 20)] public float aimSpeed; //how fast the gun aims
    public int damage;
    public int maxMagazineSize;
    public int maxAmmunition;
    public float recoil;
    public float spread; //random inaccuracy
    public float reloadTime;
    public float fireRate; //how fast the gun can shoot
    public float range;
    public float impactForce; //for hitting rigidbodies


    [Header("Other Properties")]
    [Tooltip("Is the gun automatic or not?")] public bool allowButtonHold; //True if gun is automatic, otherwise if it is single-fire
    public bool isShotgun;

    public enum WeaponSlot
    {
        Primary,
        Sidearm,
    }
}
