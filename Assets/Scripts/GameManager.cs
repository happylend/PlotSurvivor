using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //定义游戏状态
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    } 

    //当前游戏状态
    public GameState currentState;
    //上一个游戏状态
    public GameState previousState;

    [Header("UI")]
    public GameObject pasueScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DisableScreen();
    }

    void Update()
    {


        switch(currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                break;

            default:
                break;
        }
    }

    //改变游戏状态
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    //暂停游戏
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pasueScreen.SetActive(true);
        }

    }
    //恢复游戏
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pasueScreen.SetActive(false);
        }
    }

    //检测是否暂停游戏
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    void DisableScreen()
    {
        pasueScreen.SetActive(false);
    }

}
