using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemHeld
{
    Treasure,
    Potion,
    Key
}

public class Thief : Enemy
{
    [SerializeField] private ItemHeld itemHeld;
    
    private void OnEnable()
    {
        shotAction = EnemyShot_Default;
        moveAction = Move;
    }

    public override void Move()
    {
        //TO-DO:
        // make thief follow the player with the most items in the inventory
        // if the thief has successfully robbed someone, they will go towards the exit obstacle
    }

    public override void OnDefeat(Player player, int score = 0)
    {
        base.OnDefeat(player, score);
        if (itemHeld == ItemHeld.Potion)
        {
            GameObject potion = Instantiate((GameObject)Resources.Load("Prefabs/Pickups/Potion"));
            potion.transform.position = transform.position;
            return;
        }
        if(itemHeld == ItemHeld.Key)
        {
            GameObject key = Instantiate((GameObject)Resources.Load("Prefabs/Pickups/Key"));
            key.transform.position = transform.position;
            return;
        }
        else
        {
            GameObject treasure = Instantiate((GameObject)Resources.Load("Prefabs/Pickups/Treasure"));
            treasure.GetComponent<Treasure>().increaseScoreAmount = 500;
            treasure.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) RobPlayer(collision.gameObject.GetComponent<Player>());
    }

    private void RobPlayer(Player player)
    {
        player.currentHealth -= 10; //deal damage

        if (player.upgrades.Count > 0)
        {
            player.upgrades.RemoveAt(UnityEngine.Random.Range(0, player.upgrades.Count));
            player.potionsHeld--;
            GameUIManager.Instance.UpdateInventory(Array.FindIndex(GameManager.Instance.players, check => check == player));
            itemHeld = ItemHeld.Potion;
            Debug.Log("Took ur upgrade");
            return;
        }
        if(player.potionsHeld > 0)
        {
            player.potionsHeld--;
            GameUIManager.Instance.UpdateInventory(Array.FindIndex(GameManager.Instance.players, check => check == player));
            itemHeld = ItemHeld.Potion;
            Debug.Log("Took ur potion");
            return;
        }
        if(player.keysHeld > 0)
        {
            player.keysHeld--;
            GameUIManager.Instance.UpdateInventory(Array.FindIndex(GameManager.Instance.players, check => check == player));
            itemHeld = ItemHeld.Key;
            Debug.Log("Took ur key");
            return;
        }
    }
}
