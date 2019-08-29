using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerlives = 3, Score =0;
    [SerializeField] float LoadDelay = 3;
    [SerializeField] Text LiveText, ScoreText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LiveText.text = playerlives.ToString();
        ScoreText.text = Score.ToString(); 
    }

    public void addPoint(int pointsToAdd)
    {
        Score += pointsToAdd;
        ScoreText.text = Score.ToString();
    }

    public void playerDeath()
    {
        if (playerlives > 1)
        {
            TakeLife();
        }
        else {
            ResetGame();
        }
    }

    private void TakeLife()
    {
        playerlives--;
        StartCoroutine(ReloadLevel());
        LiveText.text = playerlives.ToString();

    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSecondsRealtime(LoadDelay);
        var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}//GameSession
