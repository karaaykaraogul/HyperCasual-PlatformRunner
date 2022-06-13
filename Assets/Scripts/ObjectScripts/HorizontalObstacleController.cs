using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject targetLeft;
    [SerializeField] private GameObject targetRight;
    bool isFlipped = false;
    bool isMovingRight = true;
    public float speed = 0.2f;

    void Start()
    {
        if(gameObject.GetComponentInParent<Transform>().rotation.eulerAngles.y > 90)
        {
            isFlipped = true;
        }
    }
    
    void FixedUpdate()
    {
        Vector3 obstaclePos = transform.position;
        if(isMovingRight)
        {
            transform.position = Vector3.MoveTowards(obstaclePos,targetRight.transform.position, speed);
            if(!isFlipped)
            {
                isMovingRight = (obstaclePos.x++ >= targetRight.transform.position.x) ? false : true;
            }
            else{
                isMovingRight = (obstaclePos.x-- <= targetRight.transform.position.x) ? false : true;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(obstaclePos,targetLeft.transform.position, speed);
            if(!isFlipped)
            {
                isMovingRight = (obstaclePos.x-- <= targetLeft.transform.position.x) ? true : false;
            }
            else{
                isMovingRight = (obstaclePos.x++ >= targetLeft.transform.position.x) ? true : false;
            }
        }
    }
}
