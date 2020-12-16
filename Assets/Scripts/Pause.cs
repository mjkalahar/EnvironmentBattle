using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void HandleResumeButtonOnClickEvent()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    public void HandleRestartButtonOnClickEvent()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuLoader.GoToMenu(MenuName.Main);
    }
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }
}
