using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction lookAction;
    private InputAction zoomAction;

    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float mouseSens;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private float yaw;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        InputActionMap playerMap = inputActions.FindActionMap("Player");
        lookAction = playerMap.FindAction("Look");
        zoomAction = playerMap.FindAction("Zoom");

        if (pivotTransform==null) pivotTransform = GameObject.FindGameObjectWithTag("pivotTransform").transform;
        if (playerTransform == null) playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        yaw = playerTransform.eulerAngles.y;
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

        if(playerTransform != null)
        {
            playerTransform.rotation = Quaternion.Euler(0, yaw, 0);
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
        Vector3 desiredPos = pivotTransform.position + offset;
        RaycastHit hit;

        // Ignore Player layer to prevent raycast from hitting the player's own colliders
        int layerMask = ~LayerMask.GetMask("Player");

        if(Physics.Raycast(pivotTransform.position, offset.normalized, out hit, offset.magnitude, layerMask))
        {
            // Use hit.distance to stay at safe distance from wall
            float safeDistance = Mathf.Max(0.2f, hit.distance - 0.2f);
            transform.position = pivotTransform.position + offset.normalized * safeDistance;
        }
        else
        {
            transform.position = desiredPos;
        }
        transform.LookAt(pivotTransform);
    }

}
