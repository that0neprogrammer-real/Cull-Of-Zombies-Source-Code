using System.Collections;
using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private float time;

    private void OnEnable()
    {
        hitParticle.Play();
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(time);

        hitParticle.Stop();
        hitParticle.Clear();
        hitParticle.gameObject.SetActive(false);
    }
}
