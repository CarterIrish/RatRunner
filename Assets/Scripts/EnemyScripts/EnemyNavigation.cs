using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using Unity.VisualScripting;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;
    private float distance;
    private NavMeshAgent agent;
    public List<Transform> targetList;
    public bool trackingPlayer = false;
    public float triggerDistance;
    public bool specialEnemy = false;
    public bool finalEnemy = false;
    private GameObject player;

    private bool enemyAudioIsPlaying = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        Inventory.OnItemAdded.AddListener(OnItemPickedUp);
    }

    private void OnDisable()
    {
        Inventory.OnItemAdded.RemoveListener(OnItemPickedUp);
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        // FINAL ENEMY auto-activation
        if (finalEnemy)
        {
            float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distToPlayer <= 50f)
                StartHunting();
            else
                StopHunting();
        }

        // Patrol logic
        if (targetList != null && !trackingPlayer)
        {
            if (distance < 4f)
            {
                int index = targetList.IndexOf(target);
                target = (index == targetList.Count - 1) ? targetList[0] : targetList[index + 1];
            }
        }

        // Day speed adjustments
        if (!specialEnemy)
        {
            if (DayManager.Instance.CurrentDay == 1)
            {
                agent.speed = 15f;
                agent.acceleration = 12f;
                agent.angularSpeed = 200f;
            }
            else
            {
                agent.speed = 12.5f;
                agent.acceleration = 10f;
                agent.angularSpeed = 160f;
            }
        }

        // Stop hunting if too far
        if (trackingPlayer && distance > 80f)
            StopHunting();

        // Stop movement unless game is actively playing
        if (GameManager.Instance.GameState == GameStates.PLAYING)
            agent.destination = target.position;
        else
            agent.destination = transform.position;
    }

    private void OnItemPickedUp(ItemsEnum item)
    {
        // Logic for key pickups, currently unused
    }

    public void StartHunting()
    {
        if (player == null) return;

        target = player.transform;
        trackingPlayer = true;

        // Start audio
        if (!AudioManager.Instance.EnemyNearby.isPlaying)
            AudioManager.Instance.EnemyNearby.Play();

        // Start enemy-near screen pulse
        ScreenEffectManager.Instance.EnableEnemyNearbyEffect(true);

        Debug.Log("Start hunting player.");
    }

    public void StopHunting()
    {
        trackingPlayer = false;

        // Stop audio
        AudioManager.Instance.EnemyNearby.Stop();

        // Stop pulse
        ScreenEffectManager.Instance.EnableEnemyNearbyEffect(false);

        // Return to first patrol point
        if (targetList.Count > 0)
            target = targetList[0];
        else
            target = transform;
    }
}
