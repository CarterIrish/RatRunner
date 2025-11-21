
using System;
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
    private float baseSpeed = 10.0f; // Store original speed

    [SerializeField]
    private float turningDirection = 90.0f;
    private float baseTurningDirection = 90.0f; // Store original turning speed

    private Vector3 velocity;
    private Quaternion turning;


    // Physics Settings
    [SerializeField]
    private float gravityForce = 10.0f;
    public bool EnableGravity = true;

    public Rigidbody playerBody;

    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        // grab rigidbody from _currentPlayer
        playerBody = GetComponent<Rigidbody>();
        EnableGravity = true;

        // Store base values
        baseSpeed = speed;
        baseTurningDirection = turningDirection;

        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Adds a speed bonus to the player's movement.
    /// </summary>
    public void AddSpeedBonus(float bonus)
    {
        speed = baseSpeed + bonus;
    }

    /// <summary>
    /// Adds a turning speed bonus to the player.
    /// </summary>
    public void AddTurningBonus(float bonus)
    {
        turningDirection = baseTurningDirection + bonus;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
        // apply artificial gravity
        if(EnableGravity)
        {
            Vector3 gravity = -transform.up * gravityForce;
            playerBody.AddForce(gravity, ForceMode.Force);
        }
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
            animator.enabled = true;
            velocity = -transform.forward * speed * Time.fixedDeltaTime;
            transform.position += velocity;
        }
        else
        {
            animator.enabled = false;
        }
    }

}
