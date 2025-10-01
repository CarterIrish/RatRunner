using UnityEngine;




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

    /// <summary>
    /// Changes the state of the game.
    /// </summary>
    /// <param name="newState">The new state.</param>
    public void ChangeGameState(GameStates newState)
    {
        _gameState = newState;
    }

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

    #region Gameplay Loop Methods Below
    //TODO: Add playing state logic
    private void HandlePlayingState() { }

    //TODO: Add paused state logic
    private void HandlePausedState() { }
    #endregion
}


