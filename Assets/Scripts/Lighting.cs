using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Lighting : MonoBehaviour
{
    public Light lightSource;

    public float minTime = 0.1f;
    public float maxTime = 1.2f;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LightFlicker();
    }

    void LightFlicker()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            lightSource.enabled = !lightSource.enabled;
            timer = Random .Range(minTime, maxTime);
        }
    }
}
