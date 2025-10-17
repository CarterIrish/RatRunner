
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [Header("Camera Profiles")]
    public ThirdPersonCamera thirdPersonCam;
    public FirstPersonCamera firstPersonCam;
    public FixedCamera fixedCam;
    public CameraFollow followCam;

    [Header("Camera Settings")]
    public CameraMode startingMode = CameraMode.ThirdPerson;

    [Header("Hierarchy Management")]
    [SerializeField] private Transform playerTransform;

    [SerializeField]
    private CameraMode currentMode;

    public enum CameraMode
    {
        ThirdPerson,
        FirstPerson,
        Fixed,
        Follow
    }

    private void Start()
    {
        // Check if assigned, if not grab automatically
        if (thirdPersonCam == null) thirdPersonCam = GetComponent<ThirdPersonCamera>();

        if(firstPersonCam == null) firstPersonCam = GetComponent<FirstPersonCamera>();

        if(fixedCam == null) fixedCam = GetComponent<FixedCamera>();

        if(followCam == null) followCam = GetComponent<CameraFollow>();

        // Auto-find player if not assigned
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        // Initalize starting cam profile
        SetCameraMode(startingMode);
    }

    private void Update()
    {
        // Temporary camera cycle; this will move to setting menu eventually
        if(Input.GetKeyDown(KeyCode.C))
        {
            CycleCameraMode();
        }
    }

    /// <summary>
    /// Sets the camera mode.
    /// </summary>
    /// <param name="newMode">The new mode.</param>
    private void SetCameraMode(CameraMode newMode)
    {
        // Handle hierarchy changes based on camera mode
        if (newMode == CameraMode.Follow)
        {
            // Follow mode: camera needs to be unparented (sibling to player in scene)
            transform.SetParent(null);
        }
        else
        {
            // Other modes: camera needs to be child of player
            if (playerTransform != null && transform.parent != playerTransform)
            {
                transform.SetParent(playerTransform);
            }
        }

        // Enable/disable appropriate camera profile
        switch (newMode)
        {
            case CameraMode.ThirdPerson:
                fixedCam.enabled = false;
                firstPersonCam.enabled = false;
                followCam.enabled = false;
                thirdPersonCam.enabled = true;
                break;
            case CameraMode.FirstPerson:
                fixedCam.enabled = false;
                firstPersonCam.enabled = true;
                followCam.enabled = false;
                thirdPersonCam.enabled = false;
                break;
            case CameraMode.Fixed:
                fixedCam.enabled = true;
                firstPersonCam.enabled = false;
                followCam.enabled = false;
                thirdPersonCam.enabled = false;
                break;
            case CameraMode.Follow:
                fixedCam.enabled = false;
                firstPersonCam.enabled = false;
                thirdPersonCam.enabled = false;
                followCam.enabled = true;
                break;
        }

        currentMode = newMode;
    }

    /// <summary>
    /// Cycles the camera mode.
    /// </summary>
    private void CycleCameraMode()
    {
        currentMode = (CameraMode)(((int)currentMode + 1) % 4);
        SetCameraMode(currentMode);
    }
}
