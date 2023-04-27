using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void OnEnable()
    {
        damage = level * 10;
    }

    public override void AddScore()
    {
        
    }

    public override void Attack(Player player)
    {
        player.currentHealth -= damage;
    }

    public override void Move()
    {
        
    }

    public override void OnDefeat()
    {
        this.gameObject.SetActive(false);
        AddScore();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) Attack(collision.gameObject.GetComponent<Player>());
    }
}
