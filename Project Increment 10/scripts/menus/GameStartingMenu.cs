using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameStartingMenu : MonoBehaviour
{
    protected GameStartedEvent gameStarted = new GameStartedEvent();

    void Start()
    {
        EventManager.AddGameStartedInvoker(this);

        EventManager.AddGameStartedListener(StartGame);
    }

    public void AddGameStartedListener(UnityAction<DifficultyType, GameType> listener)
    {
        gameStarted.AddListener(listener);
    }

    public void StartGame(DifficultyType diff, GameType gameType)
    {
        GameUtils.HandleGameStartedEvent(gameType, diff); 
        SceneManager.LoadScene("gameplay");
    }
}
