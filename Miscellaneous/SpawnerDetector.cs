using UnityEngine;
using System.Collections.Generic;

public class SpawnerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private List<ZombieSpawner> spawnerList;

    private void Update()
    {
        foreach (ZombieSpawner spawner in spawnerList)
        {
            if (DistanceFromTarget(spawner.transform.position, transform.position) < 50f)
            {
                if (!spawner.hasSpawned)
                {
                    spawner.Spawn(false);
                    spawner.hasSpawned = true;
                    Debug.Log("spawned");
                }
            }
        }
    }

    private float DistanceFromTarget(Vector3 pos1, Vector3 pos2) => Vector3.Distance(pos1, pos2);
}
