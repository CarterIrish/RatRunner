using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform pivot;
    //public Transform pivot;
    [SerializeField]
    float distance;
    [SerializeField]
    float height;
    [SerializeField]
    float mouseSens;
    [SerializeField]
    float zoomSpeed;
    [SerializeField]
    float minDistance;
    [SerializeField]
    float maxDistance;

    float yaw;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        //if(!pivot) pivot = GameObject.FindGameObjectWithTag("pivot").transform;
        yaw = pivot.eulerAngles.y;
        distance = 5f;
        height = 2f;
        mouseSens = 100f;
        zoomSpeed = 10f;
        minDistance = 2f;
        maxDistance = 15f;
    }

    private void LateUpdate()
    {                                                                               
        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            yaw += mouseDelta.x * mouseSens * Time.deltaTime;
            pitch -= mouseDelta.y * mouseSens * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -20f, 80f);
        }

        float scroll = Mouse.current.scroll.ReadValue().y;
        distance -= scroll * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);



        // Calc cam position
        Vector3 offset = new Vector3(
            Mathf.Sin(yaw * Mathf.Deg2Rad) * distance,
            height + Mathf.Sin(pitch * Mathf.Deg2Rad) * distance,
            Mathf.Cos(yaw * Mathf.Deg2Rad) * distance
            );

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
