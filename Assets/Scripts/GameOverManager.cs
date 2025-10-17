using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        if(GameManager.Instance.PlayerEscaped == true)
        {
            Debug.Log("Player Escaped");
        }
        else
        {
            Debug.Log("Player failed");
        }
    }
}
