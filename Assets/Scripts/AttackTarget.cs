using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    public float health = 100;
    private float nextTimeToAttack;
    public bool attacking = false;

    void Start()
    {
        nextTimeToAttack = 0.0f;
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
        health = health - 10;
        if( health <= 0)
        {
            Destroy(gameObject);
        }
        nextTimeToAttack = Time.time + 1f;
    }

    public void setAttack(bool attackState)
    {
        attacking = attackState;
    }
}
