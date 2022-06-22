using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MonoBehaviour
{
    float direction = 50;
    float appliedForce = 0;
    int randDirection;

    void Start()
    {
        int randDirection = Random.Range(0,2);
        appliedForce = -(direction)/5;
        //0 right 1 left
        //if direction is positive it will rotate left else right
        if(randDirection == 0)
        {
            //direction right
            direction = -direction;
            appliedForce = -appliedForce;
        }
    }

    void Update()
    {
        transform.Rotate(0,0,direction*Time.deltaTime, Space.Self);
    }

    private void OnCollisionStay(Collision coll)
    {
        coll.gameObject.GetComponent<Rigidbody>().AddForce(coll.gameObject.transform.right * appliedForce);
    }
    
    // private void OnCollisionExit(Collision coll)
    // {
    //     coll.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    // }
}
