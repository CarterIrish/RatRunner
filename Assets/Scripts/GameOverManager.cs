using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject escapedScreen;
    public GameObject loseScreen;

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

    public void WinOrLose()
    {
        if (GameManager.Instance.PlayerEscaped)
        {
            escapedScreen.SetActive(true);
            loseScreen.SetActive(false);
        }
        else
        {
            escapedScreen.SetActive(false);
            loseScreen.SetActive(true);
        }
    }
}
