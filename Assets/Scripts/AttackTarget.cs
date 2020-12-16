using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    public float health = 100;
    private float nextTimeToAttack;
    public bool attacking = false;

    private ScoreTracker scoreTrackerScript;
    private GameObject hud;

    void Start()
    {
        nextTimeToAttack = 0.0f;
        hud = GameObject.FindWithTag("HUD");
        scoreTrackerScript = hud.GetComponent<ScoreTracker>();
    }

    void Update()
    {
        bool ready = Time.time >= nextTimeToAttack;
        if (ready && attacking)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (gameObject.tag == "Player")
        {
            health = health - 10;
            scoreTrackerScript.DecHealth();
        }
        else
        {
            health = health - 10;
            if (health <= 0)
            {
                Destroy(gameObject);
                scoreTrackerScript.DecTreesLeft();
                scoreTrackerScript.IncScore(-1);
            }
        }
        nextTimeToAttack = Time.time + 1f;
    }

    public void setAttack(bool attackState)
    {
        attacking = attackState;
    }
}
