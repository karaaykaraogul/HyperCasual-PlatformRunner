using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotatorObstacleController : MonoBehaviour
{
    float direction = 25;
    int randDirection;

    void Start()
    {
        int randDirection = Random.Range(0,2);
        if(randDirection == 0)
        {
            direction = -direction;
        }
        
    }

    void Update()
    {
        transform.Rotate(0,direction*Time.deltaTime,0, Space.Self);
    }
}
