using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Ghost,
    Lobber,
    Grunt,
    Demon,
    Sorcerer,
    Thief,
    Death
}


public class Foe : MonoBehaviour
{
    //VARIABLES
    public float health;
    public int level;
    public int scoreIncrease;
    public EnemyType enemyType;
    //public Action<Collider> triggerAction;
    public Action<PlayerShot> shotAction;

    //material color values
    public float matRed;
    public float matGreen;
    public float matBlue;    

    public void OnTriggerEnter(Collider other)
    {
        //if (triggerAction != null) triggerAction(other);

        if (other.gameObject.GetComponentInParent<PlayerShot>() != null && shotAction != null) shotAction(other.gameObject.GetComponentInParent<PlayerShot>());
    }

    public void AddScore(Player player, int score)
    {
        player.score += score;
    }

    public void ReduceHealth(float amount, Player player)
    {
        health -= amount;

        if (health <= 0) OnDefeat(player);
    }

    public virtual void OnDefeat(Player player, int score = 0)
    {
        if (score == 0) score = scoreIncrease;
        if (player != null) AddScore(player, score);
        gameObject.SetActive(false);
    }

    public virtual void PotionAttack(float magicPower, Player player)
    {
        ReduceHealth(health * Mathf.InverseLerp(0.0f, 4.0f, magicPower), player);
        if (health <= 0) OnDefeat(player);
    }
}
