using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPInteract : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip[] soundEffects;

    public void Pickup(int index)
    {
        audioPlayer.clip = soundEffects[index];
        audioPlayer.Play();
    }
}
