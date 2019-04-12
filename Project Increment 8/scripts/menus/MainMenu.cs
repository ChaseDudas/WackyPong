﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Goes to the difficulty menu
    /// </summary>
    public void GoToDifficultyMenu()
    {

    }

    /// <summary>
    /// Starts a two player game
    /// </summary>
    public void StartTwoPlayerGame()
    {
        AudioManager.Play(AudioClipName.ButtonPress);
        SceneManager.LoadScene("gameplay");
    }

    /// <summary>
    /// Shows the help menu
    /// </summary>
    public void ShowHelp()
    {
        AudioManager.Play(AudioClipName.ButtonPress);
        MenuManager.GoToMenu(MenuName.Help);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        AudioManager.Play(AudioClipName.ButtonPress);
        Application.Quit();
    }
}
