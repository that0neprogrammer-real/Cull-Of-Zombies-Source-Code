using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_2 : MissionConfigurations
{
    [Space][SerializeField] private AmbienceSoundHandler sound;
    [SerializeField] private bool[] generatorsActivated;
    [SerializeField] private GasGenerator[] generators;
    [SerializeField] private ZombieSpawner[] spawners;
    [SerializeField] private Light[] lights;
    [SerializeField] private GenSoundAnim[] cogs;
    [SerializeField] private GameObject[] icons;
    private bool destroy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") ||
            manager.CurrentMission(data.missionIndex) || playerState.curerntlyOnMission) return;

        StartMission();
    }

    private void Update()
    {
        if (!destroy && GeneratorsActivated())
        {
            sound.PlayHordeMusic();
            destroy = true;
            EndMission();
        }

        if (!GeneratorsActivated()) CheckGenerators();
    }

    private void CheckGenerators()
    {
        for (int i = 0; i < generatorsActivated.Length; i++)
        {
            if (!generatorsActivated[i] && generators[i].HasInteracted()) // If the boolean variable hasn't been set and the generator has been interacted, perform the task
            {
                generatorsActivated[i] = true;
                lights[i].enabled = true;
                cogs[i].activate = true;
                icons[i].SetActive(false);

                sound.PlayHordeMusic();
                sound.StopBackground();
                spawners[i].Spawn(true);
            }
        }
    }

    private bool GeneratorsActivated()
    {
        for (int i = 0; i < generatorsActivated.Length; i++)  if (!generatorsActivated[i]) return false;
        return true;
    }
}
