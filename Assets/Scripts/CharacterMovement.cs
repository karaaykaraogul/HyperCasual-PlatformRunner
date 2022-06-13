using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody rb;
    Animator animator;
    public float speed = 5;
    float horizontalInput;
    float horizontalMultiplier = 2;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.State == GameState.Racing)
        {
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);
            animator.SetBool("isRunning", true);
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
