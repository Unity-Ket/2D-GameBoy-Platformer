using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerlives = 3;
    [SerializeField] float LoadDelay = 3;

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
