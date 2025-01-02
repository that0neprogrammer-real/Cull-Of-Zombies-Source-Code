using UnityEngine;

public class TruckSound : MonoBehaviour
{
    [SerializeField] private AudioSource player;

    public void PlayAudio() => player.Play();
    public void StopAudio() => player.Stop();
}
