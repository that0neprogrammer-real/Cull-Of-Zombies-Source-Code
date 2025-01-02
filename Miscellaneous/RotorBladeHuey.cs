using UnityEngine;

public class RotorBladeHuey : MonoBehaviour
{
    [SerializeField] private Transform[] rotors;
    public bool hasStarted = false;

    private void Update()
    {
        if (!hasStarted) return;

        rotors[0].Rotate(Vector3.up * 1000f * Time.deltaTime);
        rotors[1].Rotate(Vector3.forward * 1000f * Time.deltaTime);
    }
}
