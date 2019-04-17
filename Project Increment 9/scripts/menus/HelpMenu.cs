using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The help menu
/// </summary>
public class HelpMenu : MonoBehaviour
{
    /// <summary>
    /// Goes back to the main menu
    /// </summary>
    public void GoBack()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
