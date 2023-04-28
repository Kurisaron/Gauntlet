using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Foe
{
    //VARIABLES
    public int damage;
    public float detectionRadius;
    public int scoreIncrease;
    public float speed;

    //FUNCTIONS
    public abstract void Attack(Player player);
    public abstract void Move();
    public abstract void OnDefeat();
    public abstract void AddScore(Player player);
    
}
