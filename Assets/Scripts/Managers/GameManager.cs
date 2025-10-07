using UnityEngine;
using UnityEngine.SceneManagement;




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
    public GameObject pauseMenuUI;

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _gameState = GameStates.PAUSED;
        }

        if (_gameState == GameStates.PAUSED)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }


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

    //resumes the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        _gameState = GameStates.PLAYING;
    }

    //pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    //quits to menu
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    #endregion
}


