using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Foe
{
    //VARIABLES
    public int damage;
    public float detectionRadius;
    public float speed;

    //FUNCTIONS
    public void Attack(Player player) => player.currentHealth -= damage;
    public abstract void Move();
    public void OnDefeat() => gameObject.SetActive(false);

    public virtual IEnumerator DrainHealth(Player player)
    {
        while (health < 0)
        {
            Attack(player);
            health -= player.MeleePower;

            yield return new WaitForSeconds(1.5f);
        }

        if(health <= 0) OnDefeat();
    }

    public virtual void AssignStats()
    {
        health = level;
        scoreIncrease = 10;
        speed = level * 0.75f;
        Color materialColor = new Color((matRed + (30 * level)) / 255, (matGreen + (30 * level)) / 255, (matBlue + (30 * level)) / 255, 1);

        switch (level) //a few enemies have the same damage amount across levels like this
        {
            case 1:
                damage = 5;
                break;
            case 2:
                damage = 8;
                break;
            case 3:
                damage = 10;
                break;
            default:
                Debug.LogError("ruh roh somethin's wrong with this enemy's damage assignment");
                break;
        }

        GetComponent<Renderer>().material.color = materialColor;
    }

}
