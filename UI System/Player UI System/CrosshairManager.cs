using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] private PlayerState checkState;
    [SerializeField] private GameObject crosshair;

    private void Update()
    {
        Conditions();
    }

    private void Conditions()
    {
        if (checkState.GetCurrentState() == PlayerState.CurrentState.aimingDownSights 
            || checkState.GetCurrentState() == PlayerState.CurrentState.currentlyTabbing)
        {
            if (crosshair.activeSelf) crosshair.SetActive(false);
        }
        else if (!crosshair.activeSelf)
        {
            crosshair.SetActive(true);
        }
    }
}
