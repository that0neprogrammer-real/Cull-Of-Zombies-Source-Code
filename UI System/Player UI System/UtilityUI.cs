using UnityEngine;
using UnityEngine.UI;

public class UtilityUI : UIDisplay
{
    [Space][SerializeField] private GameObject[] transformHolders; //grenade, health items, health pack
    [SerializeField] private GameObject[] iconHolder;
    [SerializeField] private Image[] icons;

    void Update()
    {
        DisplayUtility();
    }

    private void DisplayUtility()
    {
        UtilityUI(iconHolder[0], transformHolders[0], icons[0]); //grenades
        UtilityUI(iconHolder[1], transformHolders[1], icons[1]); //health items
        HealthPackUI(iconHolder[2], transformHolders[2]); //health pack
    }
}
