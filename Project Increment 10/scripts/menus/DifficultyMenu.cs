using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// The help menu
/// </summary>
public class DifficultyMenu : GameStartingMenu
{
    /// <summary>
    /// Goes back to the main menu
    /// </summary>
    public void GoBack()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Main);
    }

    public void EasyDifficulty()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        gameStarted.Invoke(DifficultyType.Easy, GameType.OnePlayer);
        //SceneManager.LoadScene("gameplay");
    }

    public void MediumDifficulty()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        gameStarted.Invoke(DifficultyType.Medium, GameType.OnePlayer);
        //SceneManager.LoadScene("gameplay");
    }

    public void HardDifficulty()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        gameStarted.Invoke(DifficultyType.Hard, GameType.OnePlayer);
        //SceneManager.LoadScene("gameplay");
    }


}