using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    bool gameHasEnded = false;
    float restartDelay = 2f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.RaceBegin);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.RaceBegin:
                HandleRaceBegin();
                break;
            case GameState.Racing:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            case GameState.Finish:
                break;
            default:
                Debug.Log("Unhandled");
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleRaceBegin()
    {
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum GameState
{
    RaceBegin,
    Racing,
    Victory,
    Lose,
    Finish
}
