using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameObjects : MonoBehaviour
{
    public GameObject[] gameObjectChildren;

    public Transform GetActiveGameObject()
    {
        for (int i = 0; i < gameObjectChildren.Length; i++)
        {
            if (gameObjectChildren[i].activeSelf)
                return gameObjectChildren[i].transform.GetChild(0);
        }

        return null;
    }
}
