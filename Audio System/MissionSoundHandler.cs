using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] missionSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMissionSound(bool missionFinished)
    {
        if (missionFinished) audioSource.PlayOneShot(missionSounds[1]);
        else audioSource.PlayOneShot(missionSounds[0]);
    }
}
