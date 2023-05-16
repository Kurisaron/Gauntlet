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
        player.AddItem("potion");
    }

    public void Explode()
    {
        gameObject.SetActive(false);
        /*TO-DO:
         * damage enemies based on what Thor's magic attack is
         */
        PotionAttackManager.Instance.PotionHit(transform.position);

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
            Explode();
        }
    }
}
