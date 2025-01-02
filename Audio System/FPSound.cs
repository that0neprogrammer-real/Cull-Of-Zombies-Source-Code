using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private PlayerState playerState;

    [Space]
    [SerializeField] private AudioClip jumping;
    [SerializeField] private AudioClip[] pickups; //0 - health item, 1 - weapon
    [SerializeField] private AudioClip[] interact; //0 - door, 1 - metal door, 2 - journal, 3 - generator, 4 - gas can

    [Space]
    [Tooltip("Footsteps")]
    [SerializeField] private AudioClip[] ground;
    [SerializeField] private AudioClip[] wood;
    [SerializeField] private AudioClip[] concrete;

    private CharacterController player;
    private AudioClip[] currentFootstepClips;
    private RaycastHit hit;
    private float nextFootstepTime = 0f;
    private int prevRand = -1;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        player = FindObjectOfType<CharacterController>();
    }

    private void Update() => DetectSurface();

    private int GetCurrentSurface()
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit))
        {
            try
            {
                CustomTag tag = hit.transform.GetComponent<CustomTag>();
                return (int)tag.GetTag();
            }
            catch
            {
                return 0;
            }
        }

        return 0;
    }

    private void DetectSurface()
    {
        if (!FPMovement.Instance.IsGrounded || 
            !FPMovement.Instance.gameObject.activeSelf || 
            !FPMovement.Instance.IsPlayerMoving()) return;

        if (FPMovement.Instance.IsJumping) audioPlayer.PlayOneShot(jumping);
        switch (FPMovement.Instance.playerState)
        {
            case FPMovement.MovementState.Walking:
                Footsteps(GetCurrentSurface(), 0.4f);
                break;
            case FPMovement.MovementState.Running:
                Footsteps(GetCurrentSurface(), 0.3f);
                break;
            case FPMovement.MovementState.Crouching:
                Footsteps(GetCurrentSurface(), 0.6f);
                break;
        }
    }

    public void Interact(int index) => audioPlayer.PlayOneShot(interact[index]);
    public void StopAudio() => audioPlayer.Stop();
    private void Footsteps(int index, float timeInterval)
    {
        switch (index)
        {
            case 2:
                currentFootstepClips = ground;
                break;
            case 3:
                currentFootstepClips = wood;
                break;
            case 4:
            case 5:
                currentFootstepClips = concrete;
                break;
            default:
                currentFootstepClips = null;
                break;
        } //GetCurrentSurface

        if (Time.time > nextFootstepTime && currentFootstepClips != null)
        {
            int rand;
            do { rand = Random.Range(0, currentFootstepClips.Length); } while (rand == prevRand);

            audioPlayer.pitch = Random.Range(0.8f, 1.2f);

            prevRand = rand;
            audioPlayer.PlayOneShot(currentFootstepClips[rand]);
            nextFootstepTime = Time.time + timeInterval;
        }
    }
    public void Pickups(int index)
    {
        audioPlayer.pitch = Random.Range(0.8f, 1.2f);
        audioPlayer.PlayOneShot(pickups[index]);
    }

}