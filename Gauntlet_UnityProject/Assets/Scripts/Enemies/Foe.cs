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

    public void ReduceHealth(float amount)
    {
        health -= amount;

        if (health <= 0) OnDefeat();
    }

    public virtual void OnDefeat() => gameObject.SetActive(false);

    public virtual void PotionAttack(float magicPower)
    {

    }
}
