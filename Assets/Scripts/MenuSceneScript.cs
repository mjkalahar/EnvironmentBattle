using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneScript : MonoBehaviour
{

    public GameObject textObject;
    int index;

    string [] MenuText = { " Play ", " Controls ", " Instructions ", " Sources ", " Exit " };
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = Math.Max(index - 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = Math.Min(index + 1, MenuText.Length - 1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            index = MenuText.Length - 1;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            doMenu(index);
        }
        else { 
            string output = "";
            for (int i = 0; i < MenuText.Length; i++)
            {
                output += (i == index) ? "> " + MenuText[i] + " <" : MenuText[i];

                output += "\r\n\r\n";
            //Debug.Log(output);
            }
            
            setText(output);
        }
    }

    void setText(string input)
    {
        //Debug.Log(input);
        textObject.GetComponent<Text>().text = input;
    }

    void doMenu(int input)
    {
        switch (input)
        {
            case 0:
                MenuLoader.GoToMenu(MenuName.Game);
                break;
            case 1:
                MenuLoader.GoToMenu(MenuName.Controls);
                break;
            case 2:
                MenuLoader.GoToMenu(MenuName.Instructions);
                break;
            case 3:
                MenuLoader.GoToMenu(MenuName.Sources);
                break;
            case 4:
                Application.Quit();
                break;
        }   
    }
}
