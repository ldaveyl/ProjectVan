using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{

    public bool detectPlayer;
    public float detectionRadius;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public Vector3 patrolPosition;

    private float timer;

    private float wanderTimerMin = 15f;
    private float wanderTimerMax = 30f;
    private float wanderTimer;

    public float wanderRadiusMin = 20f;
    public float wanderRadiusMax = 50f;

    // select random position in navmesh
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;

    }

    private void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        detectPlayer = false;

        wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);

        timer = wanderTimer - 2f; // start wandering 2 seconds after spawn

    }

    private void Update()
    {

        timer += Time.deltaTime;
        

        if (detectPlayer == false)
        {

            // wandering
            if (timer >= wanderTimer)
            {

                // randomize time between next wander, and wander destination
                Vector3 wanderPos = RandomNavSphere(transform.position, Random.Range(wanderRadiusMin, wanderRadiusMax), -1);
                navMeshAgent.destination = wanderPos;
                wanderTimer = Random.Range(wanderTimerMin, wanderTimerMax);

                // speed is lower when wandering
                //navMeshAgent.speed = 1f;

                timer = 0f;

            }

        }
        else
        {

            // run to candy with high speed
            //navMeshAgent.speed = 3f;
            //navMeshAgent.destination = patrolPosition;
            //timer = 0f;
        }
    }
}
