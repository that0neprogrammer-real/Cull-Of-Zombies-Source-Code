using UnityEngine;

public class FPHeadBob : MonoBehaviour
{
    [Space]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private bool isBobbing = true;
    
    [Space]
    [SerializeField] private float[] walkingValues;
    [SerializeField] private float[] crouchingValues;
    [SerializeField] private float[] runningValues;

    private Vector3 originalPosition;
    private float amp, freq;

    void Start()
    {
        originalPosition = cameraHolder.localPosition;
    }

    void Update()
    {
        if (!isBobbing || !FPMovement.Instance.CanPlayerMove())
        {
            ResetPosition();
            return;
        }
            

        if (FPMovement.Instance.playerController.isGrounded)
            BobbingMovement();

        ResetPosition();
    }

    private void BobbingMovement()
    {

        switch (FPMovement.Instance.playerState)
        {
            case FPMovement.MovementState.Idle:
                amp = 0f;
                freq = 0f;
                break;
            case FPMovement.MovementState.Walking:
                amp = walkingValues[0];
                freq = walkingValues[1];
                break;
            case FPMovement.MovementState.Crouching:
                amp = crouchingValues[0];
                freq = crouchingValues[1];
                break;
            case FPMovement.MovementState.Running:
                amp = runningValues[0];
                freq = runningValues[1];
                break;
        }

        Movement(cameraHolder, 
            HeadMovement(amp, freq));
    }

    public void Movement(Transform position, Vector3 movement)
    {
        position.localPosition += movement * Time.deltaTime;
    }

    public Vector3 HeadMovement(float amp, float freq)
    {
        Vector3 position = Vector3.zero;

        position.x += Mathf.Cos(Time.time * freq / 2) * amp / 2;
        position.y += Mathf.Sin(Time.time * freq) * amp;
        position.z = 0f;
        return position;
    }

    private void ResetPosition()
    {
        if (cameraHolder.localPosition == originalPosition) 
            return;

        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, originalPosition, Time.deltaTime * 10f);
    }
}
