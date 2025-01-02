using UnityEngine;

public class GenSoundAnim : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public bool activate = false;
    private bool rotationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activate)
            return;

        if (!rotationStarted)
        {
            source.Play();
            rotationStarted = true;
        }

        transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
    }

}
