using System.Collections;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public GameObject missionComplete;
    [SerializeField] private TextMeshProUGUI missionTitle;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private int missionIndex;
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private TextMeshProUGUI[] missions;
    private readonly bool[] completedMissions = new bool[4];

    private void Start()
    {
        missionIndex = -1;
    }

    public void DisplayMission(MissionData data)
    {
        missionTitle.SetText(data.missionName);
        missionIndex = data.missionIndex;

        for (int i = 0; i < data.subMissions.Length; i++)
            missions[i].SetText(data.subMissions[i]);

        SoundEffect(0);
    }

    public void ClearDisplayMission()
    {
        missionTitle.SetText("None");
        completedMissions[missionIndex] = true;

        for (int i = 0; i < missions.Length; i++)
            missions[i].SetText(" ");

        SoundEffect(1);
    }

    private void SoundEffect(int index)
    {
        audioPlayer.clip = soundEffects[index];
        audioPlayer.Play();
    }

    public int GetMissionIndex() => missionIndex;
    public bool CurrentMission(int index) => completedMissions[index];
}
