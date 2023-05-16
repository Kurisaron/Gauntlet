using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private void Awake()
    {
        //triggerAction = GruntTrigger;
        shotAction = EnemyShot_Default;
        moveAction = Move;
    }

    private void OnEnable()
    {
        //AssignStats(); //for testing without the generator
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));
    }

    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }

}
