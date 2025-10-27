using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    [SerializeField]
    float pushForce = 35.0f;

    /// <summary>
    /// Push player backwards if they collide with an object
    /// </summary>
    /// <param name="collision">object that collided with script</param>
    private void OnCollisionEnter(Collision collision)
    {
        // check if player
        if (collision.gameObject.CompareTag("Player"))
        {
            // get player script
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();

            Rigidbody playerRB = playerScript.playerBody;

            // add a force to the player to push them backwards
            if (playerRB != null)
            {
                Vector3 force = transform.forward * pushForce;
                playerRB.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
