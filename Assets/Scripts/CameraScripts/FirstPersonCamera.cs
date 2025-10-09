using UnityEngine;
using UnityEngine.InputSystem;
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    private Transform pivotTransform;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction lookAction;

    [SerializeField]
    private float mouseSens = 100f;

    private float yaw;
    private float pitch;
    private Vector3 eyeOffset;


    private void Start()
    {
        if (inputActions == null)
        {
            throw new UnityException("Missing: InputActionAsset. Assign via inspector");
        }

        InputActionMap player = inputActions.FindActionMap("Player");
        lookAction = player.FindAction("look");

        eyeOffset = Vector3.up * 0.6f;
        yaw = playerTransform.eulerAngles.y;

    }

    private void LateUpdate()
    {
        if (lookAction == null) throw new UnityException("Missing: LookAction in playerTransform action map");

        Vector2 lookDelta = lookAction.ReadValue<Vector2>();
        yaw += lookDelta.x * mouseSens * Time.deltaTime;
        pitch -= lookDelta.y * mouseSens * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        transform.position = pivotTransform.position + eyeOffset;
        transform.localRotation = Quaternion.Euler(pitch, 180, 0);
        
        if(playerTransform != null)
        {
            playerTransform.rotation = Quaternion.Euler(0, yaw, 0);
        }

    }


}