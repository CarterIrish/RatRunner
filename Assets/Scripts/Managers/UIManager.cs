using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject settingsUI;
    public GameObject workbenchUI;

    public GameObject dayUI;

    // Button references for dynamic callback assignment
    public Button resumeButton;

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

    private void Start()
    {
        // Dynamically assign button callbacks to persistent GameManager
        // This prevents "Missing" references when scenes reload
        if (resumeButton != null && GameManager.Instance != null)
        {
            // Clear any existing listeners to prevent duplicates
            resumeButton.onClick.RemoveAllListeners();
            // Add the callback to the persistent GameManager
            resumeButton.onClick.AddListener(() => GameManager.Instance.ResumeFromUI());
        }
    }

    private void OnEnable()
    {
        // Only subscribe if this is the singleton instance
        if (Instance == this)
        {
            GameManager.OnGamePaused.AddListener(ShowPauseUI);
            GameManager.OnGameResumed.AddListener(HidePauseUI);
            GameManager.OnWorkbenchOpened.AddListener(ShowWorkbenchUI);
            GameManager.OnWorkbenchClosed.AddListener(HideWorkbenchUI);
        }
    }

    private void OnDisable()
    {
        GameManager.OnGamePaused.RemoveAllListeners();
        GameManager.OnGameResumed.RemoveAllListeners();
        GameManager.OnWorkbenchOpened.RemoveAllListeners();
        GameManager.OnWorkbenchClosed.RemoveAllListeners();
        // Only unsubscribe if this is the singleton instance
        if (Instance == this)
        {
            GameManager.OnGamePaused.RemoveListener(ShowPauseUI);
            GameManager.OnGameResumed.RemoveListener(HidePauseUI);
            GameManager.OnWorkbenchOpened.RemoveListener(ShowWorkbenchUI);
            GameManager.OnWorkbenchClosed.RemoveListener(HideWorkbenchUI);
        }
    }

    /// <summary>
    /// Loads the scene.
    /// </summary>
    /// <param name="sceneName">Name of the scene.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        if(sceneName == "Game" && GameManager.Instance != null)
        {
            GameManager.Instance.ChangeGameState(GameStates.PLAYING);
        }
    }

    /// <summary>
    /// Loads a new game.
    /// </summary>
    /// <param name="sceneName">Name of the scene.</param>
    public void LoadSceneNewGame(string sceneName)
    {
        SaveSystem.DeleteGameData();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        if (sceneName == "Game" && GameManager.Instance != null)
        {
            GameManager.Instance.ChangeGameState(GameStates.PLAYING);
        }
    }

    /// <summary>
    /// Shows the pause UI.
    /// </summary>
    public void ShowPauseUI()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(true);
            dayUI.SetActive(false);
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
            dayUI.SetActive(true);
        }
    }

    public void ShowSettingsUI()
    {
        if (settingsUI != null)
            settingsUI.SetActive(true);

        if (pauseUI != null)
            pauseUI.SetActive(false);
    }

    public void HideSettingsUI()
    {
        if (settingsUI != null)
            settingsUI.SetActive(false);

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    /// <summary>
    /// Shows the workbench UI.
    /// </summary>
    public void ShowWorkbenchUI()
    {
        if (workbenchUI != null)
        {
            workbenchUI.SetActive(true);
            dayUI.SetActive(false);
        }
    }

    /// <summary>
    /// Hides the workbench UI.
    /// </summary>
    public void HideWorkbenchUI()
    {
        if (workbenchUI != null)
        {
            workbenchUI.SetActive(false);
            dayUI.SetActive(true);
        }
    }
}