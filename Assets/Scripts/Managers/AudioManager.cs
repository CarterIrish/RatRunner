using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sounds")]
    [SerializeField] private AudioSource atmosphere;
    [SerializeField] private AudioSource itemPickUp;
    [SerializeField] private AudioSource buttonHover;
    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private AudioSource enemyNearby;
    [SerializeField] private AudioSource workbench;
    [SerializeField] private AudioSource dayTransition;

    // Track atmosphere state for WebGL compatibility
    private bool atmosphereIsPlaying = false;

    //Getters/Setters
    public AudioSource ItemPickUp { get { return itemPickUp; } }
    public AudioSource DayTransition { get { return dayTransition; } }
    public AudioSource EnemyNearby { get { return enemyNearby; } }
    public AudioSource Workbench { get { return workbench; } }

    private void Awake()
    {
        // Singleton pattern � ensures only one AudioManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        atmosphere.Play();
        atmosphereIsPlaying = true;
        RegisterAllButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.OnWorkbenchOpened.AddListener(PlayWorkbenchSound);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameManager.OnWorkbenchOpened.RemoveListener(PlayWorkbenchSound);
    }

    /// <summary>
    /// Plays the workbench opening sound.
    /// </summary>
    private void PlayWorkbenchSound()
    {
        if (workbench != null)
            workbench.Play();
    }

    private void RegisterAllButtons()
    {
        // TODO: Fix this deprecated code warning by using new option
        Button[] buttons = FindObjectsOfType<Button>(true);

        foreach (Button button in buttons)
        {
            // Add click sound
            button.onClick.AddListener(() => PlayButtonClick());

            // Add hover sound
            AddHoverEvent(button.gameObject);
        }
    }

    private void AddHoverEvent(GameObject buttonObj)
    {
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = buttonObj.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((_) => PlayButtonHover());

        trigger.triggers.Add(entry);
    }

    public void PlayButtonHover()
    {
        if (buttonHover != null)
            buttonHover.Play();
    }

    public void PlayButtonClick()
    {
        if (buttonPress != null)
            buttonPress.Play();
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void StopAudio(AudioSource audio)
    {
        audio.Stop();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop atmosphere audio if we're not in the Game scene
        if (scene.name != "Game") // Change "Game" to your actual gameplay scene name
        {
            if (atmosphereIsPlaying)
            {
                atmosphere.Stop();
                atmosphereIsPlaying = false;
            }
        }
        else
        {
            // If we re-enter the Game scene, restart the atmosphere
            if (!atmosphereIsPlaying)
            {
                atmosphere.Play();
                atmosphereIsPlaying = true;
            }
        }

        // Optionally re-register UI buttons in the new scene
        RegisterAllButtons();
    }

}
