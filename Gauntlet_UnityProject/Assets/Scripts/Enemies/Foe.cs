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
    public Action<Collider> triggerAction;

    //material color values
    public float matRed;
    public float matGreen;
    public float matBlue;    

    public void OnTriggerEnter(Collider other)
    {
        if (triggerAction != null) triggerAction(other);
    }

    public void AddScore(Player player, int score)
    {
        player.score += score;
    }
}
