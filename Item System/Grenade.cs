using UnityEngine;

public class Grenade : MonoBehaviour//, IDisplayUI
{
    /*
    [SerializeField] private StateController func;
    [SerializeField] private GrenadeItemData grenadeItemData;
    [SerializeField] private UtilityInventory utilityInventory;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        if (func.usingMissionTab)
            return;

        ChuckAGrenade(KeyCode.Mouse0, true);
        ChuckAGrenade(KeyCode.Mouse1, false);
    }

    private void ChuckAGrenade(KeyCode key, bool value)
    {
        if (Input.GetKey(key))
            func.holdingGrenade = true;
        else if (Input.GetKeyUp(key) && func.holdingGrenade)
        {
            ThrowGrenade(value);
            func.holdingGrenade = false;
        }
    }

    private void ThrowGrenade(bool value)
    {
        SpawnGrenade(value);
        utilityInventory.RemoveGrenade(true);
    }

    private void SpawnGrenade(bool isOverhand)
    {
        GameObject grenade = Instantiate(grenadePrefab, playerCamera.position + playerCamera.forward * 0.25f, playerCamera.rotation);
        Rigidbody throwGrenade = grenade.GetComponent<Rigidbody>();

        float throwStrength = isOverhand ? utilityInventory.nadeValues[1] : 1f;

        // Check if the player is moving forward
        if (Vector3.Dot(FPMovement.Instance.playerController.velocity, playerCamera.forward) > 0.1f)
            throwStrength *= utilityInventory.nadeValues[0];

        Vector3 throwDirection = playerCamera.forward;
        throwGrenade.AddForce(throwDirection * throwStrength, ForceMode.VelocityChange);
    }

    #region
    public Sprite Icon()
    {
        return grenadeItemData.icon;
    }
    public Vector2 AmmoCounter()
    {
        return Vector2.zero;
    }
    #endregion
    */
}
