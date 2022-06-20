using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody rb;
    Animator animator;
    Vector3 winDestination;
    public float speed = 5;
    float horizontalInput;
    float horizontalMultiplier = 2;
    bool isCharInPaintPos = false;
    bool isCamInPaintPos = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        winDestination = GameObject.FindWithTag("Destination").transform.position;
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
        if(GameManager.Instance.State == GameState.Victory)
        {
            if(!isCharInPaintPos && !isCamInPaintPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, winDestination, 10);
                if(transform.position == winDestination)
                {
                    isCharInPaintPos = true;
                    animator.SetBool("isRunning", false);
                }

            }
        }
        if(GameManager.Instance.State == GameState.Lose)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
