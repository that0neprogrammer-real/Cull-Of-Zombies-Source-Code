using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField] private BobbingConfigurations bobbingConfigs;

    [Space]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private bool sway;
    [SerializeField] private bool bobbing;

    #region
    private float[] idle = { 0.025f, 2 };
    private float[] idleCrouching = { 0.025f, 1 };
    private float[] walking = { 0.05f, 20 };
    private float[] crouching = { 0.025f, 10 };
    private float[] running = { 0.1f, 22 };
    #endregion

    private Vector3 originalPosition;
    private float amp, freq;
    private bool hasSet = false;

    void OnEnable()
    {
        SetWeapon();
    }

    void OnDisable()
    {
        weaponHolder = null;
    }

    void Update()
    {
        if (weaponHolder == null && bobbingConfigs.GetActiveGameObject() != null && !hasSet) SetWeapon();
        else if (weaponHolder != bobbingConfigs.GetActiveGameObject()) SetWeapon();
        else if (weaponHolder == null) return;

        if (!FPMovement.Instance.IsGrounded)
            return;

        if (!playerState.aimingDownSights && bobbing)
            BobbingMovement();

        SwayRotation();
        ResetPosition();
    }

    private void SetWeapon()
    {
        if (bobbingConfigs.GetActiveGameObject() != null)
        {
            weaponHolder = bobbingConfigs.GetActiveGameObject();
            originalPosition = weaponHolder.localPosition;
            hasSet = true;
        }
    }

    private void BobbingMovement()
    {
        FPMovement player = FPMovement.Instance;

        if (Mathf.Approximately(player.currentSpeed, player.GetSpeed(0)))
            MovementValue(idle);
        else if (Mathf.Approximately(player.currentSpeed, player.GetSpeed(1)))
            MovementValue(walking);
        else if (Mathf.Approximately(player.currentSpeed, player.GetSpeed(2)) && player.IsPlayerMoving())
            MovementValue(idleCrouching);
        else if (Mathf.Approximately(player.currentSpeed, player.GetSpeed(2)))
            MovementValue(crouching);
        else if (Mathf.Approximately(player.currentSpeed, player.GetSpeed(3)))
            MovementValue(running);

        bobbingConfigs.Movement(weaponHolder, 
            bobbingConfigs.BobbingMovement(amp, freq));
    }

    private void MovementValue(float[] values)
    {
        amp = values[0];
        freq = values[1];
    }

    private void SwayRotation()
    {
        if (!sway)
            return;

        Vector2 mouseInput = new(FPPlayerLook.Instance.mouseInput.x * bobbingConfigs.swayMultiplier, FPPlayerLook.Instance.mouseInput.y * bobbingConfigs.swayMultiplier);
        Quaternion rotationX = Quaternion.AngleAxis(-mouseInput.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseInput.x, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;

        weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, targetRotation, Time.deltaTime * bobbingConfigs.smooth);
    }

    private void ResetPosition()
    {
        if (weaponHolder.localPosition == originalPosition) 
            return;

        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, originalPosition, Time.deltaTime * 10f);
    }
}
