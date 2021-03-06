using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject raceBegin;
    [SerializeField] private GameObject racing;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject finish;
    [SerializeField] private Text countdownDisplay;

    public int countdownTime = 5;



    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        raceBegin.SetActive(state == GameState.RaceBegin);
        if(state == GameState.RaceBegin)
        {
            StartCoroutine(CountdownToStart());
        }

        racing.SetActive(state == GameState.Racing);

        victory.SetActive(state == GameState.Victory);

        finish.SetActive(state == GameState.Finish || state == GameState.Lose);
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        FindObjectOfType<GameManager>().StartRace();

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
