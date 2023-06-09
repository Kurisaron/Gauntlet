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
    private GameObject exit;
    
    private void OnEnable()
    {
        shotAction = EnemyShot_Default;
        moveAction = Move;

        GameEventBus.Subscribe(GameEvent.PlayerAdded, FindPlayer);

        exit = FindObjectOfType<ExitObstacle>(false).gameObject;
    }

    

    private void FindPlayer()
    {
        int mostItems = 0;
        foreach (Player player in GameManager.Instance.players)
        {
            if (player == null) continue;

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
        if (targetPlayer == null) return;

        if (!itemStolen)
        {
            // Move towards the target
            RunTowards(targetPlayer);
        }
        else
        {
            // Move away from the target
            RunAway();
        }
    }

    private void RunAway()
    {
        //Debug.Log("Lobber Running Away");

        transform.position += speed * Time.deltaTime * (exit.transform.position - transform.position);
    }

    private void RunTowards(Player target)
    {
        //Debug.Log("Lobber Running Towards");
        transform.position += speed * Time.deltaTime * (target.transform.position - transform.position);
    }

    public override void OnDefeat(Player player, int score = 0)
    {
        
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
        base.OnDefeat(player, score);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) RobPlayer(collision.gameObject.GetComponent<Player>());

        if (collision.gameObject.GetComponent<ExitObstacle>()) Escape();
    }

    private void RobPlayer(Player player)
    {
        itemStolen = true;
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

    private void Escape()
    {
        gameObject.SetActive(false);
    }
}
