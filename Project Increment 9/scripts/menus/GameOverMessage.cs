using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The game over message
/// </summary>
public class GameOverMessage : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        AudioManager.Play(AudioClipName.GameLost);

        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    /// <summary>
    /// Sets the winning side in the message to the given side
    /// </summary>
    /// <param name="winner">winner</param>
    public void SetWinner(ScreenSide winner)
    {
        if (winner == ScreenSide.Left)
        {
            messageText.text = "Game Over!\n\nLeft player won!";
        }
        else
        {
            messageText.text = "Game Over!\n\nRight player won!";
        }
    }

    /// <summary>
    /// Moves to main menu when quit button clicked
    /// </summary>
    public void HandleQuitButtonClicked()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
