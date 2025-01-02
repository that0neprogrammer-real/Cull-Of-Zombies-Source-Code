using System.Collections;
using UnityEngine;

public class HealthItems : MonoBehaviour, IDisplayUI
{
    [SerializeField] private Transform utilityTransform;
    [SerializeField] private WeaponSoundHandler soundHandler;
    [SerializeField] private P_HealthManager actions;
    [SerializeField] private UtilityInventory inventory;

    [Space]
    [SerializeField] private HealthItemData[] utilityData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && GetCurrentUtilityIndex() != -1) UseHealthItem();
    }

    private void UseHealthItem()
    {
        int index = GetCurrentUtilityIndex();
        switch (index)
        {
            case 0:
                StartCoroutine(Bandage(utilityData[index]));
                break;
            case 1:
                StartCoroutine(Pills(utilityData[index]));
                break;
            case 2:
                StartCoroutine(EnergyDrink(utilityData[index]));
                break;
            default:
                Debug.Log("No ID");
                break;
        }
    }

    private IEnumerator Bandage(HealthItemData data) 
    {
        if (actions.isFullHealth || actions.isHealing) yield break;

        soundHandler.PlaySound(3, 0);
        actions.ExecuteAction(1, null, data);

        inventory.RemoveHealthItem(true);
        Debug.Log("Bandage Used!");
    } //Gradual Heal

    private IEnumerator Pills(HealthItemData data)
    {
        if (actions.isHulk || actions.superHuman)
            yield break;

        soundHandler.PlaySound(3, 1);

        actions.ExecuteAction(2, null, data);
        inventory.RemoveHealthItem(true);
        Debug.Log("Hulk Activated!");
    } //Damage Reduction

    private IEnumerator EnergyDrink(HealthItemData data)
    {
        if (actions.isFlash || actions.superHuman)
            yield break;

        soundHandler.PlaySound(3, 2);
        actions.ExecuteAction(3, null, data);

        inventory.RemoveHealthItem(true);
        Debug.Log("Adrenaline Activated!");
    } //Faster Movement

    private int GetCurrentUtilityIndex()
    {
        for (int i = 0; i < utilityTransform.childCount; i++)
        {
            if (utilityTransform.GetChild(i).gameObject.activeSelf) return i;
        }

        return -1; // Return -1 if no active child transform found
    }

    #region
    public Sprite Icon()
    {
        if (GetCurrentUtilityIndex() == -1) return null;
        return utilityData[GetCurrentUtilityIndex()].icon;
    }
    public Vector2 IconDimensions() => Vector2.zero;
    public Vector2 AmmoCounter() => Vector2.zero;
    #endregion
}
