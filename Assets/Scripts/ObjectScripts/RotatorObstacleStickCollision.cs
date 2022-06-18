using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObstacleStickCollision : MonoBehaviour
{
    float direction = 100;
    private void OnCollisionEnter(Collision coll)
    {
        Rigidbody collRb = coll.gameObject.GetComponent<Rigidbody>();
        Vector3 diff = collRb.transform.position - transform.position;
        diff = diff.normalized * direction;
        collRb.AddForce(diff, ForceMode.Impulse);
    }
}
