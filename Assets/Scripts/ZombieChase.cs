// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class ZombieChase : MonoBehaviour
// {
//     private Transform player;
//     private NavMeshAgent agent;
//     public LayerMask obstacleLayers;
//     private Animator animator;
//     private bool playerCaught = false;
//     public Transform[]  destinations;

//     public float chaseDistance = 10f;
//     // public float destDiff = 0.001f;
//     private Vector3 dest;
//     private bool isChasing = false;
//     public float walkingSpeed, runningSpeed;
//     private int currentDestinationIndex = 0;
    
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         player = GameObject.FindWithTag("Player").transform;
//         agent = GetComponent<NavMeshAgent>();
//     }

//     void Update()
//     {
//         if (!playerCaught)
//         {
//             float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
//             if(distanceToPlayer<=chaseDistance) {
//                 Debug.Log("Dist"+"-=-="+distanceToPlayer);
//                 ChasePlayer();
//             }
//             else if(isChasing)
//                 StartCoroutine(GoIdle());
//             // if (isIdle)
//             //     WalkToRandomDestination();
//             else if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
//             {
//                 currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length;
//                 WalkToRandomDestination(currentDestinationIndex);
//             }
//             // UpdateAnimations();
//         }
//     }

//     bool CanSeePlayer()
//     {
//         RaycastHit hit;
//         Vector3 directionToPlayer = (player.position - agent.transform.position).normalized;
//         if (Physics.Raycast(agent.transform.position, directionToPlayer, out hit, chaseDistance, obstacleLayers))
//             return hit.transform == player;
//         return false;
//     }

//     void ChasePlayer()
//     {
//         Debug.Log(agent.transform.position+"//////"+player.position);
//         agent.destination = player.position;
//         isChasing = true;
//         agent.speed = runningSpeed;

//         // animator.SetTrigger("isRunning");
//         // animator.ResetTrigger("isWalking");
//         // animator.ResetTrigger("isIdle");
//     }

//     IEnumerator GoIdle()
//     {
//         isChasing = false;
//         agent.ResetPath();
//         agent.speed = 0f;
//         // isIdle = true;

//         yield return new WaitForSeconds(3f);
//         animator.SetTrigger("isIdle");
//         animator.ResetTrigger("isRunning");
//         animator.ResetTrigger("isWalking");

//         WalkToRandomDestination(currentDestinationIndex);
        
//     }

//     void WalkToRandomDestination(int index)
//     {
//         // float distanceToDest = Vector3.Distance(agent.transform.position, player.position);
//         // Debug.Log(distanceToDest);
//         // if (!agent.hasPath)
//         // {
//             agent.SetDestination(destinations[index].position);
//             agent.speed = walkingSpeed;

//             animator.SetTrigger("isWalking");
//             animator.ResetTrigger("isRunning");
//             animator.ResetTrigger("isIdle");
//         // }
//     }

//     // void UpdateAnimations()
//     // {
//     //     if (isChasing)
//     //     {
//     //         animator.SetTrigger("isRunning");
//     //         animator.ResetTrigger("isWalking");
//     //         animator.ResetTrigger("isIdle");
//     //     }
//     //     else if (!isIdle)
//     //     {
//     //         animator.SetTrigger("isWalking");
//     //         animator.ResetTrigger("isRunning");
//     //         animator.ResetTrigger("isIdle");
//     //     }
//     //     else
//     //     {
//     //         animator.SetTrigger("isIdle");
//     //         animator.ResetTrigger("isRunning");
//     //         animator.ResetTrigger("isWalking");
//     //     }
//     // }
    
//     // void OnTriggerEnter(Collider other)
//     // {
//     //     if (other.CompareTag("Player"))
//     //     {
//     //         playerCaught = true;
//     //     }
//     // }
// }





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieChase : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    public LayerMask obstacleLayers;
    private Animator animator;
    private bool playerCaught = false;
    public Transform[] destinations;
    public float chaseDistance = 10f;
    // public float destDiff = 0.001f;
    //private bool isChasing = false;
    public float walkingSpeed, runningSpeed;
    private int currentDestinationIndex = 0;

    public SceneTransitionManager stm;

    public AudioSource AudioToPause;

    public AudioSource AudioRun;

    public CameraFootsteps playerCameraFootsteps;

    public GameObject zombieCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        WalkToRandomDestination();
    }

    void Update()
    {
        if (!playerCaught)
        {
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
            if(distanceToPlayer < chaseDistance) {
                    if(AudioToPause.isPlaying){
                        AudioToPause.Stop();
                        AudioRun.Play();
                        playerCameraFootsteps.TriggerScared();
                    }
                    // ChasePlayer();
                    agent.destination = player.position;
                    agent.speed = runningSpeed;
                    animator.SetTrigger("isRunning");
                    animator.ResetTrigger("isWalking");
                    animator.ResetTrigger("isIdle");
                    
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
                    if(AudioRun.isPlaying){
                        AudioRun.Stop();
                        AudioToPause.Play();
                    }
                    WalkToRandomDestination();
                }
            }
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - agent.transform.position).normalized;
        if (Physics.Raycast(agent.transform.position, directionToPlayer, out hit, chaseDistance, obstacleLayers))
            return hit.transform == player;
        return false;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        //isChasing = true;
        agent.speed = runningSpeed;

        animator.SetTrigger("isRunning");
        animator.ResetTrigger("isWalking");
        animator.ResetTrigger("isIdle");
    }

    IEnumerator GoIdle()
    {
        animator.SetTrigger("isIdle");
        animator.ResetTrigger("isRunning");
        animator.ResetTrigger("isWalking");

        agent.ResetPath();
        agent.speed = 0f;
        // isIdle = true;

        yield return new WaitForSeconds(3f);
        
        WalkToRandomDestination();
        
    }

    void WalkToRandomDestination()
    {
        agent.SetDestination(destinations[currentDestinationIndex].position);
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length;
        agent.speed = walkingSpeed;

        animator.SetTrigger("isWalking");
        animator.ResetTrigger("isRunning");
        animator.ResetTrigger("isIdle");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCaught = true;
            zombieCam.SetActive(true);
            stm.GoToScene(3);
        }
    }
}
