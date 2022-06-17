using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    Animator animator;
    [SerializeField] ParticleSystem collisionParticle = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            animator.SetTrigger("hasDied");
            Explode();
            GetComponent<CharacterMovement>().enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }

        if(collision.collider.tag == "FinishLine")
        {
            GameObject.FindWithTag("FinishLine").SetActive(false);
            FindObjectOfType<GameManager>().WinRace();
        }
    }

    void Explode()
    {
        collisionParticle.Play();
    }
}
