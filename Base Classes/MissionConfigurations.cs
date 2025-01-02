using System.Collections;
using UnityEngine;

public class MissionConfigurations : MonoBehaviour
{
    [SerializeField] protected MissionData data;
    [SerializeField] protected MissionManager manager;
    [SerializeField] protected PlayerState playerState;
    [SerializeField] protected MissionSoundHandler missionSound;
    [SerializeField] protected GameObject notifyLayer;
    [SerializeField] protected GameObject missionIcon;

    protected virtual void StartMission()
    {
        playerState.curerntlyOnMission = true;

        manager.DisplayMission(data);
        missionIcon.SetActive(true);
        missionSound.PlayMissionSound(false);

        StartCoroutine(ActivateNotify());
        Debug.Log("Mission Started!");
    }

    private IEnumerator ActivateNotify()
    {
        notifyLayer.SetActive(true);
        yield return new WaitForSeconds(4f);
        notifyLayer.SetActive(false);
    }

    protected virtual void EndMission()
    {
        manager.ClearDisplayMission();
        missionIcon.SetActive(false);
        missionSound.PlayMissionSound(true);
        playerState.curerntlyOnMission = false;

        StartCoroutine(MissionCompleted());
        Debug.Log("Mission Ended!");
    }

    protected virtual IEnumerator MissionCompleted()
    {
        playerState.completedMission = true;
        manager.ClearDisplayMission();

        manager.missionComplete.SetActive(true);
        yield return new WaitForSeconds(5f);
        manager.missionComplete.SetActive(false);

        playerState.completedMission = false;
        Destroy(gameObject);
    }
}

