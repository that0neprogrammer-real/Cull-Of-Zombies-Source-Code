using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private bool destroyOnStart = false;

    private void Update()
    {
        if (destroyOnStart) return;

        if (duration > 0f) duration -= Time.deltaTime;
        else Destroy(gameObject);
    }
}
