using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ScriptableObjects/Objects")]
public class ObjectData : ScriptableObject
{
    public new string name;
    public float entityHealth;
    public ObjectType interactType;

    public enum ObjectType
    {
        press,
        hold,
        release
    }
}
