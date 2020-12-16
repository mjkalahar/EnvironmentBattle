using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public Text robotsLeftText;
    public Text treesLeftText;
    public Text healthText;
    public Image background;

    public int robotsLeft = 30;
    public int treesLeft = 100;
    public int health = 100;

    int score;

    const string scorePrefix = "Score: ";
    const string robotsLeftPrefix = "Robots Left: ";
    const string treesLeftPrefix = "Trees Left: ";
    const string healthPrefix = "Health: ";

    public int RobotsLeft
    {
        get { return robotsLeft; }
    }

    void Start()
    {
        RectTransform scoreTextRectTransform = scoreText.GetComponent<RectTransform>();
        scoreTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, scoreTextRectTransform.rect.width);
        scoreTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10, scoreTextRectTransform.rect.height);

        RectTransform robotsLeftTextRectTransform = robotsLeftText.GetComponent<RectTransform>();
        robotsLeftTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, robotsLeftTextRectTransform.rect.width);
        robotsLeftTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 30, robotsLeftTextRectTransform.rect.height);

        RectTransform treesLeftTextRectTransform = treesLeftText.GetComponent<RectTransform>();
        treesLeftTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, treesLeftTextRectTransform.rect.width);
        treesLeftTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 50, treesLeftTextRectTransform.rect.height);

        RectTransform healthTextRectTransform = healthText.GetComponent<RectTransform>();
        healthTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 10, healthTextRectTransform.rect.width);
        healthTextRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 70, healthTextRectTransform.rect.height);

        RectTransform backgroundRectTransform = background.GetComponent<RectTransform>();
        backgroundRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 25, backgroundRectTransform.rect.width);
        backgroundRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 7, backgroundRectTransform.rect.height);

        score = 0;

        ShowInfo();
    }

    void ShowInfo()
    {
        scoreText.text = scorePrefix + score;
        robotsLeftText.text = robotsLeftPrefix + robotsLeft;
        treesLeftText.text = treesLeftPrefix + treesLeft;
        healthText.text = healthPrefix + health;
    }

    public void IncScore(int value)
    {
        score += value;
        ShowInfo();
    }

    public void DecRobotsLeft()
    {
        robotsLeft -= 1;
        ShowInfo();
    }

    public void DecTreesLeft()
    {
        treesLeft -= 1;
        ShowInfo();
    }
    public void DecHealth()
    {
        health -= 10;
        ShowInfo();
    }

}
