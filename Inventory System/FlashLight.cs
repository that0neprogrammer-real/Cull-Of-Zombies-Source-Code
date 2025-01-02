using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioSource soundEffects;
    public bool canUse;
    private bool onOff = false;

    private void Start()
    {
        canUse = false;
        onOff = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canUse)
            ToggleLight();
    }

    private void ToggleLight()
    {
        if (onOff)
        {
            flashLight.SetActive(false);
            soundEffects.Play();

            onOff = false;
        }
        else
        {
            flashLight.SetActive(true);
            soundEffects.Play();

            onOff = true;
        }
    }
}
