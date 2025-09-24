using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // input variables
    public bool isMoving;
    public bool isTurningRight;
    public bool isTurningLeft;

    // moving variables
    private float speed = 10.0f;
    private Vector3 velocity;
    private Quaternion turning;
    private float turningDirection = 90.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // turn right if input is received
        if (isTurningRight)
        {
            turning = Quaternion.Euler(0, turningDirection * Time.fixedDeltaTime, 0);
            transform.rotation *= turning;
        }

        // turn left if input is received
        if (isTurningLeft)
        {
            turning = Quaternion.Euler(0, -(turningDirection * Time.fixedDeltaTime), 0);
            transform.rotation *= turning;
        }

        // move if input is received
        if (isMoving)
        {
            velocity = transform.forward * speed * Time.fixedDeltaTime;
            transform.position += velocity;
        }
    }
}
