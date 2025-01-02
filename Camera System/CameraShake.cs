using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private P_HealthManager playerHealth;

    [Space]
    [Tooltip("0 - Duration, 1 - Intensity")] public float[] explosion;
    [Tooltip("0 - Duration, 1 - Intensity")] public float[] damaged;
    [Tooltip("0 - Duration, 1 - Intensity")] public float[] recoil;

    public void Shake(float[] type)
    {
        float xShake = Random.Range(-type[1], type[1]);
        float yShake = Random.Range(-type[1], type[1]);
        Vector3 punch = new(xShake, yShake, 0f);

        playerCamera.DOPunchPosition(punch, type[0], 20);
    }
}
