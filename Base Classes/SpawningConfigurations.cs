using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningConfigurations : MonoBehaviour
{
    [SerializeField] protected int maxSize;
    [SerializeField] protected Transform parent;
    [SerializeField] protected GameObject[] prefabs;
    protected List<GameObject> objectList;

    protected virtual void Start()
    {
        objectList = new(); //initialize list
        Initialize();    
    }

    protected void ShuffleArray(GameObject[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);

            GameObject temp = arr[i]; //swap
            arr[i] = arr[rand];
            arr[rand] = temp;
        }
    }

    protected void Initialize()
    {
        for (int i = 0; i < maxSize; i++)
        {
            GameObject obj = CreateObject();
            objectList.Add(obj); //add gameobject to the list
        }
    }

    protected void SpawnObject(Bounds bounds)
    {
        GameObject newObj = GetObject();
        if (newObj == null) return;

        newObj.transform.position = GetRandomPointInCollider(bounds);
        newObj.SetActive(true);
    }

    protected GameObject GetObject()
    {
        int rand = Random.Range(0, objectList.Count);
        GameObject getObj = objectList[rand];

        if (getObj.activeSelf)
        {
            bool allActive = true;
            foreach (GameObject obj in objectList)
            {
                if (!obj.activeSelf)
                {
                    allActive = false;
                    return obj;
                }
            }

            if (allActive) return null;
        }

        return getObj;
    }

    protected GameObject CreateObject()
    {
        Transform newParent = parent == null ? transform : parent;
        int rand = Random.Range(0, prefabs.Length);
        GameObject prefab = Instantiate(prefabs[rand], newParent);

        prefab.SetActive(false);
        return prefab;
    }

    protected Vector3 GetRandomPointInCollider(Bounds bounds)
    {
        float randX = Random.Range(-bounds.extents.x, bounds.extents.x);
        //float randomY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float randZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        Vector3 newPosition = bounds.center + new Vector3(randX, 0f, randZ);
        return newPosition;
    }
}
