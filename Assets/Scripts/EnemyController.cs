using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 8f;

    Transform target;
    NavMeshAgent agent;
    //AudioSource groan;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        //groan = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetFloat("forward", 1.0f);
            //PlaySound();
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("forward", 0.0f);
            //groan.Stop();
        }

        if(distance <= agent.stoppingDistance)
        {
            animator.SetFloat("forward", 0.0f);
            FaceTarget();
        }
    }

    /*
    void PlaySound()
    {
        if (!groan.isPlaying)
        {
            groan.Play();
        }
    }*/
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void onDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
