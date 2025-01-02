using UnityEngine;

public class ZombieSoundHandler : MonoBehaviour
{
    [SerializeField] private ZombieNavigation state;
    public AudioSource talk;
    public AudioSource move;
    public AudioClip doorBreaking;
    public AudioClip[] idle, chase, attack, hit, death, moving;

    private float timer = 0f;
    private float nextFootstepRoam = 0f;
    private float nextFootstepChase = 0f;
    private int prevRand = -1;

    private void Awake() => state = GetComponentInParent<ZombieNavigation>();

    private void Start() => timer = Random.Range(0.5f, 5f);

    private void Update()
    {
        switch (state.CheckState())
        {
            case ZombieConfigurations.CurrentState.Roaming:
                ZombieSounds(idle, 5f);
                break;
            case ZombieConfigurations.CurrentState.Chasing:
                ZombieSounds(chase, 3f);
                break;
        }

        MovingSounds(0.7f, 0.3f);
    }

    private void ZombieSounds(AudioClip[] array, float maxTimer)
    {
        if (timer <= 0f && !talk.isPlaying)
        {
            PlayAudio(array, talk);
            timer = Random.Range(1f, maxTimer);
        }
        else timer -= Time.deltaTime;
    }

    private void MovingSounds(float roam, float chase)
    {
        if (state.IsRoaming())
        {
            if (Time.time > nextFootstepRoam)
            {
                int rand;
                do { rand = Random.Range(0, moving.Length); } while (rand == prevRand);
                prevRand = rand;

                PlayAudio(moving, move);
                nextFootstepRoam = Time.time + roam;
            }
        }
        else if (state.IsChasing())
        {
            if (Time.time > nextFootstepChase)
            {
                int rand;
                do { rand = Random.Range(0, moving.Length); } while (rand == prevRand);
                prevRand = rand;

                PlayAudio(moving, move);
                nextFootstepChase = Time.time + chase;
            }
        }
    }

    public void PlayDoor()
    { 
        move.pitch = Random.Range(0.9f, 1.1f);
        move.PlayOneShot(doorBreaking);
    }

    public void PlayAudio(AudioClip[] array, AudioSource source)
    {
        int index = Random.Range(0, array.Length);

        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(array[index]);
    }

    public void StopAudio() => talk.Stop();
    public bool AudioPlaying() => talk.isPlaying;
}
