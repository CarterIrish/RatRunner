using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (playerScript == null)
        {
            Debug.Log("Missing player script.");
        }
    }

    /// <summary>
    /// tell player to move when "w" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerScript.isMoving = true;
        }

        if (context.canceled)
        {
            playerScript.isMoving = false;
        }
    }

    /// <summary>
    /// tell player to turn right when "d" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnTurnRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerScript.isTurningRight = true;
        }

        if (context.canceled)
        {
            playerScript.isTurningRight = false;
        }
    }

    /// <summary>
    /// tell player to turn left when "a" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnTurnLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerScript.isTurningLeft = true;
        }

        if (context.canceled)
        {
            playerScript.isTurningLeft = false;
        }
    }
}
