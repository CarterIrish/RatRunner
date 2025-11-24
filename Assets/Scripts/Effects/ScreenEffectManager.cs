using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager Instance;

    [Header("UI Layers")]
    public Image enemyNearbyOverlay;    
    public Image healthOverlay;

    [Header("Enemy Nearby Settings")]
    public float enemyMaxAlpha;
    public float enemyPulseSpeed;

    [Header("Low Health Settings")]
    public float lowHealthMaxAlpha;
    public float lowHealthPulseSpeed;

    [Header("Shared Fade Settings")]
    public float fadeSpeed = 2f;

    private float noiseAlpha = 0f;
    private float vignetteAlpha = 0f;

    private bool noiseActive = false;
    private bool lowHealthActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleNoisePulse();
        HandleLowHealthPulse();
    }

    // Enemy Nearby (Noise Pulse)
    private void HandleNoisePulse()
    {
        if (enemyNearbyOverlay == null) return;

        if (noiseActive)
        {
            noiseAlpha = enemyMaxAlpha * (0.5f + 0.5f * Mathf.Sin(Time.time * enemyPulseSpeed));
        }
        else
        {
            noiseAlpha = Mathf.MoveTowards(noiseAlpha, 0f, Time.deltaTime * fadeSpeed);
        }

        enemyNearbyOverlay.color = new Color(1, 1, 1, noiseAlpha);
    }

    public void EnableEnemyNearbyEffect(bool enable)
    {
        noiseActive = enable;
    }

    // Low Health (Red Vignette)
    private void HandleLowHealthPulse()
    {
        if (healthOverlay == null) return;

        if (lowHealthActive)
        {
            vignetteAlpha = lowHealthMaxAlpha * (0.5f + 0.5f * Mathf.Sin(Time.time * lowHealthPulseSpeed));
        }
        else
        {
            vignetteAlpha = Mathf.MoveTowards(vignetteAlpha, 0f, Time.deltaTime * fadeSpeed);
        }

        healthOverlay.color = new Color(1, 1, 1, vignetteAlpha);
    }

    public void SetLowHealth(bool enable)
    {
        lowHealthActive = enable;
    }
}
