using UnityEngine;

[System.Serializable]
public class FPConfigurations
{
    [Header("Movement")]
    public bool canMove;
    public float baseSpeed;
    public float runningSpeed;
    public float crouchSpeed;
    public float jumpHeight;
    public float gravity;

    public void IncreaseSpeed(float amount)
    {
        baseSpeed += amount;
        runningSpeed += amount;
        crouchSpeed += amount;

    }

    public void DecreaseSpeed(float amount)
    {
        baseSpeed -= amount;
        runningSpeed -= amount;
        crouchSpeed -= amount;
    }
}
