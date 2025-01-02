using UnityEngine.UI;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UIDisplay))]
public class WeaponUI : UIDisplay
{
    [SerializeField] private WeaponManager[] transformHolder; //primary, secondary
    [SerializeField] private TextMeshProUGUI[] ammunitionTexts; //clip, reserve
    [SerializeField] private GameObject[] iconHolders;
    [SerializeField] private Image[] icons;

    void Update()
    {
        DisplayWeapon();
    }

    private void DisplayWeapon()
    {
        if (transformHolder[0].gameObject.activeSelf && transformHolder[0].GetCurrentWeaponIndex() != -1) //Primary
        {
            UpdateAmmo((int)transformHolder[0].weaponType, ammunitionTexts, transformHolder[0].AmmoCounter());
            DisplayWeaponUI(iconHolders[1], iconHolders[0], icons[0], transformHolder[0]);
        }
        else if (transformHolder[1].gameObject.activeSelf && transformHolder[1].GetCurrentWeaponIndex() != -1) //Sidearm
        {
            UpdateAmmo((int)transformHolder[1].weaponType, ammunitionTexts, transformHolder[1].AmmoCounter());
            DisplayWeaponUI(iconHolders[0], iconHolders[1], icons[1], transformHolder[1]);
        }
        else if (!transformHolder[0].gameObject.activeSelf || !transformHolder[1].gameObject.activeSelf)
        {
            if (iconHolders[0].activeSelf) iconHolders[0].SetActive(false);
            else if (iconHolders[1].activeSelf) iconHolders[1].SetActive(false);
        }
    }

    private void UpdateAmmo(int index, TextMeshProUGUI[] display, Vector2 magAmmo)
    {
        switch (index)
        {
            case 0:
                display[1].SetText(magAmmo.x.ToString());
                display[2].SetText(magAmmo.y.ToString());
                break;
            case 1:
                display[0].SetText(magAmmo.x.ToString());
                break;
        }
    }
}
