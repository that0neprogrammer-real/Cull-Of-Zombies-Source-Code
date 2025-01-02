using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour
{
    public HUDState currentHUD;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private GameObject crosshair;

    #region
    [HideInInspector] public float[] idle = { 0.025f, 2 };
    [HideInInspector] public float[] idleCrouching = { 0.025f, 1 };
    [HideInInspector] public float[] walking = { 0.05f, 20 };
    [HideInInspector] public float[] crouching = { 0.025f, 10 };
    [HideInInspector] public float[] running = { 0.1f, 22 };
    #endregion

    [Space]
    public bool usingMissionTab = false;
    public bool readingJournal = false;
    public bool aimingDownSights = false;
    public bool holdingGrenade = false;

    public bool curerntlyOnMission = false;
    public bool completedMission = false;

    public enum HUDState
    {
        none,
        readingJournal,
        viewingMission,
        missionComplete
    }

    void Update()
    {
        HUDConditions();
        Crosshair();
    }

    public IEnumerator MissionCompleted()
    {
        completedMission = true;
        missionManager.ClearDisplayMission();
        missionManager.missionComplete.SetActive(true);
        yield return new WaitForSeconds(5f);

        missionManager.missionComplete.SetActive(false);
        completedMission = false;
    }

    private void HUDConditions()
    {
        if (usingMissionTab)
            currentHUD = HUDState.viewingMission;
        else if (completedMission)
            currentHUD = HUDState.missionComplete;
        else if (readingJournal)
            currentHUD = HUDState.readingJournal;
        else
            currentHUD = HUDState.none;
    }

    private void Crosshair()
    {
        if (aimingDownSights || usingMissionTab) 
            ToggleCrosshair(false);
        else 
            ToggleCrosshair(true);
    }

    public void ToggleADS(bool value) => aimingDownSights = value;

    public void ToggleCrosshair(bool value) => crosshair.SetActive(value);
}
