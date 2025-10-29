using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    private GameObject player;

    public TextMeshProUGUI textObject;

    // Start is called before the first frame update
    void Start()
    {
        // gather player objects from scene
        player = GameObject.FindGameObjectWithTag("Player");

        // set base text on initialization
        textObject.text = "Find the Key.";
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
                if (!textObject.isActiveAndEnabled)
                {
                    textObject.gameObject.SetActive(true);
                }
                else
                {
                    textObject.gameObject.SetActive(false);
                }
            }
        }
    }
}
