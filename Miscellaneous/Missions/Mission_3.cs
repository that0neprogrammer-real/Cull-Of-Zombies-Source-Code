using System.Collections;
using UnityEngine;

public class Mission_3 : MissionConfigurations
{
    [Space][SerializeField] private CameraShake player;
    [SerializeField] private AmbienceSoundHandler sounds;
    [SerializeField] private TruckSound sound;
    [SerializeField] private Transform waypoint;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private ZombieSpawner spawner;
    [SerializeField] private GasGenerator[] interact; //gascan, gastank
    [SerializeField] private Transform[] missionObjects; //barricade, truck
    private bool destroy = false, hasGasCan = false, hasPlayedAudio = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") ||
            manager.CurrentMission(data.missionIndex) || playerState.curerntlyOnMission) return;

        StartMission();
    }

    private void Update()
    {
        if (!destroy && HasRammed())
        {
            sounds.PlayHordeMusic();
            sounds.StopBackground();
            destroy = true;
            EndMission();
        }

        CheckForInputs();
    }

    private void CheckForInputs()
    {
        if (!hasGasCan)
        {
            if (interact[0].HasInteracted())
            {
                interact[0].gameObject.SetActive(false);
                interact[1].gameObject.SetActive(true);
                hasGasCan = true;
            }
        }
        else if (hasGasCan && interact[1].HasInteracted())
        {
            if (!hasPlayedAudio)
            {
                StartCoroutine(PlaySounds());
                hasPlayedAudio = true;
            }
        }
    }

    private IEnumerator PlaySounds()
    {
        sound.PlayAudio();
        yield return new WaitForSeconds(1.25f);

        while (missionObjects[1].transform.position != waypoint.position)
        {
            missionObjects[1].transform.localPosition = Vector3.MoveTowards(missionObjects[1].position, waypoint.position, 5f * Time.deltaTime);
            yield return null;
        }

    }

    private bool HasRammed()
    {
        if (Vector3.Distance(missionObjects[1].position, waypoint.position) < 1.15f)
        {
            sound.StopAudio();
            Instantiate(explosionPrefab, transform);
            player.Shake(player.explosion);

            missionObjects[0].gameObject.SetActive(false);
            missionObjects[2].gameObject.SetActive(true);
            spawner.Spawn(true);
            return true;
        }

        return false;
    }

    protected override void StartMission()
    {
        base.StartMission();
        interact[0].gameObject.SetActive(true);
    }
}
