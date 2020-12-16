using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondToButtonClicks : MonoBehaviour
{
    // Start is called before the first frame update
   public void HandleBackButtonOnClickEvent()
    {
        MenuLoader.GoToMenu(MenuName.Main);
    }
    public void HandlePlayButtonOnClickEvent()
    {
        MenuLoader.GoToMenu(MenuName.Play);
    }
    public void HandleHelpButtonOnClickEvent()
    {
        MenuLoader.GoToMenu(MenuName.Help);
    }
    public void HandleRestartButtonOnClickEvent()
    {

    }
    public void HandleResumeButtonOnClickEvent()
    {

    }
    public void HandleQuitButtonOnClickEvent()
    {

    }
}
