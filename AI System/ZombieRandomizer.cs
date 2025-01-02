using System.Collections;
using UnityEngine;

public class ZombieRandomizer : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController currentController;
    [SerializeField] private Material eyeMaterial;
    [SerializeField] private GameObject[] zombieModels;

    private IEnumerator Start()
    {
        int rng = Random.Range(0, zombieModels.Length);
        for (int i = 0; i < zombieModels.Length; i++)
        {
            if (i == rng)
            {
                zombieModels[i].GetComponent<Animator>().runtimeAnimatorController = currentController;
                AssignEyes(i);
                yield return null;

                Instantiate(zombieModels[i], transform);
                break;
            }
        }

        yield return null;
        Destroy(this);
    }

    private void AssignEyes(int i)
    {
        Transform eyeballTransform = zombieModels[i].GetComponent<ZombieAnimations>().eyeball;
        if (eyeballTransform == null) return;

        Renderer[] eyeballRenderers = eyeballTransform.GetComponentsInChildren<Renderer>();
        for (int j = 0; j < eyeballRenderers.Length; j++) eyeballRenderers[j].material = eyeMaterial;
    }
}
