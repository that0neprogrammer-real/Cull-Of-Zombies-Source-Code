using Nomnom.RaycastVisualization;
using UnityEngine;

public class Weapons : WeaponConfigurations
{
    private bool currentlyShooting = false;
    private bool currentlyAiming = false;

    private float currentMag => mag;
    private float currentAmmo => ammo;

    protected override void OnDisable()
    {
        base.OnDisable();
        currentlyAiming = false;
    }

    protected override void Start() => base.Start();

    private void Update() => Conditions();

    private void Conditions()
    {
        if (checkState.GetCurrentState() == PlayerState.CurrentState.paused) return;

        if (weaponData.allowButtonHold) currentlyShooting = Input.GetKey(KeyCode.Mouse0);
        else currentlyShooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentlyAiming = !currentlyAiming;
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentMag > 0 && currentMag != weaponData.maxMagazineSize || currentMag <= 0)
        {
            if (!isReloading && !currentlyShooting) StartCoroutine(Reload(weaponData.infiniteAmmo));
        }
        else if (currentlyShooting)
        {
            if (readyToShoot && !isReloading && currentMag > 0) StartCoroutine(Shoot());
        }

        AimingPosition(currentlyAiming);
    }

    public void Refill()
    {
        ammo = weaponData.maxAmmunition;
        mag = weaponData.maxMagazineSize;

        print("working");
    }

    #region
    public int CurrentAmmo() => weaponData.maxAmmunition;
    public Vector2 AmmoCounter() => new(mag, ammo);
    public int WeaponID() => weaponData.id;
    #endregion
}
