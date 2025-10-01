using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // end the game when enemy collides with player
        if (other.CompareTag("Enemy"))
        {
            UIManager.Instance.LoadScene("GameOver");

            // stop enemy from moving
            EnemyNavigation enemyScript = other.gameObject.GetComponent<EnemyNavigation>();
            enemyScript.target = other.transform;

            Debug.Log("Enemy hit");
        }
    }
}
