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
        //Move();
    }
    public override void AddScore()
    {
        List<int> scoreEarnedWhenDefeated = new List<int> { 1000, 2000, 1000, 4000, 2000, 6000, 8000 };
        /*
         TO-DO: Cycle the score.
         * Every time you hit Death with a shot, you cycle the value. By default, it is 1000 points. The full cycle is, starting from default: 1000, 2000, 1000, 4000, 2000, 6000, 8000, and then back to the default 1000.
         */
        Debug.Log("this part happened");
        for (int i = timesTriggered; i < scoreEarnedWhenDefeated.Count; i++)
        {
            scoreIncrease = scoreEarnedWhenDefeated[i];

            //if (i > scoreEarnedWhenDefeated.Count) i = 0;
        }
        Debug.Log("Score added: " + scoreIncrease);
    }

    public override void Attack(Player player)
    {
        //Does Death actually use an attack?
    }

    public override void Move()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;

        /*float distance;

        if(distance <= detectionRadius) 
        {
            //Vector3.MoveTowards(transform.position, GameObject.Find("player(Clone)").transform.position, detectionRadius);
        }*/

    }

    public override void OnDefeat()
    {
        this.gameObject.SetActive(false);
        AddScore();
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
        if (other.gameObject.transform.parent.gameObject.name.Contains("Shot"))
        {
            health--;
            timesTriggered++;
            if (timesTriggered >= 7) timesTriggered = 0;

            AddScore();
            
            //track who shot that projectile so that the correct player is awarded a point
            //if collision is a potion attack, do ondefeat()
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }
}
