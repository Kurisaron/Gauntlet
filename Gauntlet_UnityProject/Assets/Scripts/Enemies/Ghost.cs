using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void Awake()
    {
        //triggerAction = GhostTrigger;
        shotAction = EnemyShot_Default;
    }

    private void OnEnable()
    {
        /*damage = level * 10;
        health = level;
        speed = level * 0.75f;
        scoreIncrease = level * 10;

        Color ghostColor = new Color((165 + (30 * level)) / 255, (165 + (30 * level)) / 255, (165 + (30 * level)) / 255, 1);

        GetComponent<Renderer>().material.color = ghostColor;*/

        //AssignStats();
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
            OnDefeat();
        }
    }


}
