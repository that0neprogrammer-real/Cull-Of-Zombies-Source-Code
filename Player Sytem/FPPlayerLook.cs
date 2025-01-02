using UnityEngine;

public class FPPlayerLook : MonoBehaviour
{
    public static FPPlayerLook Instance;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform player;

    [Space]
    [SerializeField] private bool canLook;
    [SerializeField] private float sensitivity;
    [SerializeField] private int maxClamp;

    public Vector2 mouseInput { get; private set; }
    private float xRotation;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sensitivity = (PlayerPrefs.GetFloat("CurrentSensitivity") * 100f);
    }

    void Update()
    {
        if (!canLook)
            return;

        float sens = PlayerPrefs.GetFloat("CurrentSensitivity");
        sens *= 100f;
        sensitivity = sens;

        mouseInput = new(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        float mouseX = mouseInput.x * (Time.deltaTime * sens);
        float mouseY = mouseInput.y * (Time.deltaTime * sens);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxClamp, maxClamp);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    public void CanPlayerLook(bool value) => canLook = value;
}