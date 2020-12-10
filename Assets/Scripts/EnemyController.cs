using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Target
{
    public float lookRadius = 8f;

    public float difficultyScore = 10;

    public int damage = 10;

    public float secondsForDamage = 2;

    public float range = 10f;

    bool casting = false;

    float currDamageTimer = 0.0f;

    Resource currResourceTarget;
    Character previousTarget;
    Character target;
    NavMeshAgent agent;
    AudioSource groan;
    Animator animator;
   

    void Start()
    {
        SetupHealthBar();
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
        if (playerDistance <= lookRadius)
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
                currResourceTarget = resource;
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
                //animator.SetFloat("forward", 0.0f);
            }
            else
            {
                agent.SetDestination(target.transform.position);
                //animator.SetFloat("forward", 1.0f);
            }

            //Casting
            if(targetDistance <= range)
            {
                if (casting == true)
                {
                    currDamageTimer += Time.deltaTime;
                    if (currDamageTimer >= secondsForDamage)
                    {
                        //Debug.Log("Send Damage!");
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
            agent.isStopped = true;
            agent.ResetPath();
            //animator.SetFloat("forward", 0.0f);
        }

        //Update health bars
        UpdateHealthBar();
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
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        if(target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }

    public void ShootAnimationPlay()
    {

    }

    public override void Process(RaycastHit hit)
    {
        Debug.Log("Enemy take damage: " + PlayerManager.Instance.GetCurrentGun().GetDamage());
        TakeDamage(PlayerManager.Instance.GetCurrentGun().GetDamage(), PlayerManager.Instance.GetPlayer().gameObject);
        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
