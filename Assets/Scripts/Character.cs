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
    GameObject foreground;
    RectTransform rectTransform;
    
    public void SetupHealthBar()
    {
        healthBar = transform.Find("HealthBar").gameObject;
        foreground = GetHealthBar().transform.Find("Foreground").gameObject;
        rectTransform = foreground.GetComponent<RectTransform>();
    }

    public void UpdateHealthBar()
    {
        rectTransform.sizeDelta = new Vector2(GetHP() * 2, rectTransform.sizeDelta.y);
        GameObject healthBar = GetHealthBar();
        healthBar.transform.LookAt(PlayerManager.Instance.GetPlayer().GetPlayerCamera().transform);
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

    public double GetHPPercent()
    {
        return (double)hp/(double)hpMax;
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
        //Debug.Log("Took damage " + damage + " from " + source);
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

    public void ProcessStun()
    {
        if (isStunned())
        {
            curDuration += Time.deltaTime;
            if (curDuration >= stunDuration)
            {
                Debug.Log("No longer stunned!");
            }
        }
    }
}
