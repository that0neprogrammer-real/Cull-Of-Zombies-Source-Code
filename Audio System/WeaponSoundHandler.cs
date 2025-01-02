using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip reload;

    [Space]
    [SerializeField] private AudioClip[] primaryWeapons;
    [SerializeField] private AudioClip[] secondaryWeapons;
    [SerializeField] private AudioClip[] grenades;
    [SerializeField] private AudioClip[] healthItems;

    [Space]
    [SerializeField] private AudioClip[] entity;
    [SerializeField] private AudioClip[] other;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlaySound(int index, int id)
    {
        audioPlayer.pitch = 1f;
        switch (index)
        {
            case 0:
                audioPlayer.PlayOneShot(primaryWeapons[id]);
                break;
            case 1:
                audioPlayer.PlayOneShot(secondaryWeapons[id]);
                break;
            case 2:
                audioPlayer.PlayOneShot(grenades[id]);
                break;
            case 3:
                audioPlayer.PlayOneShot(healthItems[id]);
                break;

        }
    }

    public void ReloadSound() => audioPlayer.PlayOneShot(reload);

    public void SurfaceSound(int index)
    {
        audioPlayer.pitch = Random.Range(0.8f, 1.1f);
        int random = Random.Range(0, 2);

        switch (index)
        {
            case 0:
                audioPlayer.PlayOneShot(entity[random]);
                break;
            case 1:
                audioPlayer.PlayOneShot(other[random]);
                break;
        }
    }
}
