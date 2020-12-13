using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    GameObject gameOver;
    GameObject roundCompleted;
    GameObject wave;
    GameObject roundsComplete;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = transform.Find("Canvas/GameOver").gameObject;
        gameOver.SetActive(false);
        roundCompleted = transform.Find("Canvas/RoundCompleted").gameObject;
        roundCompleted.SetActive(false);
        wave = transform.Find("Canvas/NextWave").gameObject;
        wave.SetActive(false);
        roundsComplete = transform.Find("Canvas/RoundsComplete").gameObject;
        roundsComplete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateRoundsComplete(int rounds)
    {
        if (rounds == 1)
        {
            roundsComplete.GetComponent<Text>().text = "You completed " + rounds + " round!";
        }
        else
        {
            roundsComplete.GetComponent<Text>().text = "You completed " + rounds + " rounds!";
        }
    }

    public void ShowRoundsComplete()
    {
        roundsComplete.SetActive(true);
    }

    public void HideRoundsComplete()
    {
        roundsComplete.SetActive(false);
    }

    public void UpdateNextWave(float time)
    {
        wave.GetComponent<Text>().text = "Next wave in: " + (int)time + "s";
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOver.SetActive(false);
    }

    public void ShowRoundCompleted()
    {
        roundCompleted.SetActive(true);
    }

    public void HideRoundCompleted()
    {
        roundCompleted.SetActive(false);
    }

    public void ShowNextWave()
    {
        wave.SetActive(true);
    }

    public void HideNextWave()
    {
        wave.SetActive(false);
    }
}
