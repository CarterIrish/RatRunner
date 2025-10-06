using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : MonoBehaviour
{
    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="other">The other object.</param>
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log($"TakeHit >> Generic: {other.name}");
        // end the game when enemy collides with player
        if (other.CompareTag("Enemy"))
        {
            DayManager.Instance.NextDay();
        }
    }
}
