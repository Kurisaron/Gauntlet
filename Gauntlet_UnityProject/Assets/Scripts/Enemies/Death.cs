using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{

    public override void AddScore()
    {
        //add this later
    }

    public override void Attack()
    {
        //Does Death actually use an attack?
    }

    public override void Move()
    {
        //implement moving later
    }

    public override void OnDefeat()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator DrainHealth(Player player)
    {
        while (true)
        {
            player.currentHealth--; //drains the player's health
            
            health--; //Death also drains its own health when it's in contact with the player
            Debug.Log("Death's health: "+ health);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));

        if (collision.gameObject.tag == "projectile") health--;
    }
    private void OnCollisionExit(Collision collision)
    {
        StopCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));
    }
}
