using UnityEngine;

public class ItemPrefabs : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [Space]
    public GameObject[] primaryWeaponPrefabs;
    public GameObject[] secondaryWeaponPrefabs;
    [SerializeField] private GameObject[] utilityPrefabs;
    [SerializeField] private GameObject[] grenadePrefabs;

    public void SwapWeapon(int previousWeapon, GameObject[] gameObjectArray)
    {
        //we find the corresponding prefab of the weapon to spawn
        foreach (GameObject weaponPrefab in gameObjectArray)
        {
            int weaponID = weaponPrefab.GetComponent<IPickable>().GetData().id;
            if (weaponID != previousWeapon)
                continue; //continue to the next iteration until the item is found

            Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);
            break; //after spawning the object, we exit out of the loop
        }

        Debug.Log("Swapped Weapon!");
    }

    public void SwapHealthBooster(int previousHealthBooster)
    {
        foreach (GameObject healthBoosterPrefab in utilityPrefabs)
        {
            int healthBooster = healthBoosterPrefab.GetComponent<IPickable>().GetData().id;
            if (healthBooster != previousHealthBooster)
                continue; //continue to the next iteration until the item is found

            Instantiate(healthBoosterPrefab, spawnPoint.position, Quaternion.identity);
            break; //after spawning the object, we exit out of the loop
        }

        Debug.Log("Swapped Utility!");
    }

    public void SwapGrenade(int previousUtility)
    {
        foreach (GameObject grenadePrefab in grenadePrefabs)
        {
            int grenade = grenadePrefab.GetComponent<IPickable>().GetData().id;
            if (grenade != previousUtility)
                continue; //continue to the next iteration until the item is found

            Instantiate(grenadePrefab, spawnPoint.position, Quaternion.identity);
            break; //after spawning the object, we exit out of the loop
        }

        Debug.Log("Swapped Utility!");
    }
}
