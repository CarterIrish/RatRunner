using System.Linq;
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
        playerMap.Enable();
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

        if (context.started && GameManager.Instance?.GameState == GameStates.PLAYING)
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

        if (context.started && GameManager.Instance?.GameState == GameStates.PLAYING)
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

        if (context.started && GameManager.Instance?.GameState == GameStates.PLAYING)
        {
            Debug.Log("Turn Left");
            playerScript.isTurningLeft = true;
        }
    }

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
