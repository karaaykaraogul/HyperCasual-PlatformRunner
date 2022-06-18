using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformController : MonoBehaviour
{
    float direction = 50;
    int randDirection;
    [SerializeField] Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        int randDirection = Random.Range(0,2);
        if(randDirection == 0)
        {
            direction = -direction;
        }
    }

    void Update()
    {
        transform.Rotate(0,0,direction*Time.deltaTime, Space.Self);
    }

    private void OnCollisionStay(Collision coll)
    {
        coll.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * (-direction/5));
    }
    
    private void OnCollisionExit(Collision coll)
    {
        coll.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
