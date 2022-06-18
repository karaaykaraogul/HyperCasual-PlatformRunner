using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollision : MonoBehaviour
{
    public OpponentController oc;
    Animator animator;
    [SerializeField] ParticleSystem collisionParticle = null;
    [SerializeField] Transform startingPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "Water")
        {
            animator.SetTrigger("hasDied");
            oc.StopAgent();
            oc.enabled = false;
            Invoke("ResetPosition", 2f);
            Explode();
        }

        if(collision.collider.tag == "FinishLine")
        {
            FindObjectOfType<GameManager>().LoseRace();
        }
    }

    void Explode()
    {
        collisionParticle.Play();
    }

    void ResetPosition()
    {
        transform.position = startingPoint.position;
        oc.enabled = true;
        animator.SetTrigger("isReset");
    }
}
