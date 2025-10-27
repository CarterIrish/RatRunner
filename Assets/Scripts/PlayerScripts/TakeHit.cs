
using UnityEngine;

public class TakeHit : MonoBehaviour
{
    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="other">The other object.</param>
    private void OnTriggerEnter(Collider other)
    {

        // end the game when enemy collides with _currentPlayer
        if (other.CompareTag("Enemy"))
        {
            DayManager.Instance.OnPlayerCaught();
        }
    }
}
