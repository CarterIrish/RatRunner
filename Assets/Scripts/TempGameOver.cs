using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameOver : MonoBehaviour
{
    public InputManager inMGN;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Over hit");
        inMGN.SetPlayerInputState(false);
        GameManager.Instance.ChangeGameState(GameStates.GAME_OVER);

    }
}
