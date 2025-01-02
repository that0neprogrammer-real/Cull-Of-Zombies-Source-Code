using UnityEngine;

public class CustomTag : MonoBehaviour
{
    public CustomTags customTag;

    public CustomTag.CustomTags GetTag()
    {
        return customTag;
    }

    public enum CustomTags
    {
        none,
        entity,
        ground,
        wood,
        concrete,
    }
}