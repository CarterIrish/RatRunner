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
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    #region UI Methods below
    #endregion

}