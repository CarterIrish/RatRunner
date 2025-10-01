using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerScript;

    [SerializeField]
    private InputActionAsset inputActions;

    private InputActionMap playerMap;
    private InputActionMap uiMap;

    
    // Start is called before the first frame update
    void Start()
    {
        if (playerScript == null)
        {
            Debug.Log("Missing player script.");
        }

        playerMap = inputActions.FindActionMap("Player");
        uiMap = inputActions.FindActionMap("UI");

        uiMap.Enable(); // Should always be enabled.

        // Set initial state based on GameManager
        if (GameManager.Instance?.GameState == GameStates.PLAYING)
        {
            playerMap.Enable();
        }
        else
        {
            playerMap.Disable(); // Ensure it starts disabled for menu states
        }
    }

    /// <summary>
    /// Runs on each unity update, ensures consistent
    /// input maps accross game states.
    /// </summary>
    private void Update()
    {
        // Check the game state to update input maps
        switch (GameManager.Instance?.GameState)
        {
            case GameStates.PLAYING:
                {
                    playerMap.Enable();
                    break;
                }
            default:
                {
                    playerMap.Disable();
                    break;
                }
        }

    }

    /// <summary>
    /// tell player to move when "w" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerScript.isMoving = false;
            return;
        }

        if (context.started)
        {
            Debug.Log("Player Moving");
            playerScript.isMoving = true;
        }
    }

    /// <summary>
    /// tell player to turn right when "d" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnTurnRight(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerScript.isTurningRight = false;
            return;
        }

        if (context.started)
        {
            Debug.Log("Turn Right");
            playerScript.isTurningRight = true;
        }
    }

    /// <summary>
    /// tell player to turn left when "a" is pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnTurnLeft(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            playerScript.isTurningLeft = false;
            return;
        }

        if (context.started)
        {
            Debug.Log("Turn Left");
            playerScript.isTurningLeft = true;
        }
    }

    /// <summary>
    /// Called when [enter pressed].
    /// </summary>
    /// <param name="context">The context.</param>
    public void OnEnterPressed(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("Enter Pressed");
            if (GameManager.Instance == null) return;
            switch (GameManager.Instance.GameState)
            {
                case (GameStates.START):
                {
                    GameManager.Instance.ChangeGameState(GameStates.PLAYING);
                    break;
                }
                case (GameStates.GAME_OVER):
                {
                    GameManager.Instance.ChangeGameState(GameStates.START);
                    break;
                }
            }    
        }
    }

    /// <summary>
    /// Called when [escape pressed].
    /// </summary>
    /// <param name="context">The context.</param>
    public void OnEscapePressed(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("Escape Pressed");
            if (GameManager.Instance == null) return;
            switch (GameManager.Instance.GameState)
            {
                case (GameStates.PLAYING):
                {
                    GameManager.Instance.ChangeGameState(GameStates.PAUSED);
                    break;
                }
                case (GameStates.PAUSED):
                {
                    GameManager.Instance.ChangeGameState(GameStates.PLAYING);
                    break;
                }
            }
        }
    }

}
