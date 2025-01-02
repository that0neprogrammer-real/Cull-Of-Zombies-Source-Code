using System.Collections;
using Nomnom.RaycastVisualization;
using DG.Tweening;
using UnityEngine;

public class WeaponConfigurations : MonoBehaviour
{
    [SerializeField] protected WeaponItemData weaponData;
    [SerializeField] protected Transform weaponCamera;
    [SerializeField] protected PlayerState checkState;
    [SerializeField] protected WeaponSoundHandler soundHandler;
    [SerializeField] protected CameraShake shake;
    [SerializeField] protected GameObject muzzleFlash;

    [Tooltip("0 - Original, 1 - ADS")]
    [SerializeField] [Space] protected Vector3[] gunPosition;
    [SerializeField] protected SpawnManager[] decalEffects;
    [SerializeField] [Range(0, 0.1f)] protected float viewmodelPunch;

    protected RaycastHit hit;
    protected int ammo, mag;
    protected int rotation = 90;
    protected bool readyToShoot = true;
    protected bool isReloading = false;

    protected WaitForSeconds fireRate => new(weaponData.fireRate);
    protected WaitForSeconds reloadSpeed => new(weaponData.reloadTime);

    protected virtual void OnDisable()
    {
        StopAllCoroutines();

        transform.localPosition = gunPosition[0];
        transform.localRotation = Quaternion.identity;
        readyToShoot = true;
        isReloading = false;
    }

    protected virtual void Start()
    {
        gunPosition[0] = transform.localPosition;

        ammo = weaponData.maxAmmunition;
        mag = weaponData.maxMagazineSize;
    }

    protected void AimingPosition(bool isAiming)
    {
        checkState.aimingDownSights = isAiming;
        Vector3 targetPosition = isReloading ? gunPosition[0] : isAiming ? gunPosition[1] : gunPosition[0];
        float bobbingOffset = isAiming ? 0f : CheckPlayerState();
        Vector3 targetBob = Vector3.up * bobbingOffset;

        Vector3 newPosition = Vector3.Lerp(transform.localPosition, targetPosition + targetBob, weaponData.aimSpeed * Time.deltaTime);
        transform.localPosition = newPosition;
    }

    private float CheckPlayerState()
    {
        switch (FPMovement.Instance.playerState)
        {
            case FPMovement.MovementState.Walking:
                return Mathf.Sin(Time.time * 10f) * 0.005f;
            case FPMovement.MovementState.Running:
                return Mathf.Sin(Time.time * 20f) * 0.005f;
            default:
                return Mathf.Sin(Time.time * 1f) * 0.005f;
        }
    }

    protected void DetectSurface(CustomTag tag, RaycastHit hit)
    {
        switch (tag.customTag)
        {
            case CustomTag.CustomTags.entity:
                decalEffects[0].GetObject(hit);
                break;
            case CustomTag.CustomTags.concrete:
                decalEffects[1].GetObject(hit);
                break;
            case CustomTag.CustomTags.wood:
                decalEffects[2].GetObject(hit);
                break;
            case CustomTag.CustomTags.ground:
                decalEffects[3].GetObject(hit);
                break;
        }
    }

    protected void DetectSound(CustomTag customTag)
    {
        switch (customTag.customTag)
        {
            case CustomTag.CustomTags.entity:
                soundHandler.SurfaceSound(0);
                break;
            case CustomTag.CustomTags.concrete:
                soundHandler.SurfaceSound(1);
                break;
            case CustomTag.CustomTags.wood:
                soundHandler.SurfaceSound(1);
                break;
            case CustomTag.CustomTags.ground:
                soundHandler.SurfaceSound(1);
                break;
        }
    }

    protected IEnumerator Shoot()
    {
        readyToShoot = false;
        StartCoroutine(SoundAndMuzzle());

        yield return new WaitForFixedUpdate();
        if (!weaponData.isShotgun)
        {
            if (VisualPhysics.Raycast(weaponCamera.position, Spread(), out hit, weaponData.range))
            {
                IDamageable damage = hit.transform.GetComponentInParent<IDamageable>();
                if (damage != null) damage.TakeDamage(weaponData.damage, weaponData.impactForce, -hit.normal);

                CustomTag tag = hit.transform.GetComponent<CustomTag>();
                if (tag != null)
                {
                    DetectSurface(tag, hit);
                    DetectSound(tag);
                }

            }
        }
        else
        {
            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                if (VisualPhysics.Raycast(weaponCamera.position, Spread(), out hit, weaponData.range))
                {
                    CustomTag tag = hit.transform.GetComponent<CustomTag>();
                    if (tag != null) DetectSurface(tag, hit);
                }
            }

            try
            {
                IDamageable damage = hit.transform.GetComponentInParent<IDamageable>();
                if (damage != null) damage.TakeDamage(weaponData.damage, weaponData.impactForce, -hit.normal);

                CustomTag tagSound = hit.transform.GetComponent<CustomTag>();
                if (tagSound != null) DetectSound(tagSound);
            }
            catch { }
        }

        shake.Shake(shake.recoil);
        transform.localPosition += Vector3.back * viewmodelPunch; //Visual Recoil
        mag--;

        yield return fireRate;
        readyToShoot = true;
    }

    private Vector3 Spread()
    {
        float adSpread = checkState.aimingDownSights ? (weaponData.spread / 2) : weaponData.spread;
        float x = Random.Range(-adSpread, adSpread);
        float y = Random.Range(-adSpread, adSpread);

        return weaponCamera.forward + new Vector3(x, y, 0);
    }

    private IEnumerator SoundAndMuzzle()
    {
        float mRot = Random.Range(-rotation, rotation);
        Vector3 newRotation = new(0f, 0f, mRot);
        muzzleFlash.transform.GetChild(0).localRotation = Quaternion.Euler(newRotation); //Muzzle Flash rotation
        soundHandler.PlaySound(weaponData.weaponType, weaponData.id); //Weapon Sound
        yield return null;

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }

    protected IEnumerator Reload(bool isInfinite)
    {
        isReloading = true;
        readyToShoot = false;
        transform.DOLocalRotate(new(10f, 0f, 0f), 0.15f).SetEase(Ease.Linear);
        yield return reloadSpeed;

        soundHandler.ReloadSound();
        transform.DOLocalRotate(new(0f, 0f, 0f), 0.15f).SetEase(Ease.Linear);
        if (isInfinite) mag = weaponData.maxMagazineSize;
        else
        {
            int reloadAmount = weaponData.maxMagazineSize - mag;
            reloadAmount = (ammo - reloadAmount) >= 0 ? reloadAmount : ammo;

            mag += reloadAmount;
            ammo -= reloadAmount;
        }

        readyToShoot = true;
        isReloading = false;
    }

}
