using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool playerCaught = false;

    public float chaseDistance = 10f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!playerCaught)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= chaseDistance)
            {
                navMeshAgent.SetDestination(player.position);
                animator.SetBool("isMoving", true);
            }
            else
            {
                navMeshAgent.ResetPath();
                animator.SetBool("isMoving", false);
            }
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         playerCaught = true;
    //     }
    // }
}
