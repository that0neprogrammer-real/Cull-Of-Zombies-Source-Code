using System.Collections;
using UnityEngine;

public class AmbienceSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAmbience;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource hordeWaveMusic;
    [SerializeField] private AudioClip deathMusic;
    [SerializeField] private AudioClip rescueMusic;

    [Space]
    [SerializeField] private AudioClip waveStart;
    [SerializeField] private AudioClip hordeMusic;
    [SerializeField] private AudioClip[] music;

    private int prevIndex = -1;
    private float randomTimer = 0f;
    private bool waveStarted = false;

    private void Start()
    {
        randomTimer = Random.Range(3f, 5f);
        backgroundMusic.PlayOneShot(RandomMusic());
    }

    private void Update()
    {
        if (!backgroundMusic.isPlaying && !waveStarted)
        {
            if (randomTimer > 0f) randomTimer -= Time.deltaTime;
            else
            {
                backgroundMusic.PlayOneShot(RandomMusic());
                randomTimer = Random.Range(3f, 5f);
            }
        }
    }

    private AudioClip RandomMusic()
    {
        int rand;
        do { rand = Random.Range(0, music.Length); }
        while (prevIndex == rand);

        prevIndex = rand;
        return music[rand];
    }

    public void PlayDeathMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.PlayOneShot(deathMusic);
    }

    public void PlayRescueMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.PlayOneShot(rescueMusic);
    }

    public void PlayHordeMusic() => hordeWaveMusic.PlayOneShot(waveStart);

    public void StopHordeMusic() => hordeWaveMusic.Stop();

    public void StopBackground() => backgroundMusic.Stop();

    public IEnumerator PlayNewWaveMusic(bool isTrue)
    {
        waveStarted = isTrue;

        backgroundMusic.Stop();
        hordeWaveMusic.Stop();
        hordeWaveMusic.PlayOneShot(waveStart);

        yield return new WaitForSeconds(5f);
        hordeWaveMusic.Play();
    }
}
