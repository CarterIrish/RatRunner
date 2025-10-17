using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using NUnit.Framework.Constraints;





/// <summary>
/// This enum serves to hold all core game loop states
/// </summary>
public enum GameStates
{
    PLAYING,
    PAUSED,
}

/// <summary>
/// Singleton GameManager class serves to manage core game loop
/// </summary>
public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Gets the singleton instance.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static GameManager Instance { get; private set; }

    // Pause menu events
    public static UnityEvent OnGamePaused = new UnityEvent();
    public static UnityEvent OnGameResumed = new UnityEvent();


    [SerializeField]
    private GameStates _gameState = GameStates.PLAYING; 
    /// <summary>
    /// Gets the state of the game.
    /// </summary>
    /// <value>
    /// The state of the game.
    /// </value>
    public GameStates GameState
    {
        get { return _gameState; }
    }



    // Player win/lose condition
    public bool PlayerEscaped { get ; private set; } = false;


    private void Awake()
    {
        // Check if an instance of the GameManager exists
        if (Instance == null)
        {
            // If empty assign this
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // else destroy the gameObject
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_gameState != GameStates.PLAYING)
        {
            _gameState = GameStates.PLAYING;
        }
    }

    // Update is called once per frame
    void Update()
    {
       


        // Gamestate machine
        switch (_gameState)
            {
                case GameStates.PLAYING:
                    HandlePlayingState();
                    break;
                case GameStates.PAUSED:
                    HandlePausedState();
                    break;
            }

    }

    private void HandlePlayingState()
    {
        // update day timer eventually
        // check any game over conditions
    }

    private void HandlePausedState()
    {
    }

    /// <summary>       Her
    /// Changes the state of the game.
    /// </summary>
    /// <param name="newState">The new state.</param>
    public void  ChangeGameState(GameStates newState)
    {
        // Change the state
        _gameState = newState;

        // Call appropriate method based on new state
        switch (newState)
        {
            case GameStates.PAUSED:
                PauseGame();
                break;
            case GameStates.PLAYING:
                ResumeGame();
                break;
        }
    }

    // Resumes the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnGameResumed.Invoke();
    }

    // Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused.Invoke();
    }

    // Quits to menu
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Public method for UI buttons to resume the game
    /// </summary>
    public void ResumeFromUI()
    {
        ChangeGameState(GameStates.PLAYING);
    }

    /// <summary>
    /// Public method for UI buttons to pause the game
    /// </summary>
    public void PauseFromUI()
    {
        ChangeGameState(GameStates.PAUSED);
    }

    public void SetPlayerEscaped(bool escaped)
    {
        PlayerEscaped = escaped;
    }

    public void ResetGameOverState()
    {
        PlayerEscaped = false;  
    }

}


