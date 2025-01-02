using System.Collections;
using Nomnom.RaycastVisualization;
using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponItemData data;
    [SerializeField] private StateController func;
    [SerializeField] private Transform weaponCamera; //assign to the weapon camera
    [SerializeField] private WeaponSoundHandler weaponSound;
    [SerializeField] private LayerMask layerToAvoid;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] [Tooltip("0 - Blood, 1 - Concrete, 2 - Wood, 3 - Dirt")] private SpawnManager[] decals;
    [SerializeField] [Tooltip ("0 - Normal Position, 1 - Aim Position")] private Vector3[] position;

    [Space]
    [SerializeField] [Range(0.1f, 1f)] private float viewmodelPunch;
    [SerializeField] private int magazineSize;
    public int ammunitionSize;

    private bool readyToShoot;
    private bool shooting;
    private bool reloading;

    private Quaternion originalRotation;
    private readonly int rotation = 90;

    private void OnDisable()
    {
        func.ToggleADS(false);
    }

    void Start()
    {
        DOTween.SetTweensCapacity(1000, 1000);
        originalRotation = weaponCamera.localRotation;

        magazineSize = data.maxMagazineSize;
        ammunitionSize = data.maxAmmunition;
        readyToShoot = true;
    }

    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (data.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && ammunitionSize > 0 && magazineSize != data.maxMagazineSize) 
            StartCoroutine(Reload(data.infiniteAmmo));

        if (Input.GetKeyDown(KeyCode.Mouse1) && !func.usingMissionTab) 
            Aim();

        if (!reloading && readyToShoot && shooting && magazineSize > 0 && !func.usingMissionTab) 
            StartCoroutine(StartShooting());

        if (!shooting) 
            ResetRecoil();
    }

    private void Aim()
    {
        if (!func.aimingDownSights) func.ToggleADS(true);
        else func.ToggleADS(false);

        Vector3 targetPosition = func.aimingDownSights ? position[1] : position[0];
        transform.DOLocalMove(targetPosition, data.aimSpeed).SetEase(Ease.Linear);
    }

    private IEnumerator StartShooting()
    {
        readyToShoot = false;

        Shoot(); //perform shoot func
        yield return new WaitForSeconds(data.fireRate);

        readyToShoot = true;
    }

    private void Shoot()
    {
        StartCoroutine(MuzzleFlash());

        if (VisualPhysics.Raycast(weaponCamera.position, weaponCamera.forward, out RaycastHit hit, data.range, ~layerToAvoid))
        {
            Transform target = hit.transform;
            if (target != null)
            {
                if (target.CompareTag("Zombie"))
                {
                    //damage
                }
            }

            CheckSurface(hit);
            Debug.Log("Hit " + hit.transform.name);
        }

        Recoil();
        magazineSize--;
    }

    private IEnumerator MuzzleFlash()
    {
        float xPosition = Random.Range(-rotation, rotation);
        Vector3 rotate = new(0f, 0f, xPosition);

        muzzleFlash.transform.GetChild(0).localRotation = Quaternion.Euler(rotate);
        weaponSound.PlaySound(data.weaponType, data.id);
        yield return null;

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.06f);
        muzzleFlash.SetActive(false); 
    }

    private void CheckSurface(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Zombie"))
            decals[0].GetObject(hit);
        else if (hit.transform.CompareTag("Concrete"))
            decals[1].GetObject(hit);
        else if (hit.transform.CompareTag("Wood"))
            decals[2].GetObject(hit);
        else if (hit.transform.CompareTag("Dirt"))
            decals[3].GetObject(hit);
    }

    private void Recoil()
    {
        VisualRecoil(true);

        float xRecoil = Random.Range(-data.recoil, data.recoil);
        float yRecoil = Random.Range(-data.recoil, data.recoil);

        Vector3 recoil = new(xRecoil, yRecoil, 0f);
        Quaternion target = weaponCamera.localRotation * Quaternion.Euler(recoil);

        weaponCamera.DOLocalRotateQuaternion(target, 0.4f).SetEase(Ease.Linear);
        VisualRecoil(false); 
    }

    private void VisualRecoil(bool value)
    {
        float zPosition = func.aimingDownSights ? position[1].z : position[0].z;

        if (value) 
            transform.DOLocalMoveZ(zPosition + 
                -viewmodelPunch, 0.2f).SetEase(Ease.Linear); //provides a visual recoil to the weapon
        else
            transform.DOLocalMoveZ(zPosition, 0.2f).SetEase(Ease.Linear); //resets the position of the weapon base on the current position
    }

    private void ResetRecoil()
    {
        if (weaponCamera.localRotation != originalRotation)
            weaponCamera.DOLocalRotateQuaternion(originalRotation, 4).SetEase(Ease.Linear);
    }

    private IEnumerator Reload(bool inifiniteAmmo)
    {
        reloading = true;
        Debug.Log("Reloading!");
        yield return new WaitForSeconds(data.reloadTime);

        if (inifiniteAmmo) magazineSize = data.maxMagazineSize;
        else
        {
            int reloadAmountNeeded = data.maxMagazineSize - magazineSize;
            reloadAmountNeeded = (ammunitionSize - reloadAmountNeeded) >= 0 ? reloadAmountNeeded : ammunitionSize;

            magazineSize += reloadAmountNeeded;
            ammunitionSize -= reloadAmountNeeded;
        }

        Debug.Log("Reload Finished!");
        reloading = false;
    }

    #region
    public int MaxAmmo() => data.maxAmmunition;
    public Vector2 AmmoCounter() => new(magazineSize, ammunitionSize);
    public int WeaponID() => data.id;
    #endregion
}
