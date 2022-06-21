using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    List<GameObject> opponents;
    GameObject player;
    GameObject finishArea;
    [SerializeField] private Text rankingText;
    [SerializeField] private Text racersText;
    [SerializeField] private Text paintPercent;
    MousePainter mp;
    bool gameHasEnded = false;
    float restartDelay = 2f;
    int ranking = 0;
    int totalRacers = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        UpdateGameState(GameState.RaceBegin);
        opponents = GameObject.FindGameObjectsWithTag("Opponent").ToList();
        player = GameObject.FindGameObjectWithTag("Player");
        finishArea = GameObject.FindGameObjectWithTag("FinishLine");
        mp = FindObjectOfType<MousePainter>();
        totalRacers = opponents.Count() + 1;
        racersText.text = totalRacers.ToString();
    }

    void Update()
    {
        if(GameManager.Instance.State == GameState.Racing)
        {
            UpdateGameState(GameState.Racing);
            ranking = GetCurrentRanking();
            
            if(ranking != Convert.ToInt32(rankingText.text))
            {
                rankingText.text = ranking.ToString();
            }
        }

        if(GameManager.Instance.State == GameState.Victory)
        {
            UpdateGameState(GameState.Victory);
            paintPercent.text = mp.GetPaintPercent().ToString();
            if(mp.GetPaintPercent() >= 100)
            {
                FinishGame();
            }
            foreach(var opponent in opponents)
            {
                Destroy(opponent);
            }
        }

        if(GameManager.Instance.State == GameState.Lose)
        {
            UpdateGameState(GameState.Lose);
        }

        if(GameManager.Instance.State == GameState.Finish)
        {
            UpdateGameState(GameState.Finish);
        }
    }

    int GetCurrentRanking()
    {
        int currRank = 1;
        float deltaPlayerZ = Mathf.Abs(finishArea.transform.position.z - player.transform.position.z);
        foreach(var opponent in opponents)
        {
            float deltaOpponentZ = Mathf.Abs(finishArea.transform.position.z - opponent.transform.position.z);
            if(deltaPlayerZ > deltaOpponentZ)
            {
                currRank++;
            }
        }
        return currRank;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.RaceBegin:
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

    
    public void WinRace()
    {
        GameManager.Instance.State = GameState.Victory;
    }

    public void LoseRace()
    {
        GameManager.Instance.State = GameState.Lose;
    }

    public void FinishGame()
    {
        GameManager.Instance.State = GameState.Finish;
    }

    public void EndGame()
    {
        if(!gameHasEnded)
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
    RaceBegin,//start with countdown
    Racing,//racing in platform challenge
    Victory,//state where player won the race and will paint the wall
    Lose,//state where opponent won the race
    Finish//state where player painted the wall
}
