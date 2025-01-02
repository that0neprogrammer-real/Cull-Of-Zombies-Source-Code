using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    protected void DisplayWeaponUI(GameObject prevIconHolder, GameObject newIconHolder, Image icon, 
        WeaponManager currWeapon)
    {
        prevIconHolder.SetActive(false);
        icon.sprite = currWeapon.Icon();
        newIconHolder.SetActive(true);
    }

    protected void UtilityUI(GameObject util, GameObject holder, Image icon)
    {
        util.SetActive(false);
        IDisplayUI utilityUI = holder.GetComponent<IDisplayUI>();
        if (utilityUI.Icon() == null) { return; }

        icon.sprite = null;
        icon.sprite = utilityUI.Icon();
        util.SetActive(true);
    }

    protected void HealthPackUI(GameObject healthPack, GameObject holder)
    {
        HealthPack check = holder.GetComponent<HealthPack>();
        if (check.GetCurrentHealthPackIndex() == -1) { healthPack.SetActive(false); return; }

        healthPack.SetActive(true);
    }
}
