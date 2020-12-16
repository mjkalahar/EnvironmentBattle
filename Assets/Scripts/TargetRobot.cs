using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRobot : Target
{
    public float health = 50;
    private ScoreTracker scoreTrackerScript;
    private GameObject hud;
    public RobotController robotController;

    public override void Process(RaycastHit hit)
    {
        effectScript.Play(hit, hitSound, hitEffect, effectDuration);

        robotController = target.GetComponent<RobotController>();
        robotController.IsHit(true);
        TakeHit();
        if (health <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    private void TakeHit()
    {
        health = health - 10;
    }

    IEnumerator Dead() {
        Animator animator = target.GetComponent<Animator>();
        hud = GameObject.FindWithTag("HUD");
        scoreTrackerScript = hud.GetComponent<ScoreTracker>();

        animator.SetBool("dead", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(target);

        scoreTrackerScript.DecRobotsLeft();
        scoreTrackerScript.IncScore(1);
    }
}