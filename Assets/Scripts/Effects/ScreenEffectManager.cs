using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager Instance;

    [Header("Effect Layers")]
    public Image vignetteOverlay;
    public Image noiseOverlay;

    [Header("Effect Settings")]
    public float vignetteMaxAlpha = 1.0f;
    public float vignetteFadeSpeed = 2f;
    public float noiseMaxAlpha = 0.25f;
    public float noisePulseSpeed = 5f;

    private bool vignetteActive = false;
    private bool noiseActive = false;
    private float vignetteAlpha = 0f;
    private float noiseAlpha = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Smoothly fade vignette
        float targetVignetteAlpha = vignetteActive ? vignetteMaxAlpha : 0f;
        vignetteAlpha = Mathf.MoveTowards(vignetteAlpha, targetVignetteAlpha, Time.deltaTime * vignetteFadeSpeed);
        if (vignetteOverlay != null)
        {
            vignetteOverlay.color = new Color(0, 0, 0, vignetteAlpha);
        }

        // Pulse noise when active
        if (noiseOverlay != null)
        {
            if (noiseActive)
            {
                noiseAlpha = noiseMaxAlpha * (0.5f + 0.5f * Mathf.Sin(Time.time * noisePulseSpeed));
            }
            else
            {
                noiseAlpha = Mathf.MoveTowards(noiseAlpha, 0f, Time.deltaTime * vignetteFadeSpeed);
            }
            noiseOverlay.color = new Color(1, 1, 1, noiseAlpha);
        }
    }

    public void EnableEffects(bool enable)
    {
        vignetteActive = enable;
        noiseActive = enable;
    }
}
