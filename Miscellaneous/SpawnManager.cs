using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxStack;

    private List<GameObject> pooledObects;

    void Start()
    {
        pooledObects = new List<GameObject>();

        for (int i = 0; i < maxStack; i++)
        {
            GameObject spawn = PoolObject(prefab);
            pooledObects.Add(spawn);
        }
    }

    private GameObject PoolObject(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.SetActive(false);

        return newObject;
    }

    public GameObject GetObject(RaycastHit hit)
    {
        foreach (GameObject prefab in pooledObects)
        {
            if (!prefab.activeSelf)
            {
                prefab.transform.SetLocalPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
                prefab.SetActive(true);
                return prefab;
            }
        }

        return null;
    }
}
