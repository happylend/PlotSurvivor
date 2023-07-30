using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //������Ϸ״̬
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    } 

    //��ǰ��Ϸ״̬
    public GameState currentState;
    //��һ����Ϸ״̬
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

    //�ı���Ϸ״̬
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    //��ͣ��Ϸ
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
    //�ָ���Ϸ
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pasueScreen.SetActive(false);
        }
    }

    //����Ƿ���ͣ��Ϸ
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
