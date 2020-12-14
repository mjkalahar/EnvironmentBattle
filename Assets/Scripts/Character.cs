using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float hpMax = 100.0f;
    public float hp = 100.0f;

    public bool stunned;
    public bool dead;

    int stunDuration;
    float curDuration;

    GameObject healthBar;
    GameObject healthForeground;
    RectTransform rectTransformHealth;
    
    public void SetupHealthBar()
    {
        healthBar = transform.Find("StatusBars/HealthBar").gameObject;
        healthForeground = GetHealthBar().transform.Find("Foreground").gameObject;
        rectTransformHealth = healthForeground.GetComponent<RectTransform>();
    }

    public void UpdateHealthBar()
    {
        rectTransformHealth.sizeDelta = new Vector2(GetHPPercent() * 2, rectTransformHealth.sizeDelta.y);
        GetHealthBar().transform.LookAt(PlayerManager.Instance.GetPlayer().GetPlayerCamera().transform);
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void Die()
    {
    }

    public float GetHPMax()
    {
        return hpMax;
    }

    public float GetHP()
    {
        return hp;
    }

    public float GetHPPercent()
    {
        return (float)hp/(float)hpMax * 100;
    }

    public GameObject GetHealthBar()
    {
        return healthBar;
    }

    public bool isDead()
    {
        return dead;
    }

    public void Respawn()
    {
        dead = false;
    }

    public bool isStunned()
    {
        return stunned;
    }

    public void TakeDamage(float damage, GameObject source)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            dead = true;
            Die();
        }
    }

    public void GetStunned(int duration, GameObject source)
    {
        stunDuration = duration;
        curDuration = 0.0f;
        stunned = true;
        //Debug.Log("Stunned for " + duration + " from " + source);
    }

    public void Stun()
    {
        stunned = true;
    }

    public void Unstun()
    {
        stunned = false;
    }
}
