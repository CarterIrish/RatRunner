using System;
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

    /// <summary>
    /// Called when [enable].
    /// </summary>
    private void OnEnable()
    {
        // Adds listener to pickup event
        Inventory.OnItemAdded.AddListener(OnItemPickedUp);
    }

    /// <summary>
    /// Called when [disable].
    /// </summary>
    private void OnDisable()
    {
        // Removes listener
        Inventory.OnItemAdded.RemoveListener(OnItemPickedUp);
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

        // only tracks pivot in playing game state
        if (GameManager.Instance.GameState == GameStates.PLAYING)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    /// <summary>
    /// Called when [item picked up].
    /// </summary>
    /// <param name="item">The item.</param>
    private void OnItemPickedUp(ItemsEnum item)
    {
        if(item == ItemsEnum.key)
        {
            StartHunting();
        }
    }

    /// <summary>
    /// Starts hunting the pivot.
    /// </summary>
    private void StartHunting()
    {
        // Gathers pivot object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // Checks if null
        if (player != null)
        {
            // Assigns target
            target = player.transform;
            Debug.Log("Start hunting pivot");
        }
    }
}
