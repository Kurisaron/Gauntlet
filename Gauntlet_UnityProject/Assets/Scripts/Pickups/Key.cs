using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    public override void OnPickUp(Player player)
    {
        //TO-DO:
        //add key to player inventory and update the UI to reflect the change
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
