using DG.Tweening;
using UnityEngine;

public class ParticleLighting : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private Light groundLight;
    [SerializeField] private float duration;

    void Start()
    {
        groundLight.DOIntensity(0, duration).SetEase(Ease.Linear);
    }
}
