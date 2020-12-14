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
    GameObject starting;
    GameObject countdown;
    GameObject five;
    GameObject four;
    GameObject three;
    GameObject two;
    GameObject one;
    GameObject go;

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
        countdown = transform.Find("Canvas/Countdown").gameObject;
        countdown.SetActive(false);
        starting = transform.Find("Canvas/Starting").gameObject;
        starting.SetActive(false);
        five = countdown.transform.Find("5").gameObject;
        five.SetActive(false);
        four = countdown.transform.Find("4").gameObject;
        four.SetActive(false);
        three = countdown.transform.Find("3").gameObject;
        three.SetActive(false);
        two = countdown.transform.Find("2").gameObject;
        two.SetActive(false);
        one = countdown.transform.Find("1").gameObject;
        one.SetActive(false);
        go = countdown.transform.Find("Go").gameObject;
        go.SetActive(false);

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

    public void UpdateStarting(float time)
    {
        if (time < 1)
        {
            if (starting.activeSelf)
            {
                starting.SetActive(false);
            }
        }
        else
        {
            starting.GetComponent<Text>().text = "Starting In: " + (int)time + "s";
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

    public void ShowStarting()
    {
        starting.SetActive(true);
    }

    public void HideStarting()
    {
        starting.SetActive(false);
    }

    public void UpdateCountdown(float time)
    {
        if (time < 0)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
            if (countdown.activeSelf)
            {
                countdown.SetActive(false);
            }
        }
        else if (time > 5)
        {
            if (time > 5.9999f)
            {
                if (countdown.activeSelf)
                {
                    countdown.SetActive(false);
                }
            }
            else
            {
                if (!countdown.activeSelf)
                {
                    countdown.SetActive(true);
                }
                if (!five.activeSelf)
                {
                    five.SetActive(true);
                }
            }
        }
        else if (time > 4)
        {
            if (!countdown.activeSelf)
            {
                countdown.SetActive(true);
            }
            if (five.activeSelf)
            {
                five.SetActive(false);
            }
            if (!four.activeSelf)
            {
                four.SetActive(true);
            }
        }
        else if (time > 3)
        {
            if (!countdown.activeSelf)
            {
                countdown.SetActive(true);
            }
            if (four.activeSelf)
            {
                four.SetActive(false);
            }
            if (!three.activeSelf)
            {
                three.SetActive(true);
            }
        }
        else if (time > 2)
        {
            if (!countdown.activeSelf)
            {
                countdown.SetActive(true);
            }
            if (three.activeSelf)
            {
                three.SetActive(false);
            }
            if (!two.activeSelf)
            {
                two.SetActive(true);
            }
        }
        else if (time > 1)
        {
            if (!countdown.activeSelf)
            {
                countdown.SetActive(true);
            }
            if (two.activeSelf)
            {
                two.SetActive(false);
            }
            if (!one.activeSelf)
            {
                one.SetActive(true);
            }
        }
        else if (time > 0)
        {
            if (!countdown.activeSelf)
            {
                countdown.SetActive(true);
            }
            if (one.activeSelf)
            { 
                one.SetActive(false);
            }
            if (!go.activeSelf)
            {
                go.SetActive(true);
            }
        }
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
