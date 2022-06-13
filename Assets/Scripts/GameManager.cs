using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

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
}

public enum GameState
{
    RaceBegin,
    Racing,
    Victory,
    Lose,
    Finish
}
