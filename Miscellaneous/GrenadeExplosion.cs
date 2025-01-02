using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private GameObject grenadeModel;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GrenadeItemData grenadeItemData;
    private float timerCountDown;
    private bool alreadySpawned = false;

    private void Start()
    {
        timerCountDown = grenadeItemData.delayTime;
        grenadeModel = gameObject;
    }

    private void Update()
    {
        if (!grenadeItemData.explodeOnHit)
        {
            timerCountDown -= Time.deltaTime;
            if (timerCountDown <= 0f && !alreadySpawned)
            {
                alreadySpawned = true;

                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(grenadeModel);
            }
        }
    }

    private void OnCollisionEnter()
    {
        if (grenadeItemData.explodeOnHit && !alreadySpawned)
        {
            alreadySpawned = true;

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(grenadeModel);
        }
    }
}

