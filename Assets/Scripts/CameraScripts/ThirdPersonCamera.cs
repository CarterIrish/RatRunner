using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform pivot;

    [SerializeField]
    private InputActionAsset inputActions;
    private InputAction lookAction;
    private InputAction zoomAction;

    [SerializeField]
    private float distance;
    [SerializeField]
    private float height;
    [SerializeField]
    private float mouseSens;
    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    private float yaw;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        InputActionMap player = inputActions.FindActionMap("Player");
        lookAction = player.FindAction("Look");
        zoomAction = player.FindAction("Zoom");

        if (pivot==null) pivot = GameObject.FindGameObjectWithTag("pivot").transform;
        yaw = pivot.eulerAngles.y;
        distance = 5f;
        height = 2f;
        mouseSens = 100f;
        zoomSpeed = 100f;
        minDistance = 2f;
        maxDistance = 15f;
    }

    private void LateUpdate()
    {
        // Get look input
        if (lookAction != null)
        {
            Vector2 lookDelta = lookAction.ReadValue<Vector2>();
            yaw += lookDelta.x * mouseSens * Time.deltaTime;
            pitch -= lookDelta.y * mouseSens * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -20f, 80f);
        }

        // Get zoom input
        if (zoomAction != null)
        {
            float scroll = zoomAction.ReadValue<float>();
            distance -= scroll * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }


        // Calc cam position
        Vector3 offset = new Vector3(
            Mathf.Sin(yaw * Mathf.Deg2Rad) * distance,
            height + Mathf.Sin(pitch * Mathf.Deg2Rad) * distance,
            Mathf.Cos(yaw * Mathf.Deg2Rad) * distance
            );

        // Check cam clipping into wall
        Vector3 desiredPos = pivot.position + offset;
        RaycastHit hit;
        if(Physics.Raycast(pivot.position, offset.normalized, out hit, distance))
        {
            transform.position = hit.point - offset.normalized * 0.2f;
        }
        else
        {
            transform.position = desiredPos;
        }
        transform.LookAt(pivot);
    }

}
