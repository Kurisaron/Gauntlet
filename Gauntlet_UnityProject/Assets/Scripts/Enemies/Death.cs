using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{

    private static int timesTriggered = 0;

    private void OnEnable()
    {

    }

    private void Update()
    {
        Move();
    }
    public override void AddScore(Player player , bool hitByPotion)
    {
        scoreIncrease = 1;

        if(hitByPotion == true)
        {
            scoreIncrease = BonusScoreIncrease();
        } 

        player.score += scoreIncrease;
    }

    private int BonusScoreIncrease()
    {
        List<int> scoreEarnedWhenDefeated = new List<int> { 1000, 2000, 1000, 4000, 2000, 6000, 8000 };

        return scoreEarnedWhenDefeated[timesTriggered];
        
    }

    public override void Attack(Player player)
    {
        //Does Death actually use an attack?
    }

    public override void Move()
    {
        transform.position += speed * Time.deltaTime * Vector3.forward;

        /*float distance;

        if(distance <= detectionRadius) 
        {
            //Vector3.MoveTowards(transform.position, GameObject.Find("player(Clone)").transform.position, detectionRadius);
        }*/

    }

    public override void OnDefeat()
    {               
        this.gameObject.SetActive(false);
    }

    private IEnumerator DrainHealth(Player player)
    {
        while (health > 0)
        {
            player.currentHealth -= damage; //drains the player's health
            
            health--; //Death also drains its own health when it's in contact with the player
            Debug.Log("Death's health: "+ health);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {          
            timesTriggered++;
            if (timesTriggered >= 7) timesTriggered = 0;

            //tracks who shot that projectile so that the correct player is awarded a point
            if (other.gameObject.transform.parent.name.Contains("Elf") && FindObjectOfType<Player>().name.Contains("Elf"))
            {
                AddScore(GameObject.Find("Elf").GetComponent<Player>(), false);
            }
            if (other.gameObject.transform.parent.name.Contains("Warrior") && FindObjectOfType<Player>().name.Contains("Warrior"))
            {
                AddScore(GameObject.Find("Warrior").GetComponent<Player>(), false);
            }
            if (other.gameObject.transform.parent.name.Contains("Valkyrie") && FindObjectOfType<Player>().name.Contains("Valkyrie"))
            {
                AddScore(GameObject.Find("Valkyrie").GetComponent<Player>(), false);
            }
            if (other.gameObject.transform.parent.name.Contains("Wizard") && FindObjectOfType<Player>().name.Contains("Wizard"))
            {
                AddScore(GameObject.Find("Wizard").GetComponent<Player>(), false);
            }
        }
        //if collision is a potion attack, do ondefeat() and addscore() and make hitbyPotion true so that it applies the bonus score
    }
    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }
}
