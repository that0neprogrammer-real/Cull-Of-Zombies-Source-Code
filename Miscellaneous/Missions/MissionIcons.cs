using UnityEngine;
using DG.Tweening;

public class MissionIcons : MissionConfigurations
{ 
    [SerializeField] private MissionManager func;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] icons;

    private void Update()
    {
        if (playerState.curerntlyOnMission)
        {
            switch (func.GetMissionIndex())
            {
                case 0:
                    LookAt(icons[0]);
                    break;
                case 1:
                    Mission2();
                    break;
                case 2:
                    LookAt(icons[2]);
                    break;
                case 3:
                    LookAt(icons[3]);
                    break;
                default:
                    Debug.Log("Invalid Level");
                    break;
            }
        }
    }

    private void Mission2()
    {
        foreach (Transform icon in icons[1]) LookAt(icon);
    }

    private void LookAt(Transform icon) => icon.DOLookAt(player.position, 0.15f, AxisConstraint.Y, Vector3.up);
}
