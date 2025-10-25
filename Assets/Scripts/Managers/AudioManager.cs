using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource atmosphere;
    public AudioSource itemPickUp;
    public AudioSource buttonHover;
    public AudioSource buttonPress;
    public AudioSource enemyNearby;
    
    // Start is called before the first frame update
    void Start()
    {
        atmosphere.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
