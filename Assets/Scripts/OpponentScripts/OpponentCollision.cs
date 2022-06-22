using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollision : MonoBehaviour
{
    public OpponentController oc;
    Animator animator;
    [SerializeField] ParticleSystem collisionParticle = null;
    [SerializeField] Transform startingPoint;

    bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isDead)
        {
            if (collision.collider.tag == "Obstacle" || collision.collider.tag == "Water")
            {
                isDead = true;
                Explode();
                animator.SetTrigger("hasDied");
                oc.StopAgent();
                oc.enabled = false;
                StartCoroutine(ResetPos());
            }
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

    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = startingPoint.position;
        oc.enabled = true;
        isDead = false;
        animator.SetTrigger("isReset");
        yield return null;
    }
}
