using UnityEngine;

[System.Serializable]
public class BobbingConfigurations
{
    [SerializeField] private GameObject[] gameObjects;

    public float smooth;
    public float swayMultiplier;

    public void Movement(Transform position, Vector3 movement)
    {
        position.localPosition += movement * Time.deltaTime;
    }

    public Vector3 BobbingMovement(float amp, float freq)
    {
        Vector3 position = Vector3.zero;

        position.x += Mathf.Cos(Time.time * freq / 2) * amp / 2;
        position.y += Mathf.Sin(Time.time * freq) * amp;
        position.z = 0f;
        return position;
    }

    public Transform GetActiveGameObject()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].activeSelf)
                return gameObjects[i].transform.GetChild(0);
        }

        return null;
    }
}
