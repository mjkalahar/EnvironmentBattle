using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRobot : Target
{
    public float health = 50;

    public override void Process(RaycastHit hit)
    {
        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
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
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(target);
    }
}