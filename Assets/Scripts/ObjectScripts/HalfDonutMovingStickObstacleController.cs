using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutMovingStickObstacleController : MonoBehaviour
{
    [SerializeField] Transform targetLeft;
    [SerializeField] Transform targetRight;
    float timeInterval = 10.0f;
    float timer = 0.0f;
    bool isExtending;
    float speed = 10f;

    void Start()
    {
        if(transform.localPosition.x >= 0)
        {
            isExtending = true;
        }
        else
        {
            isExtending = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeInterval)
        {
            timer = timer - timeInterval;
            isExtending = !isExtending;
            
        }
        if(isExtending && Vector3.Distance(transform.position, targetLeft.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLeft.position, speed * Time.deltaTime);
        }
        else if(!isExtending && Vector3.Distance(transform.position, targetRight.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetRight.position, speed * Time.deltaTime);
        }
    }
}
