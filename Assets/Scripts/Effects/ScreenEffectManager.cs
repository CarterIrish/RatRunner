using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectManager : MonoBehaviour
{
    public static ScreenEffectManager Instance;

    [Header("Effect Layers")]
    public Image noiseOverlay;

    [Header("Effect Settings")]
    public float vignetteFadeSpeed = 2f;
    public float noiseMaxAlpha = 0.25f;
    public float noisePulseSpeed = 5f;

    private bool noiseActive = false;
    private float noiseAlpha = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
        noiseActive = enable;
    }
}
