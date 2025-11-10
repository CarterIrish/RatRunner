using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private GameObject player;

    public TextMeshProUGUI textObject;

    private float timer = 100.0f;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // gather player object from scene
        player = GameObject.FindGameObjectWithTag("Player");

        // gather text object from scene
        textObject = GameObject.FindGameObjectWithTag("Feedback").GetComponent<TextMeshProUGUI>();

        textObject.gameObject.SetActive(false);

        // set base text on initialization
        textObject.text = "Find the Key.";
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5.0f)
        {
            textObject.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// displays feedback text to the player when they are near an unlockable door
    /// </summary>
    /// <param name="other">collider that triggered method</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.isTrigger) // make sure only one collider on player is triggering event
            {
                if (count < 1) // only display to player once
                {
                    if (!textObject.isActiveAndEnabled)
                    {
                        timer = 0;
                        textObject.gameObject.SetActive(true);
                        count++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// let the player know that the gate has been opened
    /// </summary>
    public void OnKeyPickup()
    {
        textObject.text = "The gate has been opened.";
        timer = 0;
        textObject.gameObject.SetActive(true);
        count++;
    }
}
