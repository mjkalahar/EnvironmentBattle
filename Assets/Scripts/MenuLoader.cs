using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuLoader
{
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Help:
                SceneManager.LoadScene("HelpMenu");
                break;
            case MenuName.Play:
                SceneManager.LoadScene("Terrain");
                break;
        }
    }
}
