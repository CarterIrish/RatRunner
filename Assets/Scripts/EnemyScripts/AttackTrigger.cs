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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyScript.target == other.transform)
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
