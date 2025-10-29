using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sounds")]
    [SerializeField]private AudioSource atmosphere;
    [SerializeField]private AudioSource itemPickUp;
    [SerializeField]private AudioSource buttonHover;
    [SerializeField]private AudioSource buttonPress;
    [SerializeField]private AudioSource enemyNearby;

    private void Awake()
    {
        // Singleton pattern — ensures only one AudioManager exists
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
        RegisterAllButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RegisterAllButtons()
    {
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
}
