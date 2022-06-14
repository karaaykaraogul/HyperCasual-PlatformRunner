using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Vector3 finishPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        finishPoint = GameObject.FindWithTag("Destination").transform.position;
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.State == GameState.Racing)
        {
            agent.isStopped = false;
            agent.SetDestination(finishPoint);
            animator.SetBool("isRunning", true);
        }
        else
        {
            StopAgent();
        }
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }
}
