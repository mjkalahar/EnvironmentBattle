using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Target
{
    public float aggroRadius = 8f;

    public float difficultyScore = 10;

    public int damage = 10;

    public float secondsForDamage = 2;

    public float damageRange = 10f;

    bool casting = false;

    float currDamageTimer = 0.0f;

    Resource currResourceTarget;
    Character previousTarget;
    Character target;
    NavMeshAgent agent;
    AudioSource groan;
    Animator animator;

    GameObject attackBar;
    GameObject attackForeground;
    RectTransform rectTransformAttack;

    public void SetupAttackBar()
    {
        attackBar = transform.Find("StatusBars/AttackBar").gameObject;
        attackForeground = GetAttackBar().transform.Find("Foreground").gameObject;
        rectTransformAttack = attackForeground.GetComponent<RectTransform>();
    }

    public float GetAttackPercent()
    {
        return (float)currDamageTimer / (float)secondsForDamage * 100;
    }


    public void UpdateAttackBar()
    {
        rectTransformAttack.sizeDelta = new Vector2(GetAttackPercent() * 2, rectTransformAttack.sizeDelta.y);
        GetAttackBar().transform.LookAt(PlayerManager.Instance.GetPlayer().GetPlayerCamera().transform);
    }

    public GameObject GetAttackBar()
    {
        return attackBar;
    }

    void Start()
    {
        SetupHealthBar();
        SetupAttackBar();
        agent = GetComponent<NavMeshAgent>();
        groan = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();       
        transform.rotation = Random.rotation;
    }

    bool ChangeTarget()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);

        //If play is within distance, they are the target
        if (playerDistance <= aggroRadius)
        {
            previousTarget = target;
            target = player;
        }
        //player is out of range, go to old resource target, if dead, pick a new one
        else
        {
            //we need a new resource target
            if (currResourceTarget == null || currResourceTarget.isDead())
            {
                Resource resource = GameManager.Instance.GetRandomAliveResource();
                if (resource != null)
                {
                    currResourceTarget = resource;
                }
            }

            //set our target as resource target
            previousTarget = target;
            target = currResourceTarget;
        }

        return previousTarget != target;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Not cc'd
        if (!isStunned() && !isDead())
        {
            //Update target
            bool targetChange = ChangeTarget();

            //If we changed target, we aren't casting
            if (targetChange)
            {
                currDamageTimer = 0.0f;
                casting = false;
            }

            FaceTarget();

            //Distance to target
            float targetDistance = Vector3.Distance(target.transform.position, transform.position);

            //Movement
            if (targetDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                agent.ResetPath();
                if (animator != null && gameObject.activeSelf)
                {
                    animator.SetFloat("forward", 0.0f);
                }
            }
            else
            {
                if (animator != null && animator.gameObject.activeSelf)
                {
                    animator.SetFloat("forward", 1.0f);
                }
                agent.SetDestination(target.transform.position);
            }

            //Casting
            if(targetDistance <= damageRange)
            {
                if (casting == true)
                {
                    currDamageTimer += Time.deltaTime;
                    if (currDamageTimer >= secondsForDamage)
                    {
                        ShootAnimationPlay();
                        target.TakeDamage(damage, this.gameObject);
                        currDamageTimer = 0.0f;
                    }
                }
                casting = true;
            }
            else
            {
                currDamageTimer = 0.0f;
                casting = false;
            }
        }
        else
        {
            if (animator != null && gameObject.activeSelf)
            {
                animator.SetFloat("forward", 0.0f);
            }
            agent.isStopped = true;
            agent.ResetPath();
        }

        //Update health bars
        UpdateHealthBar();
        UpdateAttackBar();
    }

    void PlaySound()
    {
        if(!groan.isPlaying)
        {
            groan.Play();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
        if(target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }

    public void ShootAnimationPlay()
    {

    }

    public override void Process(RaycastHit hit)
    {
        TakeDamage(PlayerManager.Instance.GetCurrentGun().GetDamage(), PlayerManager.Instance.GetPlayer().gameObject);
        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
