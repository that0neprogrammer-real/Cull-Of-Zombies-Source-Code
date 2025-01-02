using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class Mission_4 : MissionConfigurations
{
    [Space][SerializeField] private AmbienceSoundHandler music;
    [SerializeField] private GasGenerator radio;
    [SerializeField] private HueyTrigger helicopter;
    [SerializeField] private ReturnToMenu menu;
    [SerializeField] private int maxWaves;

    [SerializeField] private GameObject[] endScreen;
    [SerializeField] private ZombieSpawner[] spawners;
    [SerializeField] private ZombieSpawner[] brutes;

    private int currentWave = 0;
    private int maxHordeSpawns = 3;
    private bool hasActivated = false, fade = false, hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") ||
            manager.CurrentMission(data.missionIndex) || playerState.curerntlyOnMission) return;

        StartMission();
    }

    private void Update()
    {
        CheckForInputs();

        if (currentWave == maxWaves)
        {
            if (!helicopter.gameObject.activeSelf) helicopter.gameObject.SetActive(true);
            if (!hasPlayed)
            {
                music.StopHordeMusic();
                music.PlayRescueMusic();
                hasPlayed = true;
            }

            if (helicopter.take0ff && !fade) 
            {
                StartCoroutine(EndScreen());
                SpawnAllZombies();
                fade = false;
            }
        }
    }

    private void SpawnAllZombies()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i].NoMoreZombies()) spawners[i].Respawn();
            spawners[i].Spawn(true);
        }

        for (int i = 0; i < brutes.Length; i++)
        {
            if (brutes[i].NoMoreZombies()) brutes[i].Respawn();
            brutes[i].Spawn(true);
        }
    }

    private IEnumerator EndScreen()
    {
        missionIcon.SetActive(false);
        endScreen[0].SetActive(false);
        endScreen[1].SetActive(true);
        endScreen[2].SetActive(false);
        yield return null;

        StartCoroutine(menu.StartEndScreen());
    }

    private void CheckForInputs()
    {
        if (radio.HasInteracted() && !hasActivated)
        {
            StartCoroutine(StartWave());
            hasActivated = true;
        }
    }

    private IEnumerator StartWave()
    {
        while (currentWave < maxWaves) //Current Wave of zombies
        {
            StartCoroutine(music.PlayNewWaveMusic(true));

            for (int i = 1; i <= maxHordeSpawns; i++) //Number of hordes per wave
            {
                if (i == 3)
                {
                    SpawnBoss();
                    yield return new WaitUntil(() => brutes[0].NoMoreZombies());

                    brutes[0].Respawn();
                    break;
                }
                else SpawnHorde();

                yield return new WaitUntil(() => HordeCleared());
                RespawnHorde();
            }

            currentWave++;
        }
    }

    private void SpawnBoss()
    {
        for (int i = brutes.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);

            ZombieSpawner temp = brutes[i];
            brutes[i] = brutes[rand];
            brutes[rand] = temp;
        } //Fisher-Yates Algorithm

        brutes[0].Spawn(true);
    }

    private void SpawnHorde()
    {
        for (int i = spawners.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);

            ZombieSpawner temp = spawners[i];
            spawners[i] = spawners[rand];
            spawners[rand] = temp;
        } //Fisher-Yates Algorithm

        for (int i = 0; i < maxHordeSpawns; i++) spawners[i].Spawn(true);
    }

    private void RespawnHorde()
    {
        for (int i = 0; i < maxHordeSpawns; i++)
            spawners[i].Respawn();
    }

    private bool HordeCleared() => spawners[0].NoMoreZombies()
    && spawners[1].NoMoreZombies() && spawners[2].NoMoreZombies();
}
