using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Foe
{
    //VARIABLES
    public float damage;
    public float detectionRadius;
    public float scoreIncrease;
    public float speed;

    //FUNCTIONS
    public abstract void Attack();
    public abstract void Move();
    public abstract void OnDefeat();
    public abstract void AddScore();
    
}
