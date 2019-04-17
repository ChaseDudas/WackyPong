using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gameplay manager
/// </summary>
public class GameplayManager : MonoBehaviour
{
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        EventManager.AddPlayerWonListener(EndGame);
    }
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // pause game on escape key
        if (Input.GetKeyDown(KeyCode.Escape) &&
            Time.timeScale == 1)
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    /// <summary>
    /// Ends the game and displays the winner message
    /// </summary>
    /// <param name="winner">winning side</param>
    public void EndGame(ScreenSide winner)
    {
        // instantiate prefab and set score
        GameObject gameOverMessage = Instantiate(Resources.Load("GameOverMessage")) as GameObject;
        GameOverMessage gameOverMessageScript = gameOverMessage.GetComponent<GameOverMessage>();
        gameOverMessageScript.SetWinner(winner);
    }
}
