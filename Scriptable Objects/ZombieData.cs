using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie", menuName = "ScriptableObjects/Zombie")]
public class ZombieData : ScriptableObject
{
    public ZombieType zombieType;

    [Space] [Header("Behavior Properties")]
    public int damage;
    public float viewRadius;

    [Space]
    public float normalSpeed;
    public float chasingSpeed;
    public float roamingRadius; //how much can the zombie travel
    public Vector2 idleTime; //x - minimum time, y - maximum time


    public enum ZombieType
    {
        normal,
        runners,
        shrieks,
        prowlers,
        brutes
    }
}

