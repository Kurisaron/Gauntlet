using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void Awake()
    {
        //triggerAction = GhostTrigger;
        shotAction = EnemyShot_Default;
        moveAction = Move;
    }

    public override void AssignStats()
    {
        base.AssignStats();

        damage = level * 10;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Attack(collision.gameObject.GetComponent<Player>());
            OnDefeat(collision.gameObject.GetComponent<Player>());
        }
    }


}
