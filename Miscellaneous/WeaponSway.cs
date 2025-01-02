using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private float swayAmount;
    private Transform currentWeapon;

    private void Awake() => playerState = FindObjectOfType<PlayerState>();

    private void Update()
    {
        if (playerState.GetCurrentState() == PlayerState.CurrentState.aimingDownSights 
            || playerState.GetCurrentState() == PlayerState.CurrentState.paused) return;

        Vector3 movement = FPPlayerLook.Instance.mouseInput * swayAmount / 1000;
        currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null) currentWeapon.localPosition += movement;
    }

    private Transform GetCurrentWeapon()
    {
        foreach (Transform obj in transform) if (obj.gameObject.activeSelf) return obj;
        return null;
    }
}
