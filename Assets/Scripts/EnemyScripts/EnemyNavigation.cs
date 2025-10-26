using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;
    private float distance;
    private NavMeshAgent agent;
    public List<Transform> targetList;
    public bool trackingPlayer = false;
    public AudioSource enemyNearAudio;
    public float triggerDistance;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // StartHunting();
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

        // if enemy reaches a point in a room and isn't tracking _currentPlayer, go to the next point in the list
        if (targetList != null && !trackingPlayer)
        {
            if (distance < 4.0f)
            {
                int index = targetList.IndexOf(target);

                if (index == targetList.Count - 1)
                {
                    target = targetList[0];
                }
                else
                {
                    target = targetList[index + 1];
                }
            }
        }

        // only tracks _currentPlayer in playing game state
        if (GameManager.Instance != null && GameManager.Instance.GameState == GameStates.PLAYING)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        //EnemyNearby();
    }

    /// <summary>
    /// Called when [item picked up].
    /// </summary>
    /// <param name="item">The item.</param>
    private void OnItemPickedUp(ItemsEnum item)
    {
        if(item == ItemsEnum.key)
        {
            // StartHunting();
        }
    }

    /// <summary>
    /// Starts hunting the _currentPlayer.
    /// </summary>
    public void StartHunting()
    {
        // Gathers _currentPlayer object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // Checks if null
        if (player != null)
        {
            // Assigns target
            target = player.transform;
            trackingPlayer = true;
            Debug.Log("Start hunting _currentPlayer");
        }
    }

    /// <summary>
    /// Stops hunting the _currentPlayer
    /// </summary>
    public void StopHunting()
    {
        trackingPlayer = false;
        if (targetList != null)
        {
            target = targetList[0];
        }
        else
        {
            target = transform;
        }
    }

    public void EnemyNearby()
    {
        if (target == null || enemyNearAudio == null) return;

        //checks distance between _currentPlayer and enemy
        bool isClose = distance <= triggerDistance;

        //if near and audio is not playing, play audio
        if (isClose && !enemyNearAudio.isPlaying)
        {
            enemyNearAudio.loop = true;
            enemyNearAudio.Play();
        }
        //if not near anymore and enemy audio is playing, stop audio
        else if (!isClose && enemyNearAudio.isPlaying)
        {
            enemyNearAudio.Stop();
        }
    }

}
