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
    private bool itemStolen = false;
    private Player targetPlayer = null;
    
    private void OnEnable()
    {
        shotAction = EnemyShot_Default;
        moveAction = Move;

        int mostItems = 0;
        foreach(Player player in GameManager.Instance.players)
        {
            if (player.keysHeld + player.potionsHeld + player.upgrades.Count > mostItems)
            {
                mostItems = player.keysHeld + player.potionsHeld + player.upgrades.Count;
                targetPlayer = player;
            }
        }
        if (targetPlayer == null && GameManager.Instance.players[0] != null) targetPlayer = GameManager.Instance.players[0];
    }

    public override void Move()
    {
        if (targetPlayer == null) OnDefeat(null);

        if (!itemStolen)
        {
            // Move towards the target
            RunTowards(targetPlayer);
        }
        else
        {
            // Move away from the target
            RunAway(targetPlayer);
        }
    }

    private void RunAway(Player target)
    {
        //Debug.Log("Lobber Running Away");
        if (Vector3.Distance(transform.position, target.transform.position) - 3.0f <= 0.25f) return;

        transform.position += speed * Time.deltaTime * (transform.position - target.transform.position);
    }

    private void RunTowards(Player target)
    {
        //Debug.Log("Lobber Running Towards");
        if (Vector3.Distance(transform.position, target.transform.position) - 3.0f <= 0.25f) return;
        transform.position += speed * Time.deltaTime * (target.transform.position - transform.position);
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
