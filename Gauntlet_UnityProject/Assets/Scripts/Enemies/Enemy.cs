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
    public Action moveAction;

    //FUNCTIONS
    protected virtual void Update()
    {
        moveAction?.Invoke();
    }

    public void Attack(Player player) => player.currentHealth -= damage;
    public virtual void Move()
    {
        Player target = null;
        float minDistance = 5000.0f;

        foreach (Player player in GameManager.Instance.players)
        {
            if (player == null) continue;
            
            if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, player.transform.position);
                target = player;
            }
        }

        if (target != null)
        {
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
    }

    public virtual IEnumerator DrainHealth(Player player)
    {
        while (health < 0)
        {
            Attack(player);
            health -= player.MeleePower;

            yield return new WaitForSeconds(1.5f);
        }

        if(health <= 0) OnDefeat(player);
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

    protected void EnemyShot_Default(PlayerShot shot)
    {
        ReduceHealth(shot.shooter.ShotPower, shot.shooter);
        AddScore(shot.shooter, scoreIncrease);
        shot.gameObject.SetActive(false);
    }


}
