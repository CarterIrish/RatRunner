
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // Event for interact key press
    public static UnityEvent OnInteractPressed = new UnityEvent();

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
            Debug.Log("Missing _currentPlayer script.");
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
    /// tell _currentPlayer to move when "w" is pressed
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

            playerScript.isMoving = true;
        }
    }

    /// <summary>
    /// tell _currentPlayer to turn right when "d" is pressed
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

            playerScript.isTurningRight = true;
        }
    }

    /// <summary>
    /// tell _currentPlayer to turn left when "a" is pressed
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

            if (GameManager.Instance == null) return;
            //switch (GameManager.Instance.GameState)
            //{
            //    case (GameStates.START):
            //    {
            //        GameManager.Instance.ChangeGameState(GameStates.PLAYING);
            //        break;
            //    }
            //    case (GameStates.GAME_OVER):
            //    {
            //        GameManager.Instance.ChangeGameState(GameStates.START);
            //        break;
            //    }
            //}    
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
                case (GameStates.CRAFTING):
                {
                    GameManager.Instance.ChangeGameState(GameStates.PLAYING);
                    break;
                }
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

    /// <summary>
    /// Called when [interact pressed] (E key).
    /// Broadcasts to any listening objects (Workbench, NPCs, etc.)
    /// </summary>
    /// <param name="context">The context.</param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Invoke the event
            OnInteractPressed.Invoke();
        }
    }

}
