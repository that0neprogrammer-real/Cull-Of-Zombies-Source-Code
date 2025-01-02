using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesManager : MonoBehaviour
{
    public static ZombiesManager Instance;

    [SerializeField] private float radiusAroundTarget;
    public List<GameObject> units = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void AddUnit(GameObject obj)
    {
        if (!units.Contains(obj))
            units.Add(obj);
    }

    public void RemoveUnit(GameObject obj)
    {
        if (units.Contains(obj))
            units.Remove(obj);
    }
}
