using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Pickup
{
    [SerializeField] private bool isDestructible; 

    private void OnEnable()
    {
        Color orange = new Color(1, 165f/255f, 0, 1);

        if (!isDestructible) this.gameObject.GetComponent<Renderer>().material.color = orange; //sets the material color to orange
        GameEventBus.Subscribe(GameEvent.ShotPotion, Explode);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.ShotPotion, Explode);
    }

    public override void OnPickUp(Player player)
    {
        //TO-DO:
        //add potion to player inventory
        //update the UI for the player who picked it up to show that change
    }

    public void Explode()
    {
        this.gameObject.SetActive(false);
        /*TO-DO:
         * damage enemies based on what Thor's magic attack is
         */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            OnPickUp(collision.gameObject.GetComponent<Player>());
            this.gameObject.SetActive(false);
        }

        //TO-DO:
        //if isDestructable is true and collision.gameObject is a projectile, GameEventBus.Publish(GameEventBus.ShotPotion)
    }


}
