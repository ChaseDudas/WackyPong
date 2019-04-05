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
        MenuManager.GoToMenu(MenuName.Main);
    }
}
