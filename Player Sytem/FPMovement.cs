using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPMovement : MonoBehaviour
{
    #region
    public bool IsGrounded => playerController.isGrounded;
    public bool IsRunning => Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftControl);
    public bool IsCrouching => Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift);
    public bool IsJumping => Input.GetKeyDown(KeyCode.Space) && IsGrounded;
    #endregion

    public static FPMovement Instance;
    [SerializeField] private FPConfigurations playerConfigs;

    [Space] public CharacterController playerController;
    [SerializeField] private Transform playerCamera;

    [Space]
    public MovementState playerState;
    public float currentSpeed;
    [HideInInspector] public float currSpd;

    private float currentVelocityY;
    private float standingHeight = 2.0f;
    private float crouchHeight = 1.0f;

    public enum MovementState
    {
        Idle,
        Walking,
        Crouching,
        Running,
        Air
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!playerConfigs.canMove)
            return;

        currentVelocityY = playerController.isGrounded && currentVelocityY < 0f ? 
            -1f : currentVelocityY + playerConfigs.gravity * Time.deltaTime;

        GetInput();
        Jump();
        Crouch();
    }

    private void GetInput()
    {
        Vector2 input = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 playerInput = new(input.x, 0f, input.y);
        if (playerInput.magnitude > 1) playerInput.Normalize();

        MoveController(playerInput);
    }

    private void MoveController(Vector3 input)
    {
        SetSpeed(input);
        Vector3 movementDirection = transform.TransformDirection(input);

        movementDirection *= currentSpeed;
        movementDirection.y = currentVelocityY;
        movementDirection *= Time.deltaTime;

        playerController.Move(movementDirection);
    }

    private void SetSpeed(Vector3 io)
    {
        currSpd = io.sqrMagnitude;
        bool notRC = !IsRunning && !IsCrouching;

        if (currSpd > 0.05f && notRC) //player walking
        {
            currentSpeed = playerConfigs.baseSpeed;
            playerState = MovementState.Walking;
        }
        else if (IsRunning)
        {
            currentSpeed = playerConfigs.runningSpeed;
            playerState = MovementState.Running;
        } 
        else if (IsCrouching)
        {
            currentSpeed = playerConfigs.crouchSpeed;
            playerState = MovementState.Crouching;
        }
        else if (currSpd < 0.05f && notRC) //player standing
        {
            currentSpeed = 0f;
            playerState = MovementState.Idle;
        }
    }

    private void Jump()
    {
        if (IsJumping)
        {
            float yVelocity = Mathf.Sqrt(playerConfigs.jumpHeight * -2 * playerConfigs.gravity);
            currentVelocityY = yVelocity;
        }
    }

    private void Crouch()
    {
        float targetHeight = IsCrouching ? crouchHeight : standingHeight;

        if (playerController.height == targetHeight)
            return;

        AdjustControllerHeight(targetHeight);
        UpdateCameraPosY(IsCrouching);
    }

    #region
    public float GetSpeed(int num)
    {
        float[] values = { 0, playerConfigs.baseSpeed, playerConfigs.crouchSpeed, playerConfigs.runningSpeed };
        return values[num];
    }
    public void AdjustSpeed(float num, bool activate)
    {
        if (activate) playerConfigs.IncreaseSpeed(num);
        else  playerConfigs.DecreaseSpeed(num);
    }
    private void AdjustControllerHeight(float height)
    {
        float center = height / 2;
        playerController.height = Mathf.Lerp(playerController.height, height, Time.deltaTime * 5f);
        playerController.center = Vector3.Lerp(playerController.center, new(0, center, 0), Time.deltaTime * 5f);
    }
    private void UpdateCameraPosY(bool IsCrouching)
    {
        float targetY = IsCrouching ? 0.8f : 1.6f;
        playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, Mathf.Lerp(playerCamera.localPosition.y, targetY, Time.deltaTime * 6f), playerCamera.localPosition.z);
    }
    public bool IsPlayerMoving() => currSpd > 0.05f;
    public void SetPlayerMove(bool value) => playerConfigs.canMove = value;
    public bool CanPlayerMove() => playerConfigs.canMove;
    #endregion
}
