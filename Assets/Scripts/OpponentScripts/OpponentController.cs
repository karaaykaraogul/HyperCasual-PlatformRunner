using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Vector3 finishPoint;
    List<GameObject> waypoints;
    float accuracy = 10;
    int currentWP = 0;
    bool started = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        finishPoint = GameObject.FindWithTag("Destination").transform.position;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.State == GameState.Racing)
        {
            if(!started)
            {
                started = true;
                agent.SetDestination(waypoints[currentWP].transform.position);
                animator.SetBool("isRunning", true);
            }

            if(Vector3.Distance(waypoints[currentWP].transform.position, gameObject.transform.position) < accuracy)
            {
                SetDestination();
            }

            agent.isStopped = false;
            
        }
        else
        {
            StopAgent();
        }
    }

    void SetDestination()
    {
        currentWP++;
        agent.SetDestination(waypoints[currentWP].transform.position);
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }
}
