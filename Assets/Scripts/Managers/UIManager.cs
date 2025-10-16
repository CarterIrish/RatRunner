using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  Singleton UI manager instance
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static UIManager Instance { get; private set; }

    // Reference to the pauseUI
    public GameObject pauseUI;

    private void Awake()
    {
        // Check if an instance of the UI manager exists
        if (Instance == null)
        {
            // If empty assign this
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            // else destroy the gameObject
            Destroy(gameObject);
            return; // Exit early to prevent further initialization
        }
    }

    private void OnEnable()
    {
        // Only subscribe if this is the singleton instance
        if (Instance == this)
        {
            GameManager.OnGamePaused.AddListener(ShowPauseUI);
            GameManager.OnGameResumed.AddListener(HidePauseUI);
        }
    }

    private void OnDisable()
    {
        // Only unsubscribe if this is the singleton instance
        if (Instance == this)
        {
            GameManager.OnGamePaused.RemoveListener(ShowPauseUI);
            GameManager.OnGameResumed.RemoveListener(HidePauseUI);
        }
    }

    /// <summary>
    /// Loads the scene.
    /// </summary>
    /// <param name="sceneName">Name of the scene.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Loads a new game.
    /// </summary>
    /// <param name="sceneName">Name of the scene.</param>
    public void LoadSceneNewGame(string sceneName)
    {
        SaveSystem.DeleteGameData();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Shows the pause UI.
    /// </summary>
    public void ShowPauseUI()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the pause UI.
    /// </summary>
    public void HidePauseUI()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }
}