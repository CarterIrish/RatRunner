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

    // Track audio state for WebGL compatibility
    private bool enemyAudioIsPlaying = false;



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

        // increase enemy speed and acceleration on final day
        if (DayManager.Instance.CurrentDay == 3)
        {
            agent.speed = 10.0f;
            agent.acceleration = 9.0f;
        }
        else
        {
            agent.speed = 7.5f;
            agent.acceleration = 8.0f;
        }

        // stop hunting player when they are too far away
        if (trackingPlayer && distance > 80.0f)
        {
            StopHunting();
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
            AudioManager.Instance.EnemyNearby.Play();
        }
    }

    /// <summary>
    /// Stops hunting the _currentPlayer
    /// </summary>
    public void StopHunting()
    {
        AudioManager.Instance.EnemyNearby.Stop();
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
        if (isClose && !enemyAudioIsPlaying)
        {
            enemyNearAudio.loop = true;
            enemyNearAudio.Play();
            enemyAudioIsPlaying = true;
        }
        //if not near anymore and enemy audio is playing, stop audio
        else if (!isClose && enemyAudioIsPlaying)
        {
            enemyNearAudio.Stop();
            enemyAudioIsPlaying = false;
        }
    }

}
