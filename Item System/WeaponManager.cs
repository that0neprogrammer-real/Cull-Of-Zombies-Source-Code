using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IDisplayUI
{
    [SerializeField] private WeaponItemData[] weapons;
    [SerializeField] private Weapons[] weaponAmmo;
    public WeaponType weaponType;

    public enum WeaponType
    {
        primary,
        sidearm
    }

    public int GetCurrentWeaponIndex()
    {
        for (int i = 0; i < transform.childCount; i++) 
            if (transform.GetChild(i).gameObject.activeSelf) return i;

        return -1; // Return -1 if no active child transform found
    }

    #region
    public Sprite Icon() => weapons[GetCurrentWeaponIndex()].icon;
    public Vector2 AmmoCounter() => weaponAmmo[GetCurrentWeaponIndex()].AmmoCounter();
    public Vector2 IconDimensions() => weapons[GetCurrentWeaponIndex()].widthHeight;
    #endregion
}
