using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject raceBegin;
    [SerializeField] private GameObject[] racing;
    [SerializeField] private GameObject victory;
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

        //can improve this
        for(int i = 0; i < racing.Length; i++)
        {
            racing[i].SetActive(state==GameState.Racing);
        }

        victory.SetActive(state == GameState.Victory);
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.State = GameState.Racing;

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
