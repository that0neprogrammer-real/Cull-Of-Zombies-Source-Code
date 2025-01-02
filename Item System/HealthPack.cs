using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IDisplayUI
{
    [SerializeField] private Transform healthPack;
    [SerializeField] private HealthPackData healthPackData;
    [SerializeField] private UtilityInventory utilityInventory;
    [SerializeField] private P_HealthManager actions;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && GetCurrentHealthPackIndex() != -1)
            UseItem();
    }

    private void UseItem()
    {
        if (actions.isFullHealth || actions.isHealing)
            return;

        actions.ExecuteAction(0, healthPackData, null);

    }
    public int GetCurrentHealthPackIndex()
    {
        if (healthPack.gameObject.activeSelf) return 0;
        return -1;
    }
    public void RemoveHealthPack() => utilityInventory.RemoveHealthPack();

    #region
    public Sprite Icon() => null;
    public Vector2 IconDimensions() => Vector2.zero;
    public Vector2 AmmoCounter() => Vector2.zero;
    #endregion
}
