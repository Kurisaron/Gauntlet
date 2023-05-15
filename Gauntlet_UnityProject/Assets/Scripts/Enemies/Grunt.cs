using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    private void Awake()
    {
        triggerAction = GruntTrigger;
    }

    private void OnEnable()
    {
        //AssignStats(); //for testing without the generator
    }

    private void Update()
    {
        if(health <= 0)
        {
            OnDefeat();
        }
    }

    public override void Move()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));
    }

    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }

    private void GruntTrigger(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            //health - shot power?
            //also change this so that it accounts for players who have the same class
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
