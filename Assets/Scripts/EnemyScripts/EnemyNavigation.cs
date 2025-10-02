using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;
    private float distance;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // temporary for playtest
        target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // future reference for attacking
        distance = Vector3.Distance(transform.position, target.position);

        //if (distance < attackDistance)
        //{
        //    agent.isStopped = true;
        //}
        //else
        //{
        //    agent.isStopped = false;
        //    agent.destination = target.position;
        //}

        // only tracks player in playing game state
        if (GameManager.Instance.GameState == GameStates.PLAYING)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
