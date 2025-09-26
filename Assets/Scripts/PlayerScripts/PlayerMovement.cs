using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // input variables
    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool isTurningRight;
    [HideInInspector]
    public bool isTurningLeft;

    // Move Settings
    [Header("Move Settings")]

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float turningDirection = 90.0f;

    private Vector3 velocity;
    private Quaternion turning;


    // Physics Settings
    [SerializeField]
    private float gravityForce = 10.0f;

    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        // grab rigidbody from player
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // apply artificial gravity
        Vector3 gravity = -transform.up * gravityForce;
        playerBody.AddForce(gravity, ForceMode.Force);

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
