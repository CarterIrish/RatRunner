using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform camTarget;
    public float pLerp = .02f;
    public float rLerp = .01f;


    // Update is called once per frame
    void Update()
    {
        // have camera follow targets position
        transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);

        // have camera follow targets rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rLerp);
    }
}
