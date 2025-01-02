using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "ScriptableObjects/Missions")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public int missionIndex;
    public string[] subMissions;
}
