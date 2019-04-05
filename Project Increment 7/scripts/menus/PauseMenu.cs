using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PauseMenu : MonoBehaviour
{

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        //pause time
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes the paused game
    /// </summary>
    public void ResumeGame()
    {
        //Unpause game and destroy menu
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    /// <summary>
    /// Quits the paused game
    /// </summary>
    public void QuitGame()
    {
        //Unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
