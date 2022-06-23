using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObstacleStickCollision : MonoBehaviour
{
    float direction = 30;
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<CharacterMovement>().enabled = false;
            StartCoroutine(CharacterReenable(coll.gameObject));
        }
        else if(coll.gameObject.tag == "Opponent")
        {
            coll.gameObject.GetComponent<OpponentController>().enabled = false;
            StartCoroutine(OpponentReenable(coll.gameObject));
        }
        Rigidbody collRb = coll.gameObject.GetComponent<Rigidbody>();
        Vector3 diff = collRb.transform.position - transform.position;
        diff = diff.normalized * direction;
        collRb.AddForce(diff, ForceMode.Impulse);
    }

    IEnumerator CharacterReenable(GameObject Player)
    {
        yield return new WaitForSeconds(1f);
        Player.GetComponent<CharacterMovement>().enabled = true;
    }

    IEnumerator OpponentReenable(GameObject Opponent)
    {
        yield return new WaitForSeconds(1f);
        Opponent.GetComponent<OpponentController>().enabled = true;
    }
}
