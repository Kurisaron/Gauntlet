using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Pickup
{
    [SerializeField] private int increaseHealthAmount;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEvent.ShotFood, OnShot);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.ShotFood, OnShot);
    }

    public override void OnPickUp(Player player)
    {
        player.currentHealth += increaseHealthAmount;
    }

    private void OnShot()
    {
        //TO-DO:
        //if a player shoots the food, GameEventBus.Publish(GameEvent.ShotFood);
        this.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            OnPickUp(collision.gameObject.GetComponent<Player>());
            GameEventBus.Publish(GameEvent.AddHealth);
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
