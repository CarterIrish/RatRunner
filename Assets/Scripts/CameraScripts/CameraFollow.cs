using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform camTarget;
    [SerializeField] private float pLerp = .1f;
    [SerializeField] private float rLerp = .1f;
    [SerializeField] private Vector3 positionOffset = new Vector3(0, 3, 3);


    // FixedUpdate is called in sync with physics updates
    void FixedUpdate()
    {
        // have camera follow targets position with offset
        Vector3 targetPosition = camTarget.position + camTarget.TransformDirection(positionOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, pLerp);

        // have camera follow targets rotations with 180-degree offset
        Quaternion targetRotation = camTarget.rotation * Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rLerp);
    }
}
