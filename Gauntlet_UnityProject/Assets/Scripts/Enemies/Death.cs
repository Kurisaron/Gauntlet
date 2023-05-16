using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy
{

    private static int timesTriggered = 0;

    private void Awake()
    {
        //triggerAction = DeathTrigger;
        shotAction = DeathShot;
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

    private void DeathShot(PlayerShot shot)
    {
        timesTriggered++;
        if (timesTriggered >= 7) timesTriggered = 0;

        AddScore(shot.shooter, false);
        shot.gameObject.SetActive(false);
    }

    public override void PotionAttack(float magicPower)
    {
        base.PotionAttack(magicPower);

    }


    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }
}
