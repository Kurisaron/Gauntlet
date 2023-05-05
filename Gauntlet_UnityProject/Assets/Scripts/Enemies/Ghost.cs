using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private void Awake()
    {
        triggerAction = GhostTrigger;
    }

    private void OnEnable()
    {
        /*damage = level * 10;
        health = level;
        speed = level * 0.75f;
        scoreIncrease = level * 10;

        Color ghostColor = new Color((165 + (30 * level)) / 255, (165 + (30 * level)) / 255, (165 + (30 * level)) / 255, 1);

        GetComponent<Renderer>().material.color = ghostColor;*/

        AssignStats();
    }

    public override void AssignStats()
    {
        base.AssignStats();

        damage = level * 10;
    }

    private void Update()
    {
        Move();
    }

    public override void Attack(Player player)
    {
        player.currentHealth -= damage;
    }

    public override void Move()
    {
        //transform.position += speed * Time.deltaTime * Vector3.forward;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Attack(collision.gameObject.GetComponent<Player>());
            OnDefeat();
        }
    }


    private void GhostTrigger(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            //health - shot power?
            //also change this so that it accounts for multiple 
            //tracks who shot that projectile so that the correct player is awarded a point
            if (other.gameObject.transform.parent.name.Contains("Elf") && FindObjectOfType<Player>().name.Contains("Elf"))
            {
                AddScore(GameObject.Find("Elf").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Warrior") && FindObjectOfType<Player>().name.Contains("Warrior"))
            {
                AddScore(GameObject.Find("Warrior").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Valkyrie") && FindObjectOfType<Player>().name.Contains("Valkyrie"))
            {
                AddScore(GameObject.Find("Valkyrie").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Wizard") && FindObjectOfType<Player>().name.Contains("Wizard"))
            {
                AddScore(GameObject.Find("Wizard").GetComponent<Player>(), scoreIncrease);
                other.gameObject.SetActive(false);
            }
        }
    }
}
