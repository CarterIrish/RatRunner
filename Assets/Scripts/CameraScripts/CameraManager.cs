using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Profiles")]
    public ThirdPersonCamera thirdPersonCam;
    public FirstPersonCamera firstPersonCam;
    public FixedCamera fixedCam;

    [Header("Camera Settings")]
    public CameraMode startingMode = CameraMode.ThirdPerson;

    [SerializeField]
    private CameraMode currentMode;

    public enum CameraMode
    {
        ThirdPerson,
        FirstPerson,
        Fixed
    }

    private void Start()
    {
        // Check if assigned, if not grab automatically
        if (thirdPersonCam == null) thirdPersonCam = GetComponent<ThirdPersonCamera>();
       
        if(firstPersonCam == null) firstPersonCam = GetComponent<FirstPersonCamera>();
        
        if(fixedCam == null) fixedCam = GetComponent<FixedCamera>();

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
        switch (newMode)
        {
            case CameraMode.ThirdPerson:
                fixedCam.enabled = false;
                firstPersonCam.enabled = false;
                thirdPersonCam.enabled = true;
                break;
            case CameraMode.FirstPerson:
                fixedCam.enabled = false;
                firstPersonCam.enabled = true;
                thirdPersonCam.enabled = false;
                break;
            case CameraMode.Fixed:
                fixedCam.enabled = true;
                firstPersonCam.enabled = false;
                thirdPersonCam.enabled = false;
                break;
        }

        currentMode = newMode;
    }

    /// <summary>
    /// Cycles the camera mode.
    /// </summary>
    private void CycleCameraMode()
    {
        currentMode = (CameraMode)(((int)currentMode + 1) % 3);
        SetCameraMode(currentMode);
    }
}
