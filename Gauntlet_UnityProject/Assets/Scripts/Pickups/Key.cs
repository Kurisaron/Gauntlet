using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    public override void OnPickUp(Player player)
    {
        player.score += 100;
        player.AddItem("key");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            OnPickUp(collision.gameObject.GetComponent<Player>());
            this.gameObject.SetActive(false);
        }
    }
}
