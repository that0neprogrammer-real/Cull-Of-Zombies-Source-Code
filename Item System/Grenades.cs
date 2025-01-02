using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour, IDisplayUI
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform grenadeTransform;
    [SerializeField] private WeaponSoundHandler soundHandler;
    [SerializeField] private UtilityInventory inventory;
    [SerializeField] private PlayerState playerState;

    [Space]
    [SerializeField] private GrenadeItemData[] grenadeData;
    [SerializeField] private GameObject[] grenadeThrowables;
    [SerializeField] private float[] throwStrength; //overhand, underhand

    private void Update()
    {
        if (playerState.usingTab)
            return;

        if (GetCurrentGrenadeIndex() != -1) UseGrenade(KeyCode.Mouse0, KeyCode.Mouse1);
    }

    private void UseGrenade(KeyCode mouse0, KeyCode mouse1)
    {
        if (Input.GetKeyDown(mouse0)) StartCoroutine(SpawnPrefab(throwStrength[0])); 
        else if (Input.GetKeyDown(mouse1)) StartCoroutine(SpawnPrefab(throwStrength[1]));
    }

    private IEnumerator SpawnPrefab(float strength)
    {
        GameObject prefab = Instantiate(grenadeThrowables[GetCurrentGrenadeIndex()], playerCamera.position, playerCamera.rotation);
        Rigidbody grenadePyhsics = prefab.GetComponent<Rigidbody>();
        Vector3 playerDirection = playerCamera.forward;
        Vector3 totalDirection = playerDirection +  FPMovement.Instance.playerController.velocity.normalized;
        yield return null;

        yield return new WaitForFixedUpdate();
        grenadePyhsics.AddForce(totalDirection * strength, ForceMode.Impulse);

        int randSound = Random.Range(0, 2);
        soundHandler.PlaySound(2, randSound);
        inventory.RemoveGrenade(true);
        yield return null;
    }

    private int GetCurrentGrenadeIndex()
    {
        for (int i = 0; i < grenadeTransform.childCount; i++) if (grenadeTransform.GetChild(i).gameObject.activeSelf) return i;
        return -1; // Return -1 if no active child transform found
    }

    #region
    public Sprite Icon()
    {
        if (GetCurrentGrenadeIndex() == -1) return null;
        return grenadeData[GetCurrentGrenadeIndex()].icon;
    }
    public Vector2 IconDimensions() => Vector2.zero;
    public Vector2 AmmoCounter() => Vector2.zero;
    #endregion
}
