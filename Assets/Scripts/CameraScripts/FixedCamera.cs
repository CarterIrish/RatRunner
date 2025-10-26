using UnityEngine;
using UnityEngine.InputSystem;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction zoomAction;

    [SerializeField] private Vector3 baseOffset = new Vector3(-0.06f, 8.16f, 4.42f);

    [SerializeField] private float zoomSpeed = 0.1f;

    [SerializeField] private float minZoom = -10f;

    [SerializeField] private float maxZoom = 10f;

    private float currentZoom = -3f;

    private void Start()
    {
        InputActionMap playerMap = inputActions.FindActionMap("Player");
        zoomAction = playerMap.FindAction("Zoom");
        currentZoom = baseOffset.z;

    }

    private void LateUpdate()
    {
        if(zoomAction != null)
        {
            float scroll = zoomAction.ReadValue<float>();
            currentZoom += scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        
        transform.localPosition = new Vector3(baseOffset.x, baseOffset.y, currentZoom);
        transform.localRotation = Quaternion.Euler(28, 180, 0);
    }






}