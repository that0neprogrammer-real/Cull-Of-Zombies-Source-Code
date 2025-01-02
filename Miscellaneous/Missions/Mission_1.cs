using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_1 : MissionConfigurations
{
    [Space] [SerializeField] private ZombieSpawner spawner;
    [SerializeField] private AmbienceSoundHandler sound;
    private bool destroy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || 
            manager.CurrentMission(data.missionIndex) || playerState.curerntlyOnMission) return;

        StartMission();
    }

    private void Update()
    {
        if (!destroy && spawner.NoMoreZombies())
        {
            sound.PlayHordeMusic();
            destroy = true;
            EndMission();
        }
    }

    protected override void StartMission()
    {
        base.StartMission();
        spawner.Spawn(false);
    }
}
