using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPath : MonoBehaviour
{
    [SerializeField] private PathCreation.Examples.PathFollower pathTrigger;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.CompareTag("Player")) 
                pathTrigger.startPath = true;
        }
        catch
        {
            Debug.Log("No Player");
        }
    }
}
