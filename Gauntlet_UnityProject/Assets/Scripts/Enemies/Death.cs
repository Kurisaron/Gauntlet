using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{

    private static int timesTriggered = 0;

    private void Awake()
    {
        triggerAction = DeathTrigger;
    }

    private void Update()
    {
        Move();
    }
    public void AddScore(Player player, bool hitByPotion)
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

    public override void Move()
    {
        //transform.position += speed * Time.deltaTime * Vector3.forward;

        /*float distance;

        if(distance <= detectionRadius) 
        {
            //Vector3.MoveTowards(transform.position, GameObject.Find("player(Clone)").transform.position, detectionRadius);
        }*/

    }

    public override IEnumerator DrainHealth(Player player)
    {
        while (health > 0)
        {
            Attack(player);
            
            health--; //Death's drain health is different from the others
            Debug.Log("Death's health: "+ health);
            yield return new WaitForSeconds(1);
        }

        if (health <= 0) OnDefeat();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) StartCoroutine(DrainHealth(collision.gameObject.GetComponent<Player>()));       
    }

    private void DeathTrigger(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Shot"))
        {
            timesTriggered++;
            if (timesTriggered >= 7) timesTriggered = 0;

            //tracks who shot that projectile so that the correct player is awarded a point
            if (other.gameObject.transform.parent.name.Contains("Elf") && FindObjectOfType<Player>().name.Contains("Elf"))
            {
                AddScore(GameObject.Find("Elf").GetComponent<Player>(), false);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Warrior") && FindObjectOfType<Player>().name.Contains("Warrior"))
            {
                AddScore(GameObject.Find("Warrior").GetComponent<Player>(), false);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Valkyrie") && FindObjectOfType<Player>().name.Contains("Valkyrie"))
            {
                AddScore(GameObject.Find("Valkyrie").GetComponent<Player>(), false);
                other.gameObject.SetActive(false);
            }
            if (other.gameObject.transform.parent.name.Contains("Wizard") && FindObjectOfType<Player>().name.Contains("Wizard"))
            {
                AddScore(GameObject.Find("Wizard").GetComponent<Player>(), false);
                other.gameObject.SetActive(false);
            }
        }
        //if collision is a potion attack, do ondefeat() and addscore() and make hitbyPotion true so that it applies the bonus score
    }
    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }
}
