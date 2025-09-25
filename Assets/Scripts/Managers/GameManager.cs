using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;




/// <summary>
/// This enum serves to hold all core game loop states
/// </summary>
public enum GAME_STATES
{
    START,
    PLAYING,
    PAUSED,
    GAME_OVER
}

/// <summary>
/// This class serves to manage core game loop
/// </summary>
public class GameManager : MonoBehaviour
{

    private GAME_STATES _gameState = GAME_STATES.START;  // Initilze _gameState field for local maniupualtion    
    /// <summary>
    /// Gets the state of the game.
    /// </summary>
    /// <value>
    /// The state of the game.
    /// </value>
    public GAME_STATES GameState
    {
        get { return _gameState; }
    }

    /// <summary>
    /// Changes the state of the game.
    /// </summary>
    /// <param name="newState">The new state.</param>
    public void ChangeGameState(GAME_STATES newState)
    {
        _gameState = newState;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(_gameState != GAME_STATES.START)
        {
            _gameState = GAME_STATES.START;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gamestate machine
        switch (_gameState)
        {
            case GAME_STATES.START:
                HandleStartState();
                break;
            case GAME_STATES.PLAYING:
                HandlePlayingState();
                break;
            case GAME_STATES.PAUSED:
                HandlePausedState();
                break;
            case GAME_STATES.GAME_OVER:
                HandleGameOverState();
                break;
        }

    }

    #region Gameplay Loop Methods Below
    private void HandleStartState() { }
    private void HandlePlayingState() { }
    private void HandlePausedState() { }
    private void HandleGameOverState() { }
    #endregion
}


