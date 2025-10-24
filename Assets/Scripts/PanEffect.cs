using UnityEngine;
using UnityEngine.UI;

public class PanEffect : MonoBehaviour
{
    [Header("Pan Settings")]
    public Vector3 panAmount = new Vector3(0.5f, 0.5f, 0f); // max offset
    public float panSpeed = 1f; // how fast it moves

    [Header("Flicker Settings (optional)")]
    public bool enableFlicker = false;
    public float flickerSpeed = 5f;
    public float minAlpha = 0.5f;

    private Vector3 startPos;

    // Components for flicker
    private Image uiImage;
    private SpriteRenderer spriteRenderer;
    private bool isUI = false;

    private void Awake()
    {
        startPos = transform.position;

        // Detect if this is a UI element
        uiImage = GetComponent<Image>();
        if (uiImage != null)
        {
            isUI = true;
            startPos = ((RectTransform)transform).anchoredPosition;
        }

        // Detect if it's a SpriteRenderer for 2D objects
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float t = Time.unscaledTime; // Use unscaled time so it keeps running during pause

        // --- Pan effect ---
        float xOffset = Mathf.PerlinNoise(t * panSpeed, 0f) * panAmount.x - panAmount.x / 2f;
        float yOffset = Mathf.PerlinNoise(0f, t * panSpeed) * panAmount.y - panAmount.y / 2f;
        float zOffset = Mathf.PerlinNoise(t * panSpeed, t * panSpeed) * panAmount.z - panAmount.z / 2f;

        Vector3 newPos = startPos + new Vector3(xOffset, yOffset, zOffset);

        if (isUI)
            ((RectTransform)transform).anchoredPosition = newPos;
        else
            transform.position = newPos;

        // --- Flicker effect ---
        if (enableFlicker)
        {
            float alpha = Mathf.Lerp(minAlpha, 1f, Mathf.PerlinNoise(t * flickerSpeed, 1f));
            if (isUI && uiImage != null)
                uiImage.color = new Color(uiImage.color.r, uiImage.color.g, uiImage.color.b, alpha);
            else if (spriteRenderer != null)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
    }

}

