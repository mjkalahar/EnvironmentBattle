using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    public float lookRadius = 8f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestTree().transform;
        AttackTarget attackTarget = target.GetComponent<AttackTarget>();
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetFloat("forward", 1.0f);
            animator.SetBool("attack", false);
            attackTarget.setAttack(false);


        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("forward", 0.0f);
            animator.SetBool("attack", false);
            attackTarget.setAttack(false);
        }

        if(distance <= agent.stoppingDistance)
        {
            animator.SetFloat("forward", 0.0f);
            AttackTarget();
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    GameObject FindClosestTree()
    {
        GameObject[] trees;
        trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject closest = PlayerManager.Instance.player;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject tree in trees)
        {
            Vector3 diff = tree.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = tree;
                distance = curDistance;
            }
        }
        return closest;
    }

    void AttackTarget()
    {
        animator.SetBool("attack", true);
        AttackTarget attackTarget = target.GetComponent<AttackTarget>();
        attackTarget.setAttack(true);
    }
}
