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
                SceneManager.LoadScene("MenuScene");
                break;
            case MenuName.Controls:
                SceneManager.LoadScene("ControlsScene");
                break;
            case MenuName.Instructions:
                SceneManager.LoadScene("InstructionsScene");
                break;
            case MenuName.Game:
                SceneManager.LoadScene("Game");
                break;
            case MenuName.Sources:
                SceneManager.LoadScene("SourcesScene");
                break;
        }
    }
}
