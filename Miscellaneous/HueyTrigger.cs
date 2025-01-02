using System.Collections;
using UnityEngine;

public class HueyTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider areaOfRescue;
    public bool take0ff = false, currentlyRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !currentlyRunning)
        {
            StartCoroutine(Escape());
            currentlyRunning = true;
        }
    }

    private IEnumerator Escape()
    {
        take0ff = true;

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Escape", true);
    }
}
