using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(MonoBehaviour), true)]
public class DisplayVectors : Editor
{
    /*
    SerializedProperty radius;

    private void OnEnable()
    {
        radius = serializedObject.FindProperty("radius");
    }


    private void OnSceneGUI()
    {
        MonoBehaviour entity = (MonoBehaviour)target;

        if (entity.transform != null)
        {
            DisplayRadius(entity);
        }
    }

    private void DisplayRadius(MonoBehaviour entity)
    {
        Vector3 currentPosition = entity.transform.position;
        Handles.color = Color.white;

        Handles.DrawWireArc(currentPosition, Vector3.up, Vector3.forward, 360, radius.floatValue);
    }
    */
}
