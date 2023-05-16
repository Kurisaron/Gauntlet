using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Pickup
{
    public int increaseScoreAmount;
    
    public override void OnPickUp(Player player)
    {
        player.score += increaseScoreAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            OnPickUp(collision.gameObject.GetComponent<Player>());
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
