using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public EnemyNavigation enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyScript == null)
        {
            Debug.Log("AttackTrigger script missing enemy reference.");
        }
    }

    /// <summary>
    /// gets enemy to target player when they enter a room
    /// </summary>
    /// <param name="other">collider that triggered this</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.isTrigger)
            {
                if (enemyScript.target == other.transform && DayManager.Instance.CurrentDay == 1)
                {
                    enemyScript.StopHunting();
                }
                else
                {
                    enemyScript.StartHunting();
                }
            }
        }
    }
}
