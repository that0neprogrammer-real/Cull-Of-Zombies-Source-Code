using UnityEngine;

public class CompassUI : MonoBehaviour
{
    [SerializeField] private Transform playerTransfrom;
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        direction.z = playerTransfrom.eulerAngles.y;
        transform.localEulerAngles = direction;
    }
}
